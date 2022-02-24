namespace AwesomeMusic.Data.Model.EntityConfigurations
{
    using AwesomeMusic.Data.Model.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.Email).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Password).IsRequired().HasMaxLength(200);
            builder.Property(e => e.Name).HasMaxLength(200);

            builder
                .HasMany(c => c.Songs)
                .WithOne(e => e.User)
                .HasForeignKey(s => s.RegisteredById);
        }
    }
}
