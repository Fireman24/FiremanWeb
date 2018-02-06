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
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Column("id")]
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Место, где находится гидрант. Например, адрес.
        /// </summary>
        [Column("place")]
        public string Place { get; set; }
        
        /// <summary>
        /// Дата проверки гидранта.
        /// </summary>
        [Column("revision_date")]
        public DateTime RevisionDate { get; set; }

        /// <summary>
        /// Активен гидрант или нет.
        /// </summary>
        [Column("active")]
        public bool Active { get; set; }
        
        /// <summary>
        /// Характер неисправности.
        /// </summary>
        [Column("fault_problem")]
        public string FaulProblem { get; set; }

        /// <summary>
        /// Координаты гидранта.
        /// </summary>
        public GpsPoint GpsPoint { get; set; } = new GpsPoint();

        /// <summary>
        /// Ответственный за гидрант.
        /// </summary>     
        [Column("responsible")]
        public string Responsible { get; set; }

        /// <summary>
        /// Дополнительное описание.
        /// </summary>
        [Column("descr")]
        public string Description { get; set; }
        
        /// <summary>
        /// Вид противопожарного водоснобжения. Гидрант, водоём или пирс.
        /// </summary>
        [Column("water_type")]
        public string WaterType { get; set; }

    }
}
