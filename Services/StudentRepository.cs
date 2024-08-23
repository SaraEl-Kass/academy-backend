using MyWebApi.Models;

namespace MyWebApi.Services
{
    public class StudentRepository
    {
        private readonly List<Student> _students;

        public StudentRepository()
        {
            _students = new List<Student>
            {
                new Student { Id = 1, FirstName = "John", LastName = "Doe", FatherName = "James", MotherName = "Jane", Email = "john.doe@example.com", Age = 20 },
                new Student { Id = 2, FirstName = "Anna", LastName = "Smith", FatherName = "Robert", MotherName = "Sara", Email = "anna.smith@example.com", Age = 22 },
                new Student { Id = 2, FirstName = "Maria", LastName = "Smith", FatherName = "Robert", MotherName = "Sara", Email = "maria.smith@example.com", Age = 23 },
                new Student { Id = 2, FirstName = "Rona", LastName = "Elias", FatherName = "Nadra", MotherName = "Nada", Email = "rona.elias@example.com", Age = 21 },
                new Student { Id = 2, FirstName = "Sarah", LastName = "El-Kass", FatherName = "Houssam", MotherName = "Nada", Email = "sarah.el-kass@example.com", Age = 21 },
                new Student { Id = 2, FirstName = "Cyrine", LastName = "El-Kass", FatherName = "Houssam", MotherName = "Nada", Email = "cyrine.el-kass@example.com", Age = 13 },
            };
        }

        public List<Student> GetAllStudents() => _students;

        public Student GetStudentById(long id) => _students.FirstOrDefault(s => s.Id == id);

        public List<Student> GetStudentsByName(string name) =>
            _students.Where(s => s.FirstName.Contains(name, StringComparison.OrdinalIgnoreCase) ||
                                 s.LastName.Contains(name, StringComparison.OrdinalIgnoreCase)).ToList();

        public void UpdateStudent(long id, string newName, string newEmail)
        {
            var student = GetStudentById(id);
            if (student != null)
            {
                student.FirstName = newName;
                student.Email = newEmail;
            }
        }
    }
}

