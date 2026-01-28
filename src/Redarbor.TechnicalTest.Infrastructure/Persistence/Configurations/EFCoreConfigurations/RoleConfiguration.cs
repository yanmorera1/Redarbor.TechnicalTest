namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations.EFCoreConfigurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(
                roleId => roleId.Value,
                dbId => RoleId.Of(dbId)
            );

        builder.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
