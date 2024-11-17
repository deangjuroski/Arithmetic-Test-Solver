using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentMathTestSystem.Interfaces;
using StudentMathTestSystem.Models;
using StudentMathTestSystem.Utilities;

public class TeacherController : Controller
{
    private readonly AppDbContext _context;
    private readonly IXmlValidationService _validationService;
    private readonly IXmlHelper _xmlHelper;

    public TeacherController(AppDbContext context, IXmlValidationService validationService, IXmlHelper xmlHelper)
    {
        _context = context;
        _validationService = validationService;
        _xmlHelper = xmlHelper;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> UploadXmlAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ViewBag.Message = "File not selected.";
            return View("Index");
        }

        using (var stream = new StreamReader(file.OpenReadStream()))
        {
            var xmlContent = stream.ReadToEnd();

            var validationResult = _validationService.ValidateXml(xmlContent);
            if (validationResult != "Valid")
            {
                ViewBag.Message = $"XML Validation Failed: {validationResult}";
                return View("Index");
            }
            _xmlHelper.ParseXml(xmlContent);
        }
        await _context.SaveChangesAsync();

        ViewBag.Message = "File uploaded successfully!";
        return View("Index", _context.Teachers.Include(t => t.Students).ToList());
    }

    public IActionResult ViewStudents()
    {
        var students = _context.Students
            .Include(s => s.Exams)
            .ThenInclude(e => e.Tasks)
            .ToList();

        return View(students);
    }

    public IActionResult StudentDetails(int? StudentId)
    {
        var model = new StudentDetailsViewModel();

        if (StudentId.HasValue)
        {
            var student = _context.Students
                .Include(s => s.Exams)
                .ThenInclude(e => e.Tasks)
                .FirstOrDefault(s => s.StudentID == StudentId.Value);

            if (student != null)
            {
                model.StudentId = StudentId.Value;
                model.Exams = student.Exams.ToList();
            }
        }

        return View(model);
    }
}