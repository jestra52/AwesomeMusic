namespace AwesomeMusic.Data.Model
{
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Model.Entities;
    using Microsoft.EntityFrameworkCore;

    public class AwesomeMusicContext : DbContext, IAwesomeMusicContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }

        public AwesomeMusicContext(DbContextOptions<AwesomeMusicContext> options) : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyAllConfigurations<AwesomeMusicContext>();
        }

        public Task SaveAsync(CancellationToken cancellationToken = default)
            => SaveChangesAsync(cancellationToken);

        public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
            => Database.CanConnectAsync(cancellationToken);
    }
}
