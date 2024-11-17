using System.ComponentModel.DataAnnotations;

namespace StudentMathTestSystem.Models
{
    public class Exam
    {
        [Key]
        public int ID { get; set; }
        public int ExamID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public List<ExamTask> Tasks { get; set; } = new List<ExamTask>();
    }
}
