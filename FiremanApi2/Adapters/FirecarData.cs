// //FiremanApi->FiremanApi->FirecarData.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;

using FiremanApi2.Model;

namespace FiremanApi2.Adapters
{
    public struct FirecarData
    {
        public bool FireAvailable;

        public string Address;

        public string Description;

        public DateTime TaskDateTime;

        public List<String> LogList;

        public List<Image> ImagesList;    


    }
}
