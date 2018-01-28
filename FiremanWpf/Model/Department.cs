// //FireFight->FireFight->Department.cs
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
    /// Пожарная часть. 
    /// </summary>
    [Table("departments")]
    public sealed class Department : IMapObject, INotifyPropertyChanged 
    {
        private int _id;
        private string _name;
        private string _address;
        private GpsPoint _gpsPoint;
        private List<FireCar> _fireCars;
        private ServiceZone _serviceZone;

        public Department()
        {
            GpsPoint = new GpsPoint();
            ServiceZone = new ServiceZone();
            FireCars = new List<FireCar>();
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

        [Column("address")]
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                if ( value == _address )
                    return;
                _address = value;
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

        public ServiceZone ServiceZone
        {
            get
            {
                return _serviceZone;
            }
            set
            {
                if ( Equals(value, _serviceZone) )
                    return;
                _serviceZone = value;
                OnPropertyChanged();
            }
        }

        public List<FireCar> FireCars
        {
            get
            {
                return _fireCars;
            }
            set
            {
                if ( Equals(value, _fireCars) )
                    return;
                _fireCars = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return Name + " на " + Address;
        }


        #endregion
    }

    
}
