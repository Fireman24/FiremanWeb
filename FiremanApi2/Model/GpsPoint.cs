// //FireFight->FireFight->GpsPoint.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Gps координаты.
    /// </summary>
    public class GpsPoint
    {       

        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("lat")]
        public double Lat { get; set; }

        [Column("lon")]
        public double Lon { get; set; }
    }
}
