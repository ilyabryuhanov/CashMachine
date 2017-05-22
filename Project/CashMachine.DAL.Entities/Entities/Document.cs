using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;

namespace CashMachine.DAL.Entities.Entities
{
    /// <summary>
    /// Документ (список всех продуктов)
    /// </summary>
    public class Document : ObservableObject
    {
        private ObservableCollection<ProductOrder> _productOrders;
        private DocumentType _documentType;
        private long _documentId;

        public Document()
        {
            DocumentType = DocumentType.Sell;
        }

        [Key]
        public long DocumentId
        {
            get { return _documentId; }
            set { this.Set(ref _documentId, value); }
        }

        public DocumentType DocumentType
        {
            get { return _documentType; }
            set { this.Set(ref _documentType, value); }
        }

        public ObservableCollection<ProductOrder> ProductOrders
        {
            get { return _productOrders; }
            set { this.Set(ref _productOrders, value); }
        }
    }
}
