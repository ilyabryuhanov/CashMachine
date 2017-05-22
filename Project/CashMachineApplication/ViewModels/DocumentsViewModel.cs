using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using CashMachine.Common.CrossCutting.Entities;
using CashMachine.Common.CrossCutting.Helpers;
using CashMachine.DAL.Entities.Entities;
using CashMachineApplication.Commands;
using CashMachineApplication.DbContext;

namespace CashMachineApplication.ViewModels
{
    public class DocumentsViewModel :ObservableObject
    {
        private CashMachineDbContext _cashMachineDbContext;
        private ObservableCollection<DocumentViewModel> _documents;
        private DocumentViewModel _selectedDocument;
        private bool _isShowDataGridEnable;
        
        public DocumentsViewModel()
        {
            IsShowDataGridEnable = true;
            _cashMachineDbContext = CashMachineDbContext.Instance;
            Refresh(null);
            CreateNewDocumentCommand = new RelayCommand((obj) => true, CreateNewDocument);
            SaveDocumentCommand = new RelayCommand((obj) => true, SaveDocument);
            RefreshCommand = new RelayCommand((obj) => true, Refresh);
        }

        private void Refresh(object obj)
        {
            HelperDbContext.Refresh();
            var documents = _cashMachineDbContext.Documents.Include("ProductOrders.Product").ToList();
            Documents = new ObservableCollection<DocumentViewModel>(documents.Select(d => new DocumentViewModel(d)));
        }

        private void SaveDocument(object obj)
        {
            var selectedDocumentDocument = SelectedDocument.Document;
            SelectedDocument.DocumentStatus = DocumentStatus.Closed;
            _cashMachineDbContext.ProductOrders.AddRange(SelectedDocument.ProductOrders);
            selectedDocumentDocument.ProductOrders = new ObservableCollection<ProductOrder>(SelectedDocument.ProductOrders);
            _cashMachineDbContext.Documents.Add(selectedDocumentDocument);
            _cashMachineDbContext.SaveChanges();
            IsShowDataGridEnable = true;
        }

        private void CreateNewDocument(object o)
        {
            IsShowDataGridEnable = false;
            var documentViewModel = new DocumentViewModel(new Document()) {DocumentStatus = DocumentStatus.Open};
            Documents.Add(documentViewModel);
            SelectedDocument = documentViewModel;
        }

        public ICommand RefreshCommand { get; }

        public ICommand SaveDocumentCommand { get; }

        public ICommand CreateNewDocumentCommand { get; }
        
        public bool IsShowDataGridEnable
        {
            get { return _isShowDataGridEnable; }
            set { this.Set(ref _isShowDataGridEnable, value); }
        }

        public ObservableCollection<DocumentViewModel> Documents
        {
            get { return _documents; }
            set { this.Set(ref _documents, value); }
        }

        public DocumentViewModel SelectedDocument
        {
            get { return _selectedDocument; }
            set { this.Set(ref _selectedDocument,value); }
        }


    }
}