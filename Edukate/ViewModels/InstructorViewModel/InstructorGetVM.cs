using Edukate.Models;

namespace Edukate.ViewModels.InstructorViewModel
{
    public class InstructorGetVM
    {
        public int Id{ get; set; }
        public string Name { get; set; } = null!;
        public string Position { get; set; }
    }
}
