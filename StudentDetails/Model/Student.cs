using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentDetails.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Department { get; set; }

        [ForeignKey(nameof(StudentAddress))]
        public int StudentAddressId {  get; set; }
        public virtual StudentAddress? StudentAddress { get; set; }            //reference navigation property
        //public string Description { get; set; }
    }
}
