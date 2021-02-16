using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class ActualRoom
    {
        [Key]
        public string RoomName { get; set; }
        public string AnimalName { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string OwnerFIO { get; set; }
        public int Value { get; set; }
        public string ArrivalDate { get; set; }
        public string DepartureDate { get; set; }

    }
}