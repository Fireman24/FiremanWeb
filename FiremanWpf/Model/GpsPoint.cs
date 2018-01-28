// //FireFight->FireFight->GpsPoint.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    /// <summary>
    /// Gps координаты.
    /// </summary>
    [Table("gps_points")]
    public class GpsPoint : INotifyPropertyChanged
    {
        private double _lon;
        private double _lat;
        private int _id;

        [Column("id")]
        [Key]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if ( value == _id )
                    return;
                _id = value;
                OnPropertyChanged();
            }
        }

        [Column("lat")]
        public double Lat
        {
            get
            {
                return _lat;
            }
            set
            {
                if ( value.Equals(_lat) )
                    return;
                _lat = value;
                OnPropertyChanged();
            }
        }

        [Column("lon")]
        public double Lon
        {
            get
            {
                return _lon;
            }
            set
            {
                if ( value.Equals(_lon) )
                    return;
                _lon = value;
                OnPropertyChanged();
            }
        }

        public virtual ServiceZone ServiceZone { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
