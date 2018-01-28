// //Fireman->FiremanWpf->DataLoadUrl.cs
// //andreygolubkow Андрей Голубков

using System;

namespace FiremanWpf.Tools
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DataLoadUrlAttribute : System.Attribute
    {
        public DataLoadUrlAttribute(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}
