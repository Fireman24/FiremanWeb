// //FiremanApi->FiremanApi->Image.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FiremanApi2.Model
{
    public class Image
    {
        [Key()]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}
