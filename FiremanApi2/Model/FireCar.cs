// //FireFight->FireFight->FireCar.cs
// //andreygolubkow Андрей Голубков

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Пожарная машина.
    /// </summary>
    [Table("firecars")]
    public class FireCar : IMapObject
    {
        public FireCar()
        {
            GpsPoint = new GpsPoint();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }
    
        [Column("name")]
        public string Name { get; set; }

        [Column("num")]
        public string Num { get; set; }

        [Column("lastUpdate")]
        public DateTime LastUpdateTime { get; set; }

        [Column("spec")]
        public string Specialization { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        public GpsPoint GpsPoint { get; set; }

        public Department Department { get; set; }

        public Broadcast Broadcast { get; set; }

        //TODO: Оставлено для поддержки старой версии api. Когда будет переход на новую версию, нужно будет убрать.
        public Fire Fire { get; set; }
    }
}
