using Microsoft.EntityFrameworkCore;

namespace EntityCoreMySQLExample
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options){}
        public DbSet<Author> authors { get; set; }
        public DbSet<Book> books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "server=localhost;database=testdb1;user=root;password=root;",
                new MySqlServerVersion(new Version(9, 3, 0))
            );
        }
    }
}
