namespace AwesomeMusic.Data.Model.EntityConfigurations
{
    using AwesomeMusic.Data.Model.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.HasKey(e => new { e.Id });
            builder.Property(e => e.RegisteredById).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(200);
            builder.Property(e => e.Artist).HasMaxLength(200);
            builder.Property(e => e.Album).HasMaxLength(200);
            builder.Property(e => e.Url).HasMaxLength(600);
            builder.Property(e => e.CoverUrl).HasMaxLength(600);
            builder.Property(e => e.IsPublic).IsRequired();

            builder
                .HasOne(c => c.User)
                .WithMany(e => e.Songs)
                .HasForeignKey(s => s.RegisteredById);
        }
    }
}
