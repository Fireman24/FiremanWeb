// //FireFight->FireFight->Fire.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Пожар.
    /// </summary>
    [Table("fires")]
    public sealed class Fire : IMapObject
    {
        public Fire()
        {
            GpsPoint = new GpsPoint();
            Images = new List<Image>();
            FireCars = new List<FireCar>();
        }

        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("rank")]
        public int Rank { get; set; }

        public GpsPoint GpsPoint { get; set; }

        [Column("start_time")]
        public DateTime StartDateTime { get; set; }

        [Column("finish_time")]
        public DateTime? FinishDateTime { get; set; } 

        [Column("comments")]                                                                                
        public string Comments { get; set; }

        /// <summary>
        /// Кто принял.
        /// </summary>
        [Column("receiver")]
        public string Receiver { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        /// <summary>
        /// Руководитель.
        /// </summary>
        [Column("manager")]
        public string Manager { get; set; }

        public List<HistoryRecord> History { get; set; }
        
        public List<Image> Images { get; set; }
        
        public Operator Operator { get; set; }

        public Department Department { get; set; }

        public List<FireCar> FireCars { get; set; }


    }
}
