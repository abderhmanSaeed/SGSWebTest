using Microsoft.EntityFrameworkCore;
using SGSWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

       
        public DbSet<BonusTbl> bonusTbls { get; set; }
        public DbSet<DepTbl>  depTbls { get; set; }


    }
}
