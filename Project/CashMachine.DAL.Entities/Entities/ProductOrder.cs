using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;

namespace CashMachine.DAL.Entities.Entities
{
    /// <summary>
    /// Информация о заказе данного продукта
    /// </summary>
    public class ProductOrder : ObservableObject
    {
        private const string TotalPriceName = nameof(TotalPrice);
        private const string CountName = nameof(Count);
        private decimal _count;
        private Product _product;
        private Document _document;

        [Key]
        public long ProductOrderId { get; set; }

        [NotMapped]
        public decimal TotalPrice => Count * Product?.Price ?? 0;

        public decimal Count
        {
            get { return _count; }
            set { this.Set(ref _count, value, count => this.RaisePropertyChanged(TotalPriceName)); }
        }

        public Document Document
        {
            get { return _document; }
            set { this.Set(ref _document, value); }
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                this.Set(ref _product, value, product =>
                {
                    this.RaisePropertyChanged(TotalPriceName);
                });
            }
        }
    }
}