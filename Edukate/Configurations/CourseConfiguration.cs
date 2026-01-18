using Edukate.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Edukate.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Rating).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Image).IsRequired().HasMaxLength(1024);

            builder.HasOne(x=>x.Instructor).WithMany(x=>x.Courses).HasForeignKey(x=>x.InstructorId).HasPrincipalKey(x=>x.Id);

        }
    }
}
