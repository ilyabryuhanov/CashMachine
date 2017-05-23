using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;
using CashMachine.DAL.Entities.Entities;
using CashMachineApplication.Commands;
using CashMachineApplication.DbContext;

namespace CashMachineApplication.ViewModels
{
    public class DocumentViewModel : ObservableObject
    {
        private static string OpenString = "Открыт";
        private static string ClosedString = "Закрыт";
        private static string BuyString = "Покупка";
        private static string SellString = "Продажа";
        private ObservableCollection<ProductOrder> _productOrdersSource;
        private List<Product> _productsSource;
        private ProductOrder _selectedProductOrder;
        private readonly Document _document;
        private DocumentStatus _documentStatus;
        private int _totalCount;
        private decimal _totalSum;
        private DocumentType _documentType;
        private decimal _changeWith;

        public DocumentViewModel(Document document)
        {
            _document = document;
            DocumentType = _document.DocumentType;
            ProductOrders = _document.ProductOrders ?? new ObservableCollection<ProductOrder>();
            DocumentStatus = DocumentStatus.Closed;
            TotalCount = ProductOrders.Count;
            TotalSum = ProductOrders.Sum(o => o.TotalPrice);
            ProductsSource = HelperDbContext.ProductsSource;
            ProductOrders.CollectionChanged += ProductOrders_CollectionChanged;
            foreach (var productOrder in ProductOrders)
            {
                productOrder.PropertyChanged += ProductOrder_PropertyChanged;
            }
            AddProductOrderCommand = new RelayCommand((o) => true, AddProductOrder);
            DeleteSelectedProductOrderCommand = new RelayCommand((o) => true, RemoveSelectedProductOrder);
        }

        private void RemoveSelectedProductOrder(object obj)
        {
            SelectedProductOrder.PropertyChanged -= ProductOrder_PropertyChanged;
            ProductOrders.Remove(SelectedProductOrder);
        }

        private void ProductOrder_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            TotalCount = ProductOrders.Count;
            TotalSum = ProductOrders.Sum(o => o.TotalPrice);
        }

        private void AddProductOrder(object obj)
        {
            var productOrder = new ProductOrder() { Document = _document };
            ProductOrders.Add(productOrder);
            productOrder.PropertyChanged += ProductOrder_PropertyChanged;
        }

        private void ProductOrders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            TotalCount = ProductOrders.Count;
            TotalSum = ProductOrders.Sum(o => o.TotalPrice);
        }

        public ICommand AddProductOrderCommand { get; }

        public ICommand DeleteSelectedProductOrderCommand { get; }

        public List<Product> ProductsSource
        {
            get { return _productsSource; }
            set { this.Set(ref _productsSource, value); }
        }

        public ProductOrder SelectedProductOrder
        {
            get { return _selectedProductOrder; }
            set { this.Set(ref _selectedProductOrder, value); }
        }

        public ObservableCollection<ProductOrder> ProductOrders
        {
            get { return _productOrdersSource; }
            set { this.Set(ref _productOrdersSource, value); }
        }

        public Document Document => _document;

        public int TotalCount
        {
            get { return _totalCount; }
            set { this.Set(ref _totalCount, value); }
        }

        public decimal TotalSum
        {
            get { return _totalSum; }
            set { this.Set(ref _totalSum, value, val => this.RaisePropertyChanged("Change")); }
        }

        public decimal ChangeWith
        {
            get { return _changeWith; }
            set { this.Set(ref _changeWith, value, val => this.RaisePropertyChanged("Change")); }
        }

        public decimal Change => ChangeWith - TotalSum;

        public DocumentType DocumentType
        {
            get { return _documentType; }
            set { this.Set(ref _documentType, value, val => this.RaisePropertyChanged("DocumentTypeString")); }
        }

        public DocumentStatus DocumentStatus
        {
            get { return _documentStatus; }
            set { this.Set(ref _documentStatus, value, val => this.RaisePropertyChanged("DocumentStatusString")); }
        }

        public string DocumentTypeString => _document.DocumentType == DocumentType.Buy ? BuyString : SellString;

        public string DocumentStatusString => DocumentStatus == DocumentStatus.Open ? OpenString : ClosedString;
    }
}
