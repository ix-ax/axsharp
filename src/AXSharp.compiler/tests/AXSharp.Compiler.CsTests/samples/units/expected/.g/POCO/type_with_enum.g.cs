using System;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public partial interface IGuard
        {
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        public partial class CompareGuardLint : IGuard
        {
            public Int64 CompareToValue { get; set; }

            public global::Simatic.Ax.StateFramework.Condition Condition { get; set; }
        }
    }
}