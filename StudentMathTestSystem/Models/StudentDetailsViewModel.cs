using System.Collections.Generic;

namespace StudentMathTestSystem.Models
{
    public class StudentDetailsViewModel
    {
        public int StudentId { get; set; }
        public List<Exam> Exams { get; set; }
    }
}