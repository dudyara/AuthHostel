using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class Journal
    {
        [Key]
        public int CareID { get; set; }
        public string AnimalName { get; set; }
        public string OwnerFIO { get; set; }
        public string EmployFIO { get; set; }
        public string CareType { get; set; }
        public DateTime Time { get; set; }
    }
}