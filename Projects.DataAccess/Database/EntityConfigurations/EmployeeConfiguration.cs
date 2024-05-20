using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projects.DataAccess.Models;

namespace Projects.DataAccess.Database.EntityConfigurations;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FirstName).HasMaxLength(256);
        builder.Property(e => e.MiddleName).HasMaxLength(256);
        builder.Property(e => e.LastName).HasMaxLength(256);
    }
}