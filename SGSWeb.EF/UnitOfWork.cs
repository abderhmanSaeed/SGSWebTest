using SGSWeb.Core;
using SGSWeb.Core.Interfaces;
using SGSWeb.Core.Models;
using SGSWeb.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IBaseRepository<BonusTbl> BonusTbls { get; private set; }
        public IBaseRepository<DepTbl> DepTbls { get; private set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            BonusTbls = new BaseRepository<BonusTbl>(_context);
            DepTbls = new BaseRepository<DepTbl>(_context);

        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
