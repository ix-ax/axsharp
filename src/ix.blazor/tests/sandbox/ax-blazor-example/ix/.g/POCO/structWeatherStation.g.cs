using System;

namespace Pocos
{
    public partial class structWeatherStation
    {
        public string StationICAO { get; set; } = string.Empty;
        public enumStationStatus StationStatus { get; set; }

        public Single DewPoint { get; set; }

        public Single Pressure { get; set; }

        public Single Temp { get; set; }

        public Single Visibility { get; set; }

        public UInt16 WindHeading { get; set; }

        public Single WindSpeed { get; set; }

        public DateOnly TestDate { get; set; } = default(DateOnly);
        public DateTime TestDateTime { get; set; } = default(DateTime);
        public TimeOnly TestTimeOfDay { get; set; } = default(TimeOnly);
    }
}