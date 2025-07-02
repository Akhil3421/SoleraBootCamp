using Microsoft.EntityFrameworkCore;
using WebAPIExample.Model;

namespace WebAPIExample
{
    public class TraineeContext: DbContext
    {
        public DbSet<Trainee> trainees { get; set; } = default;
        public string conn = "Server=.\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True;";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(conn);
        }
        public DbSet<WebAPIExample.Model.Trainee> Trainee { get; set; } = default!;
    }
}
