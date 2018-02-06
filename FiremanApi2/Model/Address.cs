// //Fireman->FiremanApi->Address.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FiremanApi2.Model
{
    public class Address
    {
        [Key()]
        public int Id { get; set; }

        public string Label { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public int Rank { get; set; }

        public Department Department { get; set; }

    }
}
