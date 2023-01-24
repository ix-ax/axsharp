using System;

namespace Pocos
{
    public partial class TestLayoutOverwriting
    {
        public Boolean ix_bool { get; set; }

        public Int16 ix_int { get; set; }

        public string ix_string { get; set; } = string.Empty;
        public TestSimple simple { get; set; } = new TestSimple();
        public prgWeatherStations weather { get; set; } = new prgWeatherStations();
    }
}