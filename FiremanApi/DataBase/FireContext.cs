// //FireFight->FireFight->FireContext.cs
// //andreygolubkow Андрей Голубков

using FiremanModel;

using Microsoft.EntityFrameworkCore;

namespace FiremanApi.DataBase
{
    public class FireContext : DbContext
    {       

        public DbSet<Department> Departments { get; set; }

        public DbSet<Fire> Fires { get; set; }

        public DbSet<FireCar> FireCars { get; set; }

        public DbSet<GpsPoint> GpsPoints { get; set; }

        public DbSet<Hydrant> Hydrants { get; set; }

        public DbSet<Operator> Operators { get; set; }

        public DbSet<Departure> Departures { get; set; }


        public DbSet<Image> Images { get; set; }

        public DbSet<GpsRecord> GpsLog { get; set; }

        public DbSet<Broadcast> VideoStructs { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public FireContext(DbContextOptions<FireContext> options) : base(options)
        {
                          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

    }
}
