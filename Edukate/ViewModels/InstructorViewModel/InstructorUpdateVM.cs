using System.ComponentModel.DataAnnotations;

namespace Edukate.ViewModels.InstructorViewModel
{
    public class InstructorUpdateVM
    {
        public int Id { get; set; }
        [Required, MaxLength(200)]
        public string Name { get; set; }

        [Required, MaxLength(200)]
        public string Position { get; set; }
    }
}
