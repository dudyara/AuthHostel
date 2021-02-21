using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthHostel.Models
{
    public class RoomAdd // чисто для фронтенда чтобы собрать данные трех таблиц в одну
    {
        //public string Login { get; set; }
        public int AnimalInRoomID { get; set; }
        public int ClientID { get; set; }
        public int AnimalID { get; set; }
        public string ClientName { get; set; }
        public string AnimalName { get; set; }
        public string AnimalSpecies { get; set; }
        public string AnimalType { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int RoomID { get; set; }
    }
}