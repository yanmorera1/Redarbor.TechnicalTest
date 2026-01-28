namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations.EFCoreConfigurations;

public class PortalConfiguration : IEntityTypeConfiguration<Portal>
{
    public void Configure(EntityTypeBuilder<Portal> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .HasConversion(
                portalId => portalId.Value,
                dbId => PortalId.Of(dbId)
            );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
