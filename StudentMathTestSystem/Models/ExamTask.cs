using System.ComponentModel.DataAnnotations;

namespace StudentMathTestSystem.Models
{
    public class ExamTask
    {
        [Key]
        public int ID { get; set; }
        public int ExamTaskID { get; set; }
        public string Expression { get; set; }
        public double ExpectedResult { get; set; }
        public double StudentResult { get; set; }
        public bool IsCorrect { get; set; }
        public int ExamID { get; set; }
        public Exam Exam { get; set; }
    }
}
