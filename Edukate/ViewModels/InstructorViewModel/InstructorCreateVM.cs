using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.InstructorViewModel
{
    public class InstructorCreateVM
    {
        [Required,MaxLength(200)]
        public string Name { get; set; }
        [Required, MaxLength(200)]

        public string Position { get; set; }
    }
}
