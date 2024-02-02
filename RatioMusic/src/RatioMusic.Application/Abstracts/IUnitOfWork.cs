namespace RatioMusic.Application.Abstracts
{
    public interface IUnitOfWork
    {
        Task CreateTransaction();
        Task Save();
        Task Commit();
        Task Rollback();
        ISongRepository SongRepository { get; }
        IArtistRepository ArtistRepository { get; }
    }
}
