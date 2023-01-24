using System;

namespace ax_blazor_1.Pocos
{
    public partial class GeoLocation
    {
        public Single Latitude { get; set; }

        public Single Longitude { get; set; }

        public Single Altitude { get; set; }

        public string Description { get; set; } = string.Empty;
    }
}