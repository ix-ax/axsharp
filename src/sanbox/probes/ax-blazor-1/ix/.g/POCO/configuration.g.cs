using System;

namespace ax_blazor_1.Pocos
{
    public partial class ax_blazor_1
    {
        public Int16 myINT { get; set; }

        public weather weather { get; set; } = new weather();
        public weathers weathers { get; set; } = new weathers();
    }
}