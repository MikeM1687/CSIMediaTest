using CsiMedia.Entities;
using CsiMediaTest.Services.Interfaces;
using CSIMediaTest.Controllers;
using CSIMediaTest.ViewModels;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CsiMediaTest.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SortNumbersInAscendingOrder()
        {
            var expectedNumbers = ExpectedNumbers().OrderBy(x => x);

            var vm = new NumbersViewModel
            {
                NumbersList = new List<int>
                {
                    5,2,3,4,1
                }
            };

            var mockDataService = new Mock<IDataService>();
            mockDataService.Setup(x => x.Insert(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()));

            var controller = new HomeController(mockDataService.Object);

            var result = controller.Save(vm);

            CollectionAssert.AreEqual(expectedNumbers, vm.NumbersList);

        }

        [Test]
        public void SortNumbersInDescendingOrder()
        {
            var expectedNumbers = ExpectedNumbers().OrderByDescending(x => x);

            var vm = new NumbersViewModel
            {
                SortDirection = CSIMediaTest.Models.SortDirection.Descending,
                NumbersList = new List<int>
                {
                    5,2,3,4,1
                }
            };

            var mockDataService = new Mock<IDataService>();
            mockDataService.Setup(x => x.Insert(It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<string>()));

            var controller = new HomeController(mockDataService.Object);

            var result = controller.Save(vm);

            CollectionAssert.AreEqual(expectedNumbers, vm.NumbersList);
        }

        private List<int> ExpectedNumbers()
        {
            return new List<int>
            {
                1,3,4,5,2
            };
        }

    }
}
