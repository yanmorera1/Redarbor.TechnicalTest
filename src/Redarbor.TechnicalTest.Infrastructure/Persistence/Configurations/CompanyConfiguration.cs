namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(
                companyId => companyId.Value,
                dbId => CompanyId.Create(dbId)
            );

        builder.Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();
    }
}
