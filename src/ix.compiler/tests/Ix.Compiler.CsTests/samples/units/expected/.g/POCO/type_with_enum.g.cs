using System;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public interface IGuard
        {
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        public partial class CompareGuardLint : IGuard
        {
            public Int64 Value { get; set; } = new Int64();
            public Int64 CompareToValue { get; set; }

            public units.Simatic.Ax.StateFramework.Condition Condition { get; set; }
        }
    }
}