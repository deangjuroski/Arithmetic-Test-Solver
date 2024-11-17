
using Microsoft.EntityFrameworkCore;
using StudentMathTestSystem.Interfaces;
using StudentMathTestSystem.Models;
using System.Xml.Linq;

namespace StudentMathTestSystem.Utilities
{
    public class XmlHelper : IXmlHelper
    {
        private readonly AppDbContext _context;
        public XmlHelper(AppDbContext context)
        {
            _context = context;
        }

        public async void ParseXml(string xmlContent)
        {
            var xDocument = XDocument.Parse(xmlContent);

            var teacherId = int.Parse(xDocument.Root.Attribute("ID").Value);
            var teacher = _context.Teachers
                .Include(t => t.Students)
                .ThenInclude(s => s.Exams)
                .ThenInclude(e => e.Tasks)
                .FirstOrDefault(t => t.TeacherID == teacherId) ?? new Teacher { TeacherID = teacherId };

            foreach (var studentElement in xDocument.Descendants("Student"))
            {
                var studentId = int.Parse(studentElement.Attribute("ID").Value);
                var student = teacher.Students.FirstOrDefault(s => s.StudentID == studentId)
                              ?? new Student { StudentID = studentId, TeacherID = teacher.TeacherID, Teacher = teacher };

                if (!teacher.Students.Any(s => s.StudentID == studentId))
                {
                    teacher.Students.Add(student);
                }

                foreach (var examElement in studentElement.Descendants("Exam"))
                {
                    var examId = int.Parse(examElement.Attribute("Id").Value);
                    var exam = student.Exams.FirstOrDefault(e => e.ExamID == examId)
                               ?? new Exam { ExamID = examId, StudentID = student.StudentID, Student = student };

                    if (!student.Exams.Any(e => e.ExamID == examId))
                    {
                        student.Exams.Add(exam);
                    }

                    foreach (var taskElement in examElement.Descendants("Task"))
                    {
                        var taskId = int.Parse(taskElement.Attribute("id").Value);
                        if (!exam.Tasks.Any(t => t.ExamTaskID == taskId))
                        {
                            var taskParts = taskElement.Value.Split('=');
                            var taskExpression = taskParts[0].Trim();
                            var studentResult = double.Parse(taskParts[1].Trim());
                            var correctResult = EvaluateMathExpression(taskExpression);

                            var task = new ExamTask
                            {
                                ExamTaskID = taskId,
                                Expression = taskExpression,
                                ExpectedResult = correctResult,
                                StudentResult = studentResult,
                                IsCorrect = correctResult == studentResult,
                                ExamID = exam.ExamID,
                                Exam = exam
                            };

                            exam.Tasks.Add(task);
                        }
                    }
                }
            }
            if (teacher.ID == 0) { 
                await _context.AddAsync(teacher); 
            }
        }

        private double EvaluateMathExpression(string expression)
        {
            var dataTable = new System.Data.DataTable();
            return Convert.ToDouble(dataTable.Compute(expression, string.Empty));
        }
    }
}
