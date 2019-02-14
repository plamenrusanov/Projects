using System;
using System.ComponentModel.DataAnnotations;

namespace Eventures.Models
{
    public class MyEventViewModel
    {

        public string Name { get; set; }

        public string Place { get; set; }

        public DateTime Start { get; set; }

        public string ImageUrl { get; set; }

        [Display(Name ="Adults:")]
        public int AdultQuantity { get; set; }

        [Display(Name ="Children:")]
        public int ChildQuantity { get; set; }
    }
}
