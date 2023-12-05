using System.ComponentModel.DataAnnotations;

namespace StudentDetails.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public virtual StudentAddress StudentAddress { get; set; }
        //public string Description { get; set; }
    }
}
