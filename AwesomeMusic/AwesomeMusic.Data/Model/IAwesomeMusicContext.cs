namespace AwesomeMusic.Data.Model
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using AwesomeMusic.Data.Model.Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IAwesomeMusicContext : IDisposable
    {
        DbSet<User> Users { get; set; }
        DbSet<Song> Songs { get; set; }

        Task SaveAsync(CancellationToken cancellationToken = default);
        Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
    }
}
