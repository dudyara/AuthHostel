using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int PayTypeID { get; set; }
        public int Value { get; set; }
        public int ClientID { get; set; }
         
    }
}