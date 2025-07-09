using Microsoft.EntityFrameworkCore;

namespace MovieDirectorWebApi
{
    public class MyDbContext: DbContext
    {
        //MyDbContext() { }
        public string conn = "Data Source=SOLNB-9GX9F94\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;";

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Director> Directors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(conn);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Movie>().HasMany(m => m.Director).WithMany(d => d.Movies).UsingEntity(j => j.ToTable("MovieDirector"));
        }
    }
}
