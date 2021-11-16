using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.Core.Models
{
    public class BonusTbl
    {
        [Key]
        public int EmpID { get; set; }
        [Required, MaxLength(150)]
        public string EmpName { get; set; }
        public int Level { get; set; }
        public DateTime JDate { get; set; }
        public decimal Sarlary { get; set; }
        public decimal? Performanse { get; set; }
        public decimal? Bonus { get; set; }
        public DepTbl depTbl { get; set; }
         
        public int DepID { get; set; }
    }
}
