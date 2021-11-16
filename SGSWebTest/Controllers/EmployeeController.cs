using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGSWeb.Core;
using SGSWeb.Core.Consts;
using SGSWeb.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace SGSWebTest.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return View(_unitOfWork.BonusTbls.GetAll());
        }

        [HttpGet]
        public IActionResult Index()
        {
            var Amount = _unitOfWork.BonusTbls.GetAll();
           
            ViewBag.Total = Amount.Select(ee => ee.Bonus).Sum();
            foreach (var item in Amount)
            {
               
                if (item.Level == 1)
                {
                   var Total = item.Sarlary * 80  / 100 * 1;
                }
                else if (item.Level == 2)
                {
                    var Total = item.Sarlary * 80  / 100 * 2;
                }
                else
                {
                    var Total = item.Sarlary * 80  / 100 * 3;
                }
                   
               
            }

            var Result = _unitOfWork.BonusTbls.FindAll(b => b.EmpName.Contains(""), new[] { "depTbl" });

            List<BonusTbl> list = (List<BonusTbl>)_unitOfWork.BonusTbls.GetAll();
            ViewBag.DepartmentList = new SelectList(list, "DepID", "DepNAme");
            return View(Result);

        }

        [HttpGet("GetAllWithEmployee")]
        public IActionResult GetAllWithEmployee()
        {
            return Ok(_unitOfWork.BonusTbls.FindAll(b => b.EmpName.Contains(""), new[] { "depTbl" }));
        }

        public IActionResult Create ()
        {
            ViewBag.DepTpl = _unitOfWork.DepTbls.GetAll();
            return View();
        }
        [HttpPost("AddOne")]
        public IActionResult AddOne(BonusTbl bonusTbl)
        {
            var GetDate =   DateTime.Now.Date.Year - bonusTbl.JDate.Year;

                var employee = _unitOfWork.BonusTbls.Add(bonusTbl);

            if (GetDate == 1 )
            {
                employee.Level = 1;
                employee.Bonus = employee.Sarlary * 80 / 100 * 1;
            }
            else if (GetDate == 2 )
            {
                employee.Level = 2;
                employee.Bonus = employee.Sarlary * 80 / 100 * 2;

            }
            else if (GetDate >= 3 )
            {
                employee.Level = 3;
                employee.Bonus = employee.Sarlary * 80 / 100 * 3;
            }
            else 
            {
                employee.Level = 0;
                employee.Bonus = null;
            }
            _unitOfWork.Complete();
            return RedirectToAction(nameof(Index));
           // return Ok(employee);
        }

        public ActionResult Delete(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = _unitOfWork.BonusTbls.SingleOrDefault(b => b.EmpID == employeeId, new[] { "depTbl" });

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int employeeId)
        {
            var employee = _unitOfWork.BonusTbls.GetById(employeeId);
            _unitOfWork.BonusTbls.Delete(employee);
            _unitOfWork.Complete();

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Details(int? employeeId)
        {
            if (employeeId == null)
            {
                return NotFound();
            }
            var employee = _unitOfWork.BonusTbls.SingleOrDefault(b => b.EmpID == employeeId, new[] { "depTbl" });
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult ExportExpencesToExcel()
        {

            DataTable dt = new DataTable("الموظفين");
            dt.Columns.AddRange(new DataColumn[6] {
            new DataColumn("الاسم"),
            new DataColumn("المستوي"),
            new DataColumn("التاريخ"),
            new DataColumn("المرتب"),
            new DataColumn("التقدير"),
            new DataColumn("القسم "),


            });

            var Result = _unitOfWork.BonusTbls.GetAll(new[] { "depTbl" });


            var date = Result.AsEnumerable()
                .Select(m => new
                {

                    m.EmpName,
                    m.Level,
                    m.JDate,
                    m.Sarlary,
                    m.Performanse,
                    m.depTbl.DepNAme,





                });

            foreach (var item in date)
            {
                dt.Rows.Add(
                    item.EmpName,
                    item.Level,
                    item.JDate,
                    item.Sarlary,
                    item.Performanse,
                    item.DepNAme
                    




                    );
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                IXLWorksheet worksheet = wb.AddWorksheet(dt);

                var cols = worksheet.Columns();
                cols.AdjustToContents();

                foreach (var a in cols)
                {//set mas width to 50
                    a.Width = a.Width > 50 ? 50 : a.Width;

                }
                cols.Style.Alignment.WrapText = true;
                //var GovernorateName = _db.Governorates.Where(x => x.Gov_ID == LK_DirectorateId)
                //                                              .Select(x => x.Gov_Name)
                //                                              .FirstOrDefault();
                string govName = " بيانات الموظفين .xlsx";
                worksheet.Tables.FirstOrDefault().ShowAutoFilter = false;
                wb.Style.Alignment.ReadingOrder = XLAlignmentReadingOrderValues.RightToLeft;
                return ClosedXMLExtensions.Deliver(wb, govName);

            }
        }

    }
}
