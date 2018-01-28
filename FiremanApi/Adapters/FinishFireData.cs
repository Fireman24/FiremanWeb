// //firemanapi->FiremanApi->FinishFireData.cs
// //andreygolubkow Андрей Голубков

using System;

namespace FiremanApi.Adapters
{
    public struct FinishFireData
    {
        public int IdFire { get; set; }
        
        public DateTime FinishDateTime { get; set; }
        
    }
}
