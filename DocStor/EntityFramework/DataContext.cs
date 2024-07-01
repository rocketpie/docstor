using DocStor.Models;
using Microsoft.EntityFrameworkCore;

namespace DocStor.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }

        //private readonly string _dbFilePath;

        //public DataContext(string dbFilePath)
        //{
        //    ArgumentOutOfRangeException.ThrowIfNullOrWhiteSpace(nameof(dbFilePath));

        //    var parentPath = Path.GetDirectoryName(dbFilePath);
        //    ArgumentOutOfRangeException.ThrowIfNullOrWhiteSpace(parentPath, "78a8e9");
        //    if (!Path.Exists(parentPath)) throw new Exception("4519ea");

        //    Database.EnsureCreated();
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite($"Data Source={_dbFilePath}");
        //}

        public DbSet<DDocument> Documents { get; set; }
        public DbSet<DFile> DocumentFiles { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
