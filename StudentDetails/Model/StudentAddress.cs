using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDetails.Model
{
    public class StudentAddress
    {
        [Key]
        public int Id { get; set; }
        public string? Address { get; set; }

        //[ForeignKey("Student")]
        //public int StudentId { get; set; }

        //public virtual Student Student { get; set; }
    }
}
