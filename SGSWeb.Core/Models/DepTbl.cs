using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGSWeb.Core.Models
{
    public class DepTbl
    {
        [Key]
        public int DepID { get; set; }
        [Required, MaxLength(150)]
        public string DepNAme { get; set; }
    }
}
