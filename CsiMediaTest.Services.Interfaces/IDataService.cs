using CsiMedia.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsiMediaTest.Services.Interfaces
{
    public interface IDataService
    {
        void Insert(string direction, int timeTaken, string numbers);
        IEnumerable<SortedNumber> GetAll();
    }
}
