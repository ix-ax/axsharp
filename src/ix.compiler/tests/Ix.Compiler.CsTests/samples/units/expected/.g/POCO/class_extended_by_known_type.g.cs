using System;

namespace Pocos
{
    namespace Simatic.Ax.StateFramework
    {
        public partial class State1Transition : AbstractState
        {
        }
    }

    namespace Simatic.Ax.StateFramework
    {
        public partial class AbstractState : IState, IStateMuteable
        {
            public Int16 StateID { get; set; }

            public string StateName { get; set; } = string.Empty;
        }
    }
}