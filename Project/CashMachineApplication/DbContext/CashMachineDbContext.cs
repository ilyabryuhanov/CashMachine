using System;
using System.Data.Entity;
using CashMachine.DAL.Entities.Entities;

namespace CashMachineApplication.DbContext
{
    public class CashMachineDbContext : System.Data.Entity.DbContext
    {
        private static volatile CashMachineDbContext instance;
        private static object syncRoot = new Object();

        public CashMachineDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<CashMachineDbContext>());
        }

        public static CashMachineDbContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new CashMachineDbContext();
                        }
                    }
                }
                return instance;
            }
        }

        public DbSet<Document> Documents { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductOrder> ProductOrders { get; set; }

        public DbSet<ProductStore> ProductStores { get; set; }
    }
}