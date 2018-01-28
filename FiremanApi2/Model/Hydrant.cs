// //FireFight->FireFight->Hydrant.cs
// //andreygolubkow Андрей Голубков

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Пожарный гидрант.
    /// </summary>
    [Table("hydrants")]
    public class Hydrant : IMapObject
    {
        public Hydrant()
        {
            GpsPoint = new GpsPoint();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        
        [Column("revision_date")]
        public DateTime RevisionDate { get; set; }

        [Column("active")]
        public bool Active { get; set; }

        public GpsPoint GpsPoint { get; set; }

        /// <summary>
        /// Ответственный за гидрант.
        /// </summary>     
        [Column("responsible")]
        public string Responsible { get; set; }

        [Column("descr")]
        public string Description { get; set; }

    }
}
