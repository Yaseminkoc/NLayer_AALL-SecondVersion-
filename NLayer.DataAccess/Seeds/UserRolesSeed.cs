using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Entity.Concrete;


namespace NLayer.Repository.Seeds
{
    internal class UserRolesSeed : IEntityTypeConfiguration<ApplicationUserRoles>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRoles> builder)
        {
            builder.HasData(
                new ApplicationUserRoles() { Id = 1, UserId = 1,RoleId = 1},
                new ApplicationUserRoles() { Id = 2, UserId = 2, RoleId = 2 },
                new ApplicationUserRoles() { Id = 3, UserId = 3, RoleId = 3 },
                new ApplicationUserRoles() { Id = 4, UserId = 4, RoleId = 3 }
            );
        }
    }
}
