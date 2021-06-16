using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace reifen.Models
{
    public class ReifenContext : DbContext
    {
        public ReifenContext(DbContextOptions<ReifenContext> options):base(options)
        {
          
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(
            //   @"Server=server.kita365.de;Database=reifenozdb;User Id=reifenozdb;Password=cngwmqfjivu0tpehdyok;");
            ////optionsBuilder.UseSqlServer(
            //   @"Server=server.kita365.de;Database=reifendb;User Id=reifenusr;Password=kgwvpzfaed3xlbhcjstm;");
            optionsBuilder.UseSqlServer(
            @"Server=DESKTOP-HU40FCR\SQLSERVER;Database=ReifenDatabase;User Id=sa;Password=Mehmet123?;");
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Reminder> Reminders { get; set; }


    }
}
