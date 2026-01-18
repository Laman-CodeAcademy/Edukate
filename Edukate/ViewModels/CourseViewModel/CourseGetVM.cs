using Edukate.Models;

namespace Edukate.ViewModels.CourseViewModel
{
    public class CourseGetVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Rating { get; set; }
        public string Image { get; set; }
        public string InstructorName { get; set; }
    }
}
