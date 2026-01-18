using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.CourseViewModel
{
    public class CourseUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;
        [Required, Range(0, 5)]

        public int Rating { get; set; }
        [Required]

        public IFormFile? Image { get; set; }
        [Required]
        public int InstructorId { get; set; }
    }
}
