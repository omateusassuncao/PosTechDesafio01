using Microsoft.EntityFrameworkCore;
using PosTechDesafio01.Model;
using System.Data;

namespace PosTechDesafio01.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Power> Powers { get; set; }

    }
}
