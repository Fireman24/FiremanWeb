// //FiremanApi->FiremanApi->GeoObject.cs
// //andreygolubkow Андрей Голубков

using FiremanModel;

namespace FiremanApi.Adapters
{
    public struct GeoObject
    {
        public GeoObject(string marker, GpsPoint gps,string popupText,int id)
        {
            Marker = marker;
            GpsPoint = gps;
            PopupText = popupText;
            Id = id;
        }

        public int Id { get; set; }

        public string PopupText { get; set; }

        public string Marker { get; set; }

        public GpsPoint GpsPoint { get; set; }
    }
}
