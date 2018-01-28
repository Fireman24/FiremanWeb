// //FireFight->FireFight->FireCar.cs
// //andreygolubkow Андрей Голубков

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    /// <inheritdoc />
    /// <summary>
    /// Пожарная машина.
    /// </summary>
    [Table("firecars")]
    public sealed class FireCar : IMapObject, INotifyPropertyChanged
    {
        private Fire _fire;
        private Department _department;
        private GpsPoint _gpsPoint;
        private DateTime _lastUpdateTime;
        private string _num;
        private string _name;
        private int _id;

        public FireCar()
        {
            GpsPoint = new GpsPoint();
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

        [Column("num")]
        public string Num
        {
            get
            {
                return _num;
            }
            set
            {
                if ( value == _num )
                    return;
                _num = value;
                OnPropertyChanged();
            }
        }

        [Column("lastUpdate")]
        public DateTime LastUpdateTime
        {
            get
            {
                return _lastUpdateTime;
            }
            set
            {
                if ( value.Equals(_lastUpdateTime) )
                    return;
                _lastUpdateTime = value;
                OnPropertyChanged();
            }
        }

        public GpsPoint GpsPoint
        {
            get
            {
                return _gpsPoint;
            }
            set
            {
                if ( Equals(value, _gpsPoint) )
                    return;
                _gpsPoint = value;
                OnPropertyChanged();
            }
        }

        public Department Department
        {
            get
            {
                return _department;
            }
            set
            {
                if ( Equals(value, _department) )
                    return;
                _department = value;
                OnPropertyChanged();
            }
        }

        public Fire Fire
        {
            get
            {
                return _fire;
            }
            set
            {
                if ( Equals(value, _fire) )
                    return;
                _fire = value;
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
