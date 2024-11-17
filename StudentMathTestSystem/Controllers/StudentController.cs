using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace StudentMathTestSystem.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        // Display student analytics
        public IActionResult Results(int studentId)
        {
            var student = _context.Students
                .Include(s => s.Exams)
                .ThenInclude(e => e.Tasks)
                .FirstOrDefault(s => s.ID == studentId);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            return View(student);
        }

    }
}