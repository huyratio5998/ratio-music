using RatioMusic.Domain.Entities;

namespace RatioMusic.Application.Abstracts
{
    public interface IUnitOfWork
    {
        Task CreateTransactionAsync();
        Task SaveAsync();
        Task CommitAsync();
        Task RollbackAsync();

        IBaseRepository<T> GetRepository<T>() where T : BaseEntity;
        ISongRepository SongRepository { get; }
        IArtistRepository ArtistRepository { get; }
    }
}
