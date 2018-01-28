// //FiremanApi->FiremanApi->VideoStruct.cs
// //andreygolubkow Андрей Голубков

using System;

namespace FiremanWpf.Model
{
    public class Video
    {
        public Video(DateTime dateTime, String fileName)
        {
            FileName = fileName;
            DateTime = dateTime;
        }

        public Video()
        {

        }

        public int Id { get; set; }

        public string FileName { get; set; }

        public DateTime DateTime { get; set; }
}
}
