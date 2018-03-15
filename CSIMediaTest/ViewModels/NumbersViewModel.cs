using CSIMediaTest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CSIMediaTest.ViewModels
{
    public class NumbersViewModel
    {
        public int Id { get; set; }

        [Required]
        public int Number { get; set; }

        [DisplayName("Sort Direction")]
        public SortDirection SortDirection { get; set; }

        [DisplayName("Time Taken To Sort (ticks)")]
        public int TimeTakenToSort { get; set; }
        public IEnumerable<int> NumbersList { get; set; }
        public string Numbers { get; set; }

        public List<NumberRowViewModel> NumbersRow { get; set; }
    }
}