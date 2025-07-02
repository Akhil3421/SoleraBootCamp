using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class MyDbContext: DbContext
    {
        public string conn = "Server=.\\SQLEXPRESS;Database=TestDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public DbSet<Author> authors {  get; set; }
        public DbSet<Book> books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(conn);
        }
    }
}
