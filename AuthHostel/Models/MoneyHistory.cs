using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class MoneyHistory // таблица - представление с историей операций
    {
        [Key]
        public string FIO { get; set; }
        public string Date { get; set; }
        public int Value { get; set; }
        public string PayType { get; set; }
    }
}