using Edukate.Models.common;

namespace Edukate.Models
{
    public class Instructor:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Position { get; set; }
        public string Image { get; set; }
        public ICollection<Course> Courses { get; set; } = [];
    }
}
