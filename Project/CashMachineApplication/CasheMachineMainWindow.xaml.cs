using System.Windows;
using CashMachineApplication.ViewModels;

namespace CashMachineApplication
{
    /// <summary>
    /// Interaction logic for CasheMachineMainWindow.xaml
    /// </summary>
    public partial class CasheMachineMainWindow : Window
    {
        public CasheMachineMainWindow()
        {
            DataContext = new CasheMachineViewModel();
            InitializeComponent();
        }
    }
}
