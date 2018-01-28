// //FireFight->FireFight->Hydrant.cs
// //andreygolubkow Андрей Голубков

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

using FiremanWpf.Annotations;

namespace FiremanWpf.Model
{
    /// <summary>
    /// Пожарный гидрант.
    /// </summary>
    [Table("hydrants")]
    public class Hydrant : IMapObject, INotifyPropertyChanged
    {
        private string _responsible;
        private GpsPoint _gpsPoint;
        private bool _active;
        private DateTime _revisionDate;
        private int _id;

        public Hydrant()
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

        [Column("revision_date")]
        public DateTime RevisionDate
        {
            get
            {
                return _revisionDate;
            }
            set
            {
                if ( value.Equals(_revisionDate) )
                    return;
                _revisionDate = value;
                OnPropertyChanged();
            }
        }

        [Column("active")]
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                if ( value == _active )
                    return;
                _active = value;
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

        /// <summary>
        /// Ответственный за гидрант.
        /// </summary>     
        [Column("responsible")]
        public string Responsible
        {
            get
            {
                return _responsible;
            }
            set
            {
                if ( value == _responsible )
                    return;
                _responsible = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
