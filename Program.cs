using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleShop
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new VehicleShopContext())
            {
                Console.Write("Enter a name for a new vehicle:");
                var name = Console.ReadLine();

                var vehiclemake = new VehicleMake { VehicleName = name };
                db.VehicleMakes.Add(vehiclemake);
                db.SaveChanges();

                var query = from b in db.VehicleMakes
                            orderby b.VehicleName
                            select b;
                foreach (var item in query)
            {
                    Console.WriteLine(item.VehicleName);
            }
            }
        }
    }
    public class VehicleMake
    {
        public int VehicleMakeId { get; set; }
        public string VehicleName { get; set; }

        public string url { get; set; }

        public virtual List<VehicleModel> VehicleModels { get; set; }
       
    }
    public class VehicleModel
    {
        public string VehicleModelId { get; set; }
        public string ModelName { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string MakeId { get; set; }
        public int YearBuilt { get; set; }
        public string Color { get; set; }
        
        public int VehicleMakeId { get; set; }
        public virtual VehicleMake VehicleMake { get; set; }
    }
    public class User
    {
        [Key]
        public string Username { get; set; }
        public string DisplayName { get; set; }
    }
    public class VehicleShopContext : DbContext
    {  
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.DisplayName)
                .HasColumnName("display_name");
        }
    }
}
