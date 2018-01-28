// //FiremanApi->FiremanApi->FirecarData.cs
// //andreygolubkow Андрей Голубков

using System;
using System.Collections.Generic;

using FiremanModel;

namespace FiremanApi.Adapters
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
