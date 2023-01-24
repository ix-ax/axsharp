using System;

namespace ax_blazor_1.Pocos
{
    public partial class weather
    {
        public Single Temperature { get; set; }

        public Single Humidity { get; set; }

        public string Location { get; set; } = string.Empty;
        public string HeyMan { get; set; } = string.Empty;
        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
    }

    public partial class weathers
    {
        public weather[] i { get; set; }
    }
}