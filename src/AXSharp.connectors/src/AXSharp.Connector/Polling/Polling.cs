using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AXSharp.Connector.ValueTypes;

namespace AXSharp.Connector
{
    internal class Polling
    {

        private static HashSet<ITwinPrimitive> PollingPool { get; } = new HashSet<ITwinPrimitive>();

        private Polling(ITwinElement twinObject,
                            object holdingObject,
                            int interval)
        {

        }

        private static Dictionary<int, Task> PollingTasks { get; } = new();

        public static void Add(ITwinElement obj, int interval)
        {
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    if (PollingPool.Add(primitive))
                    {
                        AddToPolling(interval, primitive as OnlinerBase);
                    }
                    else
                    {
                        UpdatePolling(interval, primitive as OnlinerBase);
                    }
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        if (PollingPool.Add(primitive))
                        {
                            AddToPolling(interval, primitive as OnlinerBase);
                        }
                        else
                        {
                            UpdatePolling(interval, primitive as OnlinerBase);
                        }
                    }
                    break;
            }

            if (!PollingTasks.ContainsKey(interval))
            {
                PollingTasks[interval] = Task.Run(() =>
                {
                    while (true)
                    {
                        List<ITwinPrimitive> list = new List<ITwinPrimitive>();
                        foreach (var primitive in PollingPool) list.Add(primitive);
                        foreach (var twinPrimitive in list.Where(p => p.PollingInterval == interval))
                        {
                            twinPrimitive.Poll();
                        }

                        Task.Delay(interval).Wait();
                    }
                });
            }
        }

        private static void UpdatePolling(int interval, OnlinerBase primitive)
        {
            primitive.PollingsCount++;
            primitive.PollingInterval = primitive.PollingInterval > interval
                ? interval
                : primitive.PollingInterval;
        }

        private static void AddToPolling(int interval, OnlinerBase primitive)
        {
            primitive.PollingInterval = interval;
            primitive.PollingsCount = 1;
        }

        public static void Remove(ITwinElement obj)
        {
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    if (--((OnlinerBase)primitive).PollingsCount <= 0)
                        PollingPool.Remove(primitive);
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        if (--((OnlinerBase)primitive).PollingsCount <= 0)
                            PollingPool.Remove(primitive);

                    }
                    break;
            }
        }
    }
}