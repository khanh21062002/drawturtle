using Microsoft.EntityFrameworkCore;


namespace Theo_Dõi_Chi_Phí.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Transaction> Giao_dịch { get; set; }
        public DbSet<Category> Danh_mục { get; set; }
    }
}