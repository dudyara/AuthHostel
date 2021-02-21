using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;


namespace AuthHostel.Models
{
    public class Context : DbContext
    {
        public DbSet<Journal> Journals { get; set; }
        public DbSet<MoneyHistory> MoneyHistories { get; set; }
        public DbSet<Universal> Universals { get; set; }
        public DbSet<ActualRoom> ActualRooms { get; set; }
        public DbSet<FutureRoom> FutureRooms { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalInRoom> AnimalInRooms { get; set; }
        public DbSet<Care> Cares { get; set; }
        public DbSet<CareJournal> CareJournals { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employ> Employs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        
    }
}