// //Fireman->FiremanApi->HystoryRecord.cs
// //andreygolubkow Андрей Голубков

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanModel
{
    [Table("History")]
    public class HistoryRecord
    {
        [Column("id")]
        [Key]
        public int Id { get; set; }

        [Column("dateTime")]
        public DateTime DateTime { get; set; }

        [Column("record")]
        public string Record { get; set; }
    }
}
