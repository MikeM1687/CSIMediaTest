using CsiMedia.Entities;
using CsiMediaTest.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace CsiMediaTest.Services
{
    public class DataService : IDataService
    {
        private Entities db = new Entities();

        public IEnumerable<SortedNumber> GetAll()
        {
            return db.SortedNumbers;
        }

        public void Insert(string direction, int timeTaken, string numbers)
        {
            db.Insert(direction, timeTaken, numbers);
        }
    }
}
