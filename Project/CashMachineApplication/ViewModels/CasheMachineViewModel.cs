using System.Windows.Input;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;
using CashMachineApplication.Commands;

namespace CashMachineApplication.ViewModels
{
    public class CasheMachineViewModel : ObservableObject
    {
        private ProductCreateViewModel _productCreateViewModel;
        private DocumentsViewModel _documentsViewModel;

        public CasheMachineViewModel()
        {
            ProductCreateViewModel = new ProductCreateViewModel();
            DocumentsViewModel = new DocumentsViewModel();
            RefreshCommand = new RelayCommand((o)=>true, Refresh);
        }

        public ICommand RefreshCommand { get; }

        public ProductCreateViewModel ProductCreateViewModel
        {
            get { return _productCreateViewModel; }
            set { this.Set(ref _productCreateViewModel, value); }
        }

        public DocumentsViewModel DocumentsViewModel
        {
            get { return _documentsViewModel; }
            set { this.Set(ref _documentsViewModel, value); }
        }

        private void Refresh(object obj)
        {
            ProductCreateViewModel.RefreshCommand.Execute(null);
            DocumentsViewModel.RefreshCommand.Execute(null);
        }
    }
}