using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace AuthHostel.Models
{
    public class CareJournal //таблица, содержащая идентификаторы параметров события ухода
    {
        [Key]
        public int ID { get; set; }
        public DateTime Time { get; set; } 
        public int EmployID { get; set; }
        public int CareID { get; set; }
        public int AnimalID { get; set; }
    }
}