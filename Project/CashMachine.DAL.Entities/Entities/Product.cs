using System.ComponentModel.DataAnnotations;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;

namespace CashMachine.DAL.Entities.Entities
{
    /// <summary>
    /// Класс продукт
    /// </summary>
    public class Product : ObservableObject
    {
        private string _name;
        private decimal _price;
        
        [Key]
        public int ProductId { get; set; }

        public string Name
        {
            get { return _name; }
            set { this.Set(ref _name, value); }
        }

        public decimal Price
        {
            get { return _price; }
            set { this.Set(ref _price, value); }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}