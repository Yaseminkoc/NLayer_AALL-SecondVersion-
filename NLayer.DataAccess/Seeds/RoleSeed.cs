using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Entity.Concrete;

namespace NLayer.Repository.Seeds
{
    internal class RoleSeed : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> builder)
        {
            builder.HasData(
                new ApplicationRole() { Id = 1, Name = "Manager" },
                new ApplicationRole() { Id = 2, Name = "Teacher" },
                new ApplicationRole() { Id = 3, Name = "Student" }
            );
        }
    }
}
