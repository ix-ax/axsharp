using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ix.Connector
{
    internal class Polling : IEquatable<Polling>
    {
        private Polling(ITwinElement twinObject,
                            object holdingObject,
                            int interval)
        {
            TwinObject = twinObject;
            KeepPolling = true;
            Interval = interval;
            PollingTask = new Task(() =>
            {
                while (KeepPolling)
                {
                    twinObject?.Poll();
                    Task.Delay(Interval).Wait();
                }
            });
        }

        public static void Add(ITwinElement obj, int interval)
        {
            if (obj == null) return;
            // Object itself is being polled already.
            var po = pollings.FirstOrDefault(p => p.TwinObject == obj);
            if (po != null) po.Interval = interval < po.Interval ? interval : po.Interval;
            // Some parent being polled already.
            po = po ?? pollings.FirstOrDefault(p => p.TwinObject.Symbol.StartsWith(obj.Symbol));
            // Object is not currently polled.
            po = po ?? new Polling(obj, null, interval);

            if (pollings.Add(po))
            {
                po.PollingTask.Start();
            }
            else
            {
                po.Instances++;
            }
        }

        public static void Remove(ITwinElement obj)
        {
            var po = pollings.FirstOrDefault(p => p.TwinObject == obj);
            if (po == null) return;
            po.Instances--;
            if (po.Instances > 0) return;
            po.KeepPolling = false;
            pollings.Remove(po);
        }

        private int Instances { get; set; } = 1;

        private static HashSet<Polling> pollings = new();

        private ITwinElement TwinObject { get; }

        private Task PollingTask { get; }

        private bool KeepPolling { get; set; }

        private int Interval { get; set; }

        public bool Equals(Polling other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TwinObject.Symbol, other.TwinObject.Symbol);
            //&& HoldingObject.GetType() == other.HoldingObject.GetType();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Polling)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TwinObject);
        }
    }
}
