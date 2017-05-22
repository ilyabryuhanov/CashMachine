using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashMachine.DAL.Entities.Entities
{
    public enum DocumentType
    {
        Sell,
        Buy
    }

    public enum DocumentStatus
    {
        Open,
        Closed
    }
}
