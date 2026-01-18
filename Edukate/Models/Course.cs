using Edukate.Models.common;

namespace Edukate.Models
{
    public class Course:BaseEntity
    {
        public string  Name{ get; set; } = string.Empty;
        public string Image { get; set; }
        public int Rating { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
    }
}
