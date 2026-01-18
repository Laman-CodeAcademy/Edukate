using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.CourseViewModel
{
    public class CourseCreateVM
    {
        [System.ComponentModel.DataAnnotations.Required, MaxLength(200)]
        public string Name { get; set; } = null!;
        [System.ComponentModel.DataAnnotations.Required, Range(0,5)]

        public int Rating { get; set; }

        [System.ComponentModel.DataAnnotations.Required]

        public IFormFile Image { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public int InstructorId { get; set; }
    }
}
