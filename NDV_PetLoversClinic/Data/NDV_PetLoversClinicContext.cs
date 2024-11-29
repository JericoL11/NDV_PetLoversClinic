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

        public DbSet<Person> Person { get; set; } = default!;
        public DbSet<Contact> Contact { get; set; } = default!;
        public DbSet<Pet> Pet { get; set; } = default!;
    }
}
