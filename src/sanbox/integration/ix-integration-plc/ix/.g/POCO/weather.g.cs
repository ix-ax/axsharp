using System;

namespace Pocos
{
    public partial class weather
    {
        public GeoLocation GeoLocation { get; set; } = new GeoLocation();
        public Single Temperature { get; set; }

        public Single Humidity { get; set; }

        public string Location { get; set; } = string.Empty;
        public Single ChillFactor { get; set; }

        public Feeling Feeling { get; set; }
    }

    public partial class weathers
    {
        public weatherBase[] i { get; set; } = new weatherBase[51];
    }
}