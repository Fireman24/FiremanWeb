// //FiremanApi->FiremanApi->VideoStruct.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations.Schema;

namespace FiremanModel
{
    [Table("video_broadcast")]
    public class Broadcast
    {
        public Broadcast(string url)
        {
            Url = url;
        }

        public Broadcast()
        {
        }

        public int Id { get; set; }

        public string Url { get; set; }
    }
}
