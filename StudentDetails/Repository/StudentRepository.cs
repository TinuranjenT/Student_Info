using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDetails.Data;
using StudentDetails.Model;

namespace StudentDetails.Repository
{
    public class StudentRepository
    {
        private readonly DatabaseContext databaseContext;
        public StudentRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        public List<Student> GetStudents()
        {
            return databaseContext.Students.Include(s => s.StudentAddress).ToList();
        }

        public Student GetStudentById(int id) 
        {
            return databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
        }

        public void CreateStudent(Student student)
        {
            databaseContext.Add(student);
            databaseContext.SaveChanges();
        }

        public void UpdateStudent(int id, Student updatedStudent)
        {
            Student existingStudent = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            if (existingStudent != null)
            {
                existingStudent.Name = updatedStudent.Name;
                existingStudent.Department = updatedStudent.Department;
                existingStudent.StudentAddress.Address = updatedStudent.StudentAddress.Address;

                databaseContext.SaveChanges();
            }
            
        }

        public void PatchStudent(int id, Student student)
        {
            var existingStudent = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            if (existingStudent.Name != student.Name && student.Name != null)
            {
                existingStudent.Name = student.Name;
            }
            if (existingStudent.Department != student.Department && student.Department != null)
            {
                existingStudent.Department = student.Department;
            }
            if (existingStudent.StudentAddress != student.StudentAddress && student.StudentAddress != null)
            {
                existingStudent.StudentAddress = student.StudentAddress;
            }
            databaseContext.SaveChanges();
        }

        public void DeleteStudent(int id)
        {
            Student student = databaseContext.Students.Include(s => s.StudentAddress).FirstOrDefault(s => s.Id == id);
            if (student.StudentAddress != null)
            {
                databaseContext.StudentAddresses.Remove(student.StudentAddress);
            }
            databaseContext.Students.Remove(student);
            databaseContext.SaveChanges();
        }

        public List<Student> searchStudents(string searchItem)
        {
            return databaseContext.Students
                .Where(s =>
                    s.Name.Contains(searchItem) ||
                    s.Department.Contains(searchItem) ||
                    s.StudentAddress.Address.Contains(searchItem))
                 .OrderBy(s => s.Name)
                 .ThenBy(s => s.Department)
                 .ThenBy(s => s.StudentAddress.Address)
                .Include(s => s.StudentAddress)
                .ToList();
        }
        

        public Dictionary<string, List<Student>> GroupByDepartment()
        {
            var groupedData = databaseContext.Students
                .Include(s => s.StudentAddress)
                .GroupBy(s => s.Department)
                .ToDictionary(
                    group => group.Key,
                    group => group.Select(student => new Student
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Department = student.Department,
                        StudentAddressId = student.StudentAddressId,
                        StudentAddress = new StudentAddress
                        {
                            Id = student.StudentAddress.Id,
                            Address = student.StudentAddress.Address
                        }
                    }).ToList()
                );

            return groupedData;
        }


            
    }
}
