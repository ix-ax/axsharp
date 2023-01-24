using System;

namespace Pocos
{
    public partial class fbWorldWeatherWatch
    {
        public structWeatherStation NorthPole { get; set; } = new structWeatherStation();
        public structWeatherStation SouthPole { get; set; } = new structWeatherStation();
        public structWeatherStation Verl { get; set; } = new structWeatherStation();
        public structWeatherStation Kriva { get; set; } = new structWeatherStation();
    }
}