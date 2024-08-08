using GymManagement.Domain.Admins;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagement.Infrastructure.Admins.Persistence;

public class AdminConfigurations : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasData(new Admin(
            userId: Guid.NewGuid(),
            id: Guid.Parse("90691915-8ca1-4843-aca4-072586abf309")));
    }
}
