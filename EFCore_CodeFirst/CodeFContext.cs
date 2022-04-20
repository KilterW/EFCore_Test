using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_CodeFirst
{
    public class CodeFContext : DbContext
    {
        //定义实体
        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Comment> Comments { get; set; } = null!;
        private string ConnectStr=null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration config = new ConfigurationBuilder()
                     .SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json")
                     .Build();
                ConnectStr = config.GetConnectionString("MysqlDbConn_CodeFirst");
                optionsBuilder.UseMySql(ConnectStr, ServerVersion.AutoDetect(ConnectStr));
                optionsBuilder.UseBatchEF_MySQLPomelo();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
