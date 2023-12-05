using Microsoft.EntityFrameworkCore;


namespace StudentDetails.Data
{
    public class DatabaseContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<Student> StudentInformation { get; set; }
    }
}
