// //FireFight->FireFight->Fire.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    /// <summary>
    /// Пожар.
    /// </summary>
    [Table("fires")]
    public sealed class Fire : IMapObject, INotifyPropertyChanged
    {
        private int _id;
        private string _address;
        private GpsPoint _gpsPoint;
        private DateTime _startDateTime;
        private DateTime? _finishDateTime;
        private string _comments;
        private List<Image> _images;
        private Operator _operator;
        private Department _department;
        private List<FireCar> _fireCars;

        public Fire()
        {
            GpsPoint = new GpsPoint();
            Images = new List<Image>();
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

        [Column("start_time")]
        public DateTime StartDateTime
        {
            get
            {
                return _startDateTime;
            }
            set
            {
                if ( value.Equals(_startDateTime) )
                    return;
                _startDateTime = value;
                OnPropertyChanged();
            }
        }

        [Column("finish_time")]
        public DateTime? FinishDateTime
        {
            get
            {
                return _finishDateTime;
            }
            set
            {
                if ( value.Equals(_finishDateTime) )
                    return;
                _finishDateTime = value;
                OnPropertyChanged();
            }
        }

        [Column("comments")]
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                if ( value == _comments )
                    return;
                _comments = value;
                OnPropertyChanged();
            }
        }

        public List<Image> Images
        {
            get
            {
                return _images;
            }
            set
            {
                if ( Equals(value, _images) )
                    return;
                _images = value;
                OnPropertyChanged();
            }
        }

        public Operator Operator
        {
            get
            {
                return _operator;
            }
            set
            {
                if ( Equals(value, _operator) )
                    return;
                _operator = value;
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
    }
}
