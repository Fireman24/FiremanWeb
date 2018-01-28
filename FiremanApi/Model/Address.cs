// //Fireman->FiremanApi->Address.cs
// //andreygolubkow Андрей Голубков
namespace FiremanModel
{
    public class Address
    {
        public int Id { get; set; }

        public string Label { get; set; }

        public double Lat { get; set; }

        public double Lon { get; set; }

        public int Rank { get; set; }

        public Department Department { get; set; }

    }
}
