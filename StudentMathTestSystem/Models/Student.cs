using System.ComponentModel.DataAnnotations;

namespace StudentMathTestSystem.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }
        public int StudentID { get; set; }
        public int TeacherID { get; set; }
        public Teacher Teacher { get; set; }
        public List<Exam> Exams { get; set; } = new List<Exam>();
    }
}
