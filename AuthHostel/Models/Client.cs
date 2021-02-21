using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class Client //таблица клиентов
    {
        [Key]
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Login { get; set; }
    }
}