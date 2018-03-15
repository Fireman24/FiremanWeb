// //Fireman->FiremanApi->Departure.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanApi2.Model
{

    /// <summary>
    /// Выезд.
    /// </summary>
    public class Departure
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("date_time")]
        public DateTime DateTime { get; set; }
        
        /// <summary>
        /// Цель выезда.
        /// </summary>    
        [Column("intent")]
        public string Intent { get; set; }

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

        [Column("comments")]
        public string Comments { get; set; }

        public GpsPoint GpsPoint { get; set; }

        public Operator Operator { get; set; }

        public List<FireCar> FireCars { get; set; }

        public List<HistoryRecord> History { get; set; }

        public List<Image> Images { get; set; }
        
    }
}
