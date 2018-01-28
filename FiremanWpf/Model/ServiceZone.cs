// //FireFight->FireFight->ServiceZone.cs
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
    /// Зона обслуживания.
    /// </summary>
    [Table("service_zones")]
    public class ServiceZone : INotifyPropertyChanged
    {
        private List<GpsPoint> _points;
        private string _name;
        private int _id;

        public ServiceZone()
        {
            Points = new List<GpsPoint>();
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

        public virtual List<GpsPoint> Points { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
