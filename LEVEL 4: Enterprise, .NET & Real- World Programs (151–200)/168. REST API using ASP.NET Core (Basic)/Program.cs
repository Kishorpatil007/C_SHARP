using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DemoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public List<string> GetStudents()
        {
            return new List<string> { "Ram", "Shyam", "Sita" };
        }

        [HttpGet("{id}")]
        public string GetStudent(int id)
        {
            return "Student ID: " + id;
        }
    }
}