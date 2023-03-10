using FluxoDeCaixa.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluxoDeCaixa.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Status> Status { get; set; }
        public DbSet<Caixa> Caixa { get; set; }
        public DbSet<Movimento> Movimento { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public ApplicationDbContext() { }
    }
}