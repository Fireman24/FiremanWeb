// //FiremanApi->FiremanApi->OperatorZoneAndName.cs
// //andreygolubkow Андрей Голубков

using FiremanApi2.Model;

namespace FiremanApi2.Adapters
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
