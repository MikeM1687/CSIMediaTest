using CsiMediaTest.Services.Interfaces;
using CSIMediaTest.Models;
using CSIMediaTest.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace CSIMediaTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataService _dataService;

        public HomeController(IDataService dataService)
        {
            this._dataService = dataService;
        }

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

            numbers = String.Join(",", numbersList);

            _dataService.Insert(vm.SortDirection.ToString(), vm.TimeTakenToSort, numbers);

            GetAllRecordsPartialView(vm);

            return View("Index", vm);
        }

        public ActionResult GetAllRecordsPartialView(NumbersViewModel vm)
        {
            var records = _dataService.GetAll();
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

        [HttpPost]
        public FileResult ExportToXml()
        {
            try
            {
                var data = _dataService.GetAll().OrderBy(x => x.TimeTakenToSort).ToList();

                MemoryStream ms = new MemoryStream();
                XmlWriterSettings xws = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,
                    Indent = true
                };

                using (XmlWriter writer = XmlWriter.Create(ms, xws))
                {
                    XDocument doc = new XDocument(
                   new XDeclaration("1.0", "utf-8", "yes"),
                   new XElement("Numbers",
                   from num in data
                   select
                   new XElement("Number", new XElement("NumberString", num.Numbers),
                   new XElement("SortDirection", num.OrderedDirection),
                   new XElement("TimeTakenToSort", num.TimeTakenToSort))));

                    doc.WriteTo(writer);
                }

                ms.Position = 0;

                return File(ms, "application/xml", "Numbers.xml");
            }
            catch (Exception)
            {

                throw;
            }


        }

        private static void SortNumbers(NumbersViewModel vm)
        {
            var timer = Stopwatch.StartNew();

            switch (vm.SortDirection)
            {
                case SortDirection.Descending:
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