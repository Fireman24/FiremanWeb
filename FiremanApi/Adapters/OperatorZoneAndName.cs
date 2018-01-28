// //FiremanApi->FiremanApi->OperatorZoneAndName.cs
// //andreygolubkow Андрей Голубков

using FiremanModel;

namespace FiremanApi.Adapters
{
    public struct OperatorZoneAndName
    {
        public OperatorZoneAndName(string name, GpsPoint geoZone)
        {
            Name = name;
            GeoZone = geoZone;
        }

        public string Name { get; set; }

        public GpsPoint GeoZone { get; set; }
    }
}
