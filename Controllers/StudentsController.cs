using Microsoft.AspNetCore.Mvc;
using MyWebApi.Models;       
using MyWebApi.Services;     
using System.Globalization;

namespace StudentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly StudentRepository _repository;

        public StudentsController(StudentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var students = _repository.GetAllStudents();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(long id)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpGet("search")]
        public ActionResult<IEnumerable<Student>> GetStudentsByName([FromQuery] string name)
        {
            var students = _repository.GetStudentsByName(name);
            return Ok(students);
        }

        [HttpGet("date")]
        public ActionResult<string> GetDateByCulture([FromHeader(Name = "Accept-Language")] string language)
        {
            var culture = new CultureInfo(language);
            var formattedDate = DateTime.Now.ToString("D", culture);
            return Ok(formattedDate);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(long id, [FromBody] StudentUpdateDto updateDto)
        {
            var student = _repository.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }

            _repository.UpdateStudent(id, updateDto.FirstName, updateDto.Email);
            return NoContent();
        }

        public class StudentUpdateDto
        {
            public string FirstName { get; set; }
            public string Email { get; set; }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            var filePath = Path.Combine("wwwroot/images", file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { filePath });
        }


    }
}
