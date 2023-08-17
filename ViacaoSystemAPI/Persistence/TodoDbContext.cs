using Microsoft.EntityFrameworkCore;
using ViacaoSystemAPI.Entity;

namespace ViacaoSystemAPI.Persistence
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoEventsCx { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoItem>(e =>
            {
                e.HasKey(de => de.Id);
                e.Property(de => de.Title).IsRequired(false).HasMaxLength(100).HasColumnType("varchar(100)");
                e.Property(de => de.Descricao).IsRequired(false).HasMaxLength(250).HasColumnType("varchar(250)");
                e.Property(de => de.IsCompleted);
            });
        }
    }
}
