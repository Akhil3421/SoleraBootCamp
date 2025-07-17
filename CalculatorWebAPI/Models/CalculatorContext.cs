using Microsoft.EntityFrameworkCore;

namespace CalculatorWebAPI.Models
{
    public class CalculatorContext:DbContext
    {
        static string conn = "server=localhost;Database=testdb1;user=root;password=root";
        public DbSet<Calculator> Calculators {  get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySql(conn, new MySqlServerVersion(new Version(9, 0, 3)));
        }
    }
}
