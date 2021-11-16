using SGSWeb.Core.Interfaces;
using SGSWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<BonusTbl> BonusTbls { get; }
        IBaseRepository<DepTbl> DepTbls { get; }


        int Complete();
    }
    
}
