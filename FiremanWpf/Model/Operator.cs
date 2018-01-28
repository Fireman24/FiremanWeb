// //andreygolubkow Андрей Голубков

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    /// <summary>
    /// Оператор центрального пульта.
    /// </summary>
    [Table("operators")]
    public sealed class Operator : INotifyPropertyChanged
    {
        private List<Fire> _fires;
        private GpsPoint _geoZone;
        private string _key;
        private string _name;
        private int _id;

        public Operator()
        {
            Fires = new List<Fire>();
            GeoZone = new GpsPoint();
        }

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

        [Column("name")]
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if ( value == _name )
                    return;
                _name = value;
                OnPropertyChanged();
            }
        }

        [Column("key")]
        public string Key
        {
            get
            {
                return _key;
            }
            set
            {
                if ( value == _key )
                    return;
                _key = value;
                OnPropertyChanged();
            }
        }

        public GpsPoint GeoZone
        {
            get
            {
                return _geoZone;
            }
            set
            {
                if ( Equals(value, _geoZone) )
                    return;
                _geoZone = value;
                OnPropertyChanged();
            }
        }

        public List<Fire> Fires
        {
            get
            {
                return _fires;
            }
            set
            {
                if ( Equals(value, _fires) )
                    return;
                _fires = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
