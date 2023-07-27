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

        public static void Add(ITwinElement obj, int interval, object holder)
        {
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    if (PollingPool.Add(primitive))
                    {
                        AddToPolling(interval, primitive as OnlinerBase, holder);
                    }
                    else
                    {
                        UpdatePolling(interval, primitive as OnlinerBase, holder);
                    }
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        if (PollingPool.Add(primitive))
                        {
                            AddToPolling(interval, primitive as OnlinerBase, holder);
                        }
                        else
                        {
                            UpdatePolling(interval, primitive as OnlinerBase, holder);
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
                        List<OnlinerBase> list = new List<OnlinerBase>();
                        foreach (var primitive in PollingPool) list.Add((OnlinerBase)primitive);
                        foreach (var twinPrimitive in
                                 list.Where(p => p.PollingInterval == interval && p.PollingHolders.Any(a => a is not null)))
                        {
                            twinPrimitive.Poll();
                        }

                        //PollingPool.FirstOrDefault()?.GetParent()?.GetConnector().ReadBatchAsync(PollingPool);
                        Task.Delay(interval).Wait();
                    }
                });
            }
        }

        private static void UpdatePolling(int interval, OnlinerBase primitive, object holder)
        {
            primitive.PollingHolders.Add(holder);
            primitive.PollingInterval = primitive.PollingInterval > interval
                ? interval
                : primitive.PollingInterval;
        }

        private static void AddToPolling(int interval, OnlinerBase primitive, object holder)
        {
            primitive.PollingInterval = interval;
            primitive.PollingHolders.Add(holder);
        }

        public static void Remove(ITwinElement obj, object holder)
        {
            switch (obj)
            {
                case ITwinPrimitive primitive:
                    ((OnlinerBase)primitive).PollingHolders.Remove(holder);
                    if (((OnlinerBase)primitive).PollingHolders.Count <= 0)
                        PollingPool.Remove(primitive);
                    break;
                case ITwinObject twinObject:
                    foreach (var primitive in twinObject.RetrievePrimitives())
                    {
                        ((OnlinerBase)primitive).PollingHolders.Remove(holder);
                        if (((OnlinerBase)primitive).PollingHolders.Count <= 0)
                            PollingPool.Remove(primitive);

                    }
                    break;
            }
        }
    }
}