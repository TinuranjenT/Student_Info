using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDetails.Model
{
    public class StudentAddress
    {
        [Key,ForeignKey("Student")]
        public int AddressId { get; set; }
        public string Address { get; set; }
        public virtual Student Student { get; set; }
    }
}
