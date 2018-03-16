// //andreygolubkow Андрей Голубков

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Оператор центрального пульта.
    /// </summary>
    public sealed class Operator
    {
        public Operator()
        {
            Fires = new List<Fire>();
            GeoZone = new GpsPoint();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }
        
        [Column("name")]
        public string Name { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("key")]
        public string Key { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        public GpsPoint GeoZone { get; set; }

        public List<Fire> Fires { get; set; }

    }
}
