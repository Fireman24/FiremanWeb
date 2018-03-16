// //FireFight->FireFight->Department.cs
// //andreygolubkow Андрей Голубков

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{

    /// <summary>
    /// Пожарная часть. 
    /// </summary>
    public sealed class Department : IMapObject 
    {
        public Department()
        {
            GpsPoint = new GpsPoint();
            FireCars = new List<FireCar>();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        /// <summary>
        /// Активна или нет ПЧ.
        /// </summary>
        public bool Active { get; set; } = true;

        public GpsPoint GpsPoint { get; set; }

        public List<FireCar> FireCars { get; set; }
    }

    
}
