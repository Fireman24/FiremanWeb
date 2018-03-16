// //FiremanApi->FiremanApi->VideoStruct.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FiremanApi2.Model
{
    public class Broadcast
    {
        public Broadcast(string url)
        {
            Url = url;
        }

        public Broadcast()
        {
        }

        [Key()]
        public int Id { get; set; }

        public string Url { get; set; }
    }
}
