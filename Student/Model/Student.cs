using System.ComponentModel.DataAnnotations;

namespace Student.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
