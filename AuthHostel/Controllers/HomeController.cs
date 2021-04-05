using AuthHostel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AuthHostel.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Context db = new Context();

        public ActionResult Money()
        {
            return View(db.MoneyHistories);
        }

        public ActionResult History()
        {
            return View(db.Universals);
        }

        public ActionResult Journal()
        {
            return View(db.Journals);
        }

        public ActionResult Rooms()
        { 
            return View(db.ActualRooms);
        }
        public ActionResult FutureRooms()
        {
            return View(db.FutureRooms);
        }

        [HttpGet]
        public ActionResult EditJournal(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CareJournal jour = db.CareJournals.Find(id);
            if (jour != null)
            {
                SelectList employs = new SelectList(db.Employs, "ID", "FIO");
                ViewBag.Employs = employs;
                SelectList animals = new SelectList(db.Animals, "ID", "Name");
                ViewBag.Animals = animals;
                SelectList cares = new SelectList(db.Cares, "ID", "Name");
                ViewBag.Cares = cares;
                return View(jour);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult EditJournal(CareJournal jour)
        {
            db.Entry(jour).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Journal");
        }

        [HttpPost]
        public ActionResult EditAnimalInRoom(RoomAdd roomAdd)
        {
            Animal animal = new Animal();
            Client client = new Client();
            AnimalInRoom animalInRoom = new AnimalInRoom();

            client.FIO = roomAdd.ClientName;
            client.Login = User.Identity.Name;
            client.ID = roomAdd.ClientID;
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();

            List<Client> clients = db.Clients.ToList();
            for (int i = 0; i < clients.Count(); i++)
                if (clients[i].FIO == client.FIO)
                    animal.ClientID = clients[i].ID;

            animal.Name = roomAdd.AnimalName;
            animal.ID = roomAdd.AnimalID;
            animal.Type = roomAdd.AnimalType;
            animal.Species = roomAdd.AnimalSpecies;
            db.Entry(animal).State = EntityState.Modified;
            db.SaveChanges();

            List<Animal> animals = db.Animals.ToList();
            for (int i = 0; i < animals.Count(); i++)
                if (animals[i].Name == animal.Name)
                    animalInRoom.AnimalID = animals[i].ID;

            animalInRoom.ID = roomAdd.AnimalInRoomID;
            animalInRoom.AnimalID = animal.ID;
            animalInRoom.RoomID = roomAdd.RoomID;
            animalInRoom.ArrivalDate = roomAdd.ArrivalDate;
            animalInRoom.DepartureDate = roomAdd.DepartureDate;
            db.Entry(animalInRoom).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("FutureRooms");
        }

        [HttpGet]
        public ActionResult EditAnimalInRoom(int id)
        {
            AnimalInRoom animalInRooms = db.AnimalInRooms.Find(id);
            Animal animal = db.Animals.Find(animalInRooms.AnimalID);
            Client client = db.Clients.Find(animal.ClientID);
            RoomAdd roomAdd = new RoomAdd();

            roomAdd.AnimalInRoomID = id;
            roomAdd.ClientID = client.ID;
            roomAdd.AnimalID = animal.ID;
            roomAdd.ClientName = client.FIO;
            roomAdd.AnimalName = animal.Name;
            roomAdd.AnimalSpecies = animal.Species;
            roomAdd.AnimalType = animal.Type;
            roomAdd.ArrivalDate = animalInRooms.ArrivalDate;
            roomAdd.DepartureDate = animalInRooms.DepartureDate;
            roomAdd.RoomID = animalInRooms.RoomID;

            SelectList rooms = new SelectList(db.Rooms, "ID", "Name");
            ViewBag.Rooms = rooms;
            if (animalInRooms != null)
            {
                return View(roomAdd);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult CreateJournal()
        {
            // Формируем список команд для передачи в представление
            SelectList employs  = new SelectList(db.Employs, "ID", "FIO");
            ViewBag.Employs = employs;
            SelectList animals = new SelectList(db.Animals, "ID", "Name");
            ViewBag.Animals = animals;
            SelectList cares = new SelectList(db.Cares, "ID", "Name");
            ViewBag.Cares = cares;
            DateTime time = DateTime.Now;
            ViewBag.Time = time.ToString(System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"));
            return View();
        }

        [HttpPost]
        public ActionResult CreateJournal(CareJournal jour)
        {
            db.CareJournals.Add(jour);
            db.SaveChanges();

            return RedirectToAction("Journal");
        }

        [HttpGet]
        public ActionResult CreateAnimalInRoom()
        {
            SelectList rooms = new SelectList(db.Rooms, "ID", "Name");
            ViewBag.Rooms = rooms;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAnimalInRoom(RoomAdd roomAdd)
        {
            Animal animal = new Animal(); 
            Client client = new Client();
            AnimalInRoom animalInRoom = new AnimalInRoom();

            if(User.Identity.Name != "admin@mail.ru")
                client.Login = User.Identity.Name;
            client.FIO = roomAdd.ClientName;

            List<Client> clients = db.Clients.ToList();
            for (int i = 0; i < clients.Count(); i++)
                if (clients[i].FIO == client.FIO)
                    animal.ClientID = clients[i].ID;

            if (!(animal.ClientID > 0))
            { 
                db.Clients.Add(client);
                db.SaveChanges();
                List<Client> clients2 = db.Clients.ToList();
                for (int i = 0; i < clients2.Count(); i++)
                    if ((clients2[i].FIO == client.FIO) && (clients2[i].Login == client.Login))
                        animal.ClientID = clients2[i].ID;
            }

            animal.Name = roomAdd.AnimalName;
            animal.Type = roomAdd.AnimalType;
            animal.Species = roomAdd.AnimalSpecies;

            List<Animal> animals = db.Animals.ToList();
            for (int i = 0; i < animals.Count(); i++)
                if ((animals[i].Name == animal.Name) && (animals[i].Type == animal.Type) && (animals[i].Species == animal.Species) && (animals[i].ClientID == animal.ClientID))
                    animalInRoom.AnimalID = animals[i].ID;

            if (!(animalInRoom.AnimalID > 0))
            {
                db.Animals.Add(animal);
                db.SaveChanges();
                List<Animal> animals2 = db.Animals.ToList();
                for (int i = 0; i < animals2.Count(); i++)
                    if ((animals2[i].Name == animal.Name) && (animals2[i].Type == animal.Type) && (animals2[i].Species == animal.Species) && (animals2[i].ClientID == animal.ClientID))
                        animalInRoom.AnimalID = animals2[i].ID;
            }

            animalInRoom.RoomID = roomAdd.RoomID;
            animalInRoom.ArrivalDate = roomAdd.ArrivalDate;
            animalInRoom.DepartureDate = roomAdd.DepartureDate;
            db.AnimalInRooms.Add(animalInRoom);
            db.SaveChanges();

            return RedirectToAction("FutureRooms");
        }

        public ActionResult DeleteJournal(int id)
        {
            CareJournal b = new CareJournal { ID = id };
            db.Entry(b).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("Journal");
        }

        [Authorize]
        public ActionResult DeleteFutureRoom(int id)
        {
            AnimalInRoom animalInRooms = db.AnimalInRooms.Find(id);
            db.Entry(animalInRooms).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction("FutureRooms");
        }

        public ActionResult DeleteRoom(int id)
        {
            AnimalInRoom animalInRooms = db.AnimalInRooms.Find(id);
            animalInRooms.DepartureDate = DateTime.Now.AddHours(-24);
            db.SaveChanges();

            return RedirectToAction("Rooms");
        }

        public ActionResult DeleteConfirmed(int id)
        {
            CareJournal b = db.CareJournals.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.CareJournals.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Journal");
        }

    }
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}