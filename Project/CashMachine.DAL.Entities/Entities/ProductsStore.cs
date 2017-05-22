using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;

namespace CashMachine.DAL.Entities.Entities
{
    public class ProductStore : ObservableObject
    {
        private Product _product;
        private decimal _totalCount;

        [Key]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public decimal TotalCount
        {
            get { return _totalCount; }
            set { this.Set(ref _totalCount, value); }
        }

        public Product Product
        {
            get { return _product; }
            set { this.Set(ref _product, value); }
        }
    }
}