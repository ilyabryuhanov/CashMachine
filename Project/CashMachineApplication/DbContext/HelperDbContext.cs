using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMachine.DAL.Entities.Entities;

namespace CashMachineApplication.DbContext
{
    public static class HelperDbContext
    {
        private static List<Product> _productsSource;
        static HelperDbContext()
        {
            Refresh();
        }

        public static void Refresh()
        {
           _productsSource = CashMachineDbContext.Instance.Products.ToList();
        }

        public static List<Product> ProductsSource => _productsSource;
    }
}
