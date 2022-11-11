using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZadanieCSV.Models
{
    public class dataContext : DbContext
    {
        public dataContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<document> documents { get; set; }
        public DbSet<item> items { get; set; }

        public string path = "data.db";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={path}");
    }
}
