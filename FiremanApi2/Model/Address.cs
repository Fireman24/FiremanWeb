// //Fireman->FiremanApi->Address.cs
// //andreygolubkow Андрей Голубков

using System.ComponentModel.DataAnnotations;

namespace FiremanApi2.Model
{
    /// <summary>
    /// Адрес.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        [Key()]
        public int Id { get; set; }

        /// <summary>
        /// Общепринятое название  объекта.
        /// </summary>
        public string Label { get; set; }
        
        /// <summary>
        /// Расположение объекта.
        /// </summary>
        public string Place { get; set; }

        /// <summary>
        /// Категория объекта.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Координаты объекта.
        /// </summary>
        public GpsPoint GpsPoint { get; set; } = new GpsPoint();

        /// <summary>
        /// Ранг, при пожаре.
        /// </summary>
        public int Rank { get; set; } = 0;

        /// <summary>
        /// Дополнительное описание объекта.
        /// </summary>
        public string Description { get; set; }
        
        /// <summary>
        /// Отделение пожарной части, к которой относится адрес.
        /// </summary>
        public Department Department { get; set; }
    }
}
