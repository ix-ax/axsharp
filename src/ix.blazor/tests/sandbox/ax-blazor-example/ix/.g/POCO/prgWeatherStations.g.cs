using System;

namespace Pocos
{
    public partial class prgWeatherStations
    {
        public fbWorldWeatherWatch _weatherStations { get; set; } = new fbWorldWeatherWatch();
        public string PlcCommentOnCurrentWeather { get; set; } = string.Empty;
    }
}