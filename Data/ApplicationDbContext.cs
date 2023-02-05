using Microsoft.EntityFrameworkCore;
using Project_API_Midursan_Velusamy_2031313.Models;

namespace Project_API_Midursan_Velusamy_2031313.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {
        }

        public DbSet<session> sessions { get; set; }
        public DbSet<task> tasks { get; set; }
        public DbSet<user> users { get; set; }

    }
}

