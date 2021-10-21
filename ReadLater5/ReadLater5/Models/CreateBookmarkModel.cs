using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ReadLater5.Models
{
    public class CreateBookmarkModel
    {
        public int ID { get; set; }

        [StringLength(maximumLength: 500)]
        public string URL { get; set; }

        public string ShortDescription { get; set; }

        public string CategoryName { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
