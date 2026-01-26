namespace Redarbor.TechnicalTest.Infrastructure.Persistence.Configurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .HasConversion(
                employeeId => employeeId.Value,
                dbId => EmployeeId.Create(dbId)
            );

        builder.HasOne<Company>()
            .WithMany()
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();

        builder.ComplexProperty(
            e => e.Email,
            nameBuilder =>
            {
                nameBuilder.Property(e => e.Value)
                    .HasColumnName(nameof(Employee.Email))
                    .HasMaxLength(50)
                    .IsRequired();
            }
        );

        builder.ComplexProperty(
            e => e.Fax,
            nameBuilder =>
            {
                nameBuilder.Property(f => f.Value)
                    .HasColumnName(nameof(Employee.Fax))
                    .HasMaxLength(15);
            }
        );

        builder.Property(e => e.Name)
            .HasMaxLength(200);

        builder.Property(e => e.LastLogin);

        builder.ComplexProperty(
            e => e.Password,
            nameBuilder =>
            {
                nameBuilder.Property(p => p.Value)
                    .HasColumnName(nameof(Employee.Password))
                    .HasMaxLength(150);
            }
        );

        builder.HasOne<Portal>()
            .WithMany()
            .HasForeignKey(e => e.PortalId)
            .IsRequired();

        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(e => e.RoleId)
            .IsRequired();

        builder.Property(e => e.StatusId);

        builder.ComplexProperty(
            e => e.Telephone,
            nameBuilder =>
            {
                nameBuilder.Property(t => t.Value)
                    .HasColumnName(nameof(Employee.Telephone))
                    .HasMaxLength(15);
            }
        );

        builder.Property(e => e.Username)
            .HasMaxLength(100);
    }
}
