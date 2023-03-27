using System;

namespace Pocos
{
    public partial class Motor
    {
        public Boolean isRunning { get; set; }
    }

    public partial class Vehicle
    {
        public Motor m { get; set; } = new Motor();
        public Int16 displacement { get; set; }
    }
}