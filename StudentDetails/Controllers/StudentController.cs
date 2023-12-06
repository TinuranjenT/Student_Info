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
            //List<Student>students = new List<Student>();
            //students = databaseContext.Students.ToList();
            return Ok(students);
            
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            //Student student = databaseContext.Students.FirstOrDefault(x => x.Id == id);
            //return Ok(student);

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

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            // Remove the related StudentAddress entity
            if (student.StudentAddress != null)
            {
                databaseContext.StudentAddresses.Remove(student.StudentAddress);
            }

            // Remove the Student entity
            databaseContext.Students.Remove(student);
            databaseContext.SaveChanges();

            return NoContent();
        }

        //[HttpPut("{id}")]
        //public IActionResult Update(int id, [FromBody] Student updatedStudent)
        //{
        //    Student existingStudent = databaseContext.Students.Find(id);

        //    if (existingStudent == null)
        //    {
        //        return NotFound();
        //    }

        //    existingStudent.Name = updatedStudent.Name;
        //    existingStudent.Department = updatedStudent.Department;

        //    databaseContext.SaveChanges();

        //    return Ok(existingStudent);
        //}





    }
}
