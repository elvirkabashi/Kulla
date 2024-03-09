using KULLA.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KULLA.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Produkti> Produktet {  get; set; }
        public DbSet<Shitja> Shitjet {  get; set; }
        public DbSet<Fatura> Faturat {  get; set; }

    }
}
