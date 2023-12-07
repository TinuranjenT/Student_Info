using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDetails.Data;
using StudentDetails.Model;

namespace StudentDetails.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly DatabaseContext databaseContext;
        public StudentController(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            List<Student> students = databaseContext.Students.Include(s => s.StudentAddress).ToList();
            if(students == null)
            {
                return NotFound();
            }
            return Ok(students);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Student student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }
            
        [HttpPost]
        public IActionResult Create(Student student)
        {
            databaseContext.Add(student);
            databaseContext.SaveChanges();
            return Ok(student);
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Student updatedStudent)
        {
            Student existingStudent = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);

            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Department = updatedStudent.Department;
            existingStudent.StudentAddress.Address = updatedStudent.StudentAddress.Address;

            databaseContext.SaveChanges();
            return Ok(existingStudent);
        }

        [HttpPatch("{id}")]

        public IActionResult Patch(int id, [FromBody] string Address)
        {
            var student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            student.StudentAddress.Address = Address;
            databaseContext.SaveChanges();  
            return Ok(student);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }
            if (student.StudentAddress != null)
            {
                databaseContext.StudentAddresses.Remove(student.StudentAddress);
            }
            databaseContext.Students.Remove(student);
            databaseContext.SaveChanges();

            return NoContent();
        }

        [HttpGet("search")]
        public IActionResult Search(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
            {
                return NotFound();
            }

            List<Student> matchingStudents = databaseContext.Students
                .Where(s =>
                    s.Id.ToString().StartsWith(searchItem) ||
                    s.Name.StartsWith(searchItem) ||
                    s.Department.StartsWith(searchItem) ||
                    s.StudentAddress.Address.StartsWith(searchItem))
                .Include(s => s.StudentAddress)
                .ToList();

            return Ok(matchingStudents);
        }




    }
}
