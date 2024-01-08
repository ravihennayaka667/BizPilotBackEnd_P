using BizPilotBackEndProduction.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BizPilotBackEnd.Core.dbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Customers> Customers { get; set; }
    }
}
