using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;
using CashMachine.DAL.Entities.Entities;
using CashMachineApplication.Commands;
using CashMachineApplication.DbContext;
using CashMachineDbContext = CashMachineApplication.DbContext.CashMachineDbContext;

namespace CashMachineApplication.ViewModels
{
    public class ProductCreateViewModel : ObservableObject
    {
        private readonly CashMachineDbContext _cashMachineDbContext;
        private Product _selectedProduct;
        private ObservableCollection<Product> _products;
        private bool _isShowDataGridEnable;
        
        public ProductCreateViewModel()
        {
            IsShowDataGridEnable = true;
            _cashMachineDbContext = CashMachineDbContext.Instance;
            Refresh(null);
            CreateProductCommand = new RelayCommand((obj) => true, CreateProduct);
            SaveProductCommand = new RelayCommand((obj) => true, SaveProduct);
            RefreshCommand = new RelayCommand((obj) => true, Refresh);
        }

        private void Refresh(object obj)
        {
            HelperDbContext.Refresh();
            Products = new ObservableCollection<Product>(HelperDbContext.ProductsSource);
        }

        private void SaveProduct(object obj)
        {
            _cashMachineDbContext.Products.Add(SelectedProduct);
            _cashMachineDbContext.SaveChanges();
            IsShowDataGridEnable = true;
        }

        private void CreateProduct(object o)
        {
            IsShowDataGridEnable = false;
            var product = new Product();
            Products.Add(product);
            SelectedProduct = product;
        }
        
        public bool IsShowDataGridEnable
        {
            get { return _isShowDataGridEnable; }
            set { this.Set(ref _isShowDataGridEnable, value); }
        }

        public ICommand RefreshCommand { get; }

        public ICommand SaveProductCommand { get; }

        public ICommand CreateProductCommand { get; }

        public ObservableCollection<Product> Products
        {
            get { return _products; }
            set { this.Set(ref _products, value); }
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set { this.Set(ref _selectedProduct, value); }
        }
    }
}