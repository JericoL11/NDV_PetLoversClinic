using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Data
{
    public class NDV_PetLoversClinicContext : DbContext
    {
        public NDV_PetLoversClinicContext (DbContextOptions<NDV_PetLoversClinicContext> options)
            : base(options)
        {
        }

        #region == fluent Api for diable deleting the person ==
        /* 
          protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
              modelBuilder.Entity<Clients>()
                  .HasOne(c => c.Person)
                  .WithMany() // A person can be associated with many clients
                  .HasForeignKey(c => c.person_Id)
                  .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of Person if associated with Client
          }*/
        #endregion

        public DbSet<Person> Person { get; set; } = default!;
        public DbSet<Clients> Clients { get; set; } = default!;
        public DbSet<Contact> Contact { get; set; } = default!;
        public DbSet<Pet> Pet { get; set; } = default!;

        public DbSet<Breed> Breeds { get; set; } = default!; 
        public DbSet<Specie> Species { get; set; } = default!;

      
    }
}
