using Microsoft.EntityFrameworkCore;

namespace Theo_Doi_Chi_Phi.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Transaction> Giao_dich { get; set; }
        public DbSet<Category> Danh_muc { get; set; }
    }
}