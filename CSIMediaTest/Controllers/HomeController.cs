using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CSIMediaTest.Controllers
{
    public class HomeController : Controller
    {
        private Entities db = new Entities();

        public ActionResult Index(NumbersViewModel vm)
        {
            GetAllRecordsPartialView(vm);
            return View(vm);
        }

        [HttpPost]
        public ActionResult Save(NumbersViewModel vm)
        {
            if (vm.NumbersList == null)
            {
                ModelState.AddModelError("", "Error");
                return RedirectToAction("Index", vm);
            }

            SortNumbers(vm);

            string numbers = ""; ;
            var numbersList = vm.NumbersList;

            foreach (var number in numbersList)
            {
                numbers = String.Join(",", numbersList);
            }

            db.Insert(vm.SortDirection, vm.TimeTakenToSort, numbers);

            GetAllRecordsPartialView(vm);

            return View("Index", vm);
        }

        public ActionResult GetAllRecordsPartialView(NumbersViewModel vm)
        {
            var records = db.SelectAll();
            vm.NumbersRow = new List<NumberRowViewModel>();

            records.OrderBy(x => x.TimeTakenToSort).ToList().ForEach(x =>
            {
                vm.NumbersRow.Add(new NumberRowViewModel
                {
                    Numbers = x.Numbers,
                    TimeTakenToSort = x.TimeTakenToSort,
                    SortDirection = x.OrderedDirection
                });
            });

            return PartialView("_ShowAllSortings", vm);
        }

        public ActionResult ExportToXml()
        {
            var data = db.SortedNumbers.OrderBy(x => x.TimeTakenToSort).ToList();

            XDocument doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("Numbers",
                from num in data
                select
                new XElement("Number", new XElement("NumberString", num.Numbers),
                new XElement("SortDirection", num.OrderedDirection),
                new XElement("TimeTakenToSort", num.TimeTakenToSort))));

            doc.Save("C:/Temp/Numbers.xml");

            return Json(new { success = true });

        }
    
        private static void SortNumbers(NumbersViewModel vm)
        {
            var timer = Stopwatch.StartNew();

            switch (vm.SortDirection)
            {
                case "Descending":
                    vm.NumbersList = vm.NumbersList.OrderByDescending(x => x);
                    break;
                default:
                    vm.NumbersList = vm.NumbersList.OrderBy(x => x);
                    break;
            }

            timer.Stop();

            vm.TimeTakenToSort = (int)timer.Elapsed.Ticks;

        }
    }
}