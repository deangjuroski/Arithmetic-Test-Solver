using System.ComponentModel.DataAnnotations;

namespace StudentMathTestSystem.Models
{
    public class Teacher
    {
        [Key]
        public int ID { get; set; }
        public int TeacherID { get; set; }
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
