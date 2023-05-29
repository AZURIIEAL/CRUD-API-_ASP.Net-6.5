using Donations.Models;
using Microsoft.EntityFrameworkCore;

namespace Donations.Data
{
    //Make this a child of DBContext
    public class DonationsAPIDbContext : DbContext
    {
        //A ctor was made which has options and the options would be passed to the base class.
        public DonationsAPIDbContext(DbContextOptions options) : base(options)
        {
        }


        //Create a property that would act as tables for the entity framework core.
        //Create a single property as we only need one model to work with,its of type DBSet<//Having our model domain>.
        public DbSet<Donation> Donations { get; set; }
        //We would need to Inject this to the Program.cs as a service for the connection later.
    }
}
