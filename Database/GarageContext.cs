using GarageMueller.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GarageMueller.Database
{   /// <summary>
    /// Fake InMemory Datenbank
    /// </summary>
    public class GarageContext : DbContext
    {
        public GarageContext(DbContextOptions<GarageContext> options)  : base(options)
        {

        }

        public DbSet<Auto> Autos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auto>().HasData(
                new Auto
                {
                    Id = 1,
                    Marke = "BMW",
                    Modell = "M1",
                },
                new Auto
                {
                    Id = 2,
                    Marke = "Opel",
                    Modell = "Mokka",
                },
                new Auto
                {
                    Id = 3,
                    Marke = "Tesla",
                    Modell = "S1",
                });
        }
    }
}
