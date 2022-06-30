using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ApplicationUser = NLayer.Entity.Concrete.ApplicationUser;


namespace NLayer.DataAccess.Seeds
{
    internal class UserSeed : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
                new ApplicationUser() { Id = 1, DisplayName = "Sude", Username = "sudecom", Password = "1234" },
                new ApplicationUser { Id = 2, DisplayName = "Eylem", Username = "dataselek", Password = "6547"},
                new ApplicationUser { Id = 3, DisplayName = "Eda", Username = "eda12", Password = "1478"},
                new ApplicationUser { Id = 4, DisplayName = "Canan", Username = "cnn2", Password = "2589" }
            );
        }
    }
    
}
