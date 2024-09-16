using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using WebAppForAPITest.Services.Interfaces;

namespace WebAppForAPITest.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _service;

        public StudentController(IStudentService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<IActionResult> Student()
        {
            //var students = await _service.Find();
            var students = await _service.GetAllStudents();
            return View(students);
        }

        public async Task<IActionResult> GetStudentByName()
        {
            var students = await _service.GetByName();
            return View(students);
        }

        public async Task<IActionResult> AddStudent()
        {
            var students = await _service.AddStudent();
            return RedirectToAction("GetStudentByName", students);
        }
    }
}
