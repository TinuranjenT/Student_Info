using Microsoft.EntityFrameworkCore;
using StudentDetails.Model;

namespace StudentDetails.Data
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Student> Student_Information { get; set; }
        public DbSet<StudentAddress> Student_Address_Info { get; set; }
    }
}
