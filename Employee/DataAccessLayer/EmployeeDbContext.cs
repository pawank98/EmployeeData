using EmployeeData.Models.DbModels;
using Microsoft.EntityFrameworkCore; 

namespace EmployeeData.DataAccessLayer
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options) : base(options)
        {
        }
        //communication with database

        public DbSet<Employees> employees { get; set; }
        //public DbSet<Employees> MyProperty { get; set; }

    }
}
