using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class AnimalInRoom
    {
        public int ID { get; set; }
        public int AnimalID { get; set; }
        public int RoomID { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
    }
}