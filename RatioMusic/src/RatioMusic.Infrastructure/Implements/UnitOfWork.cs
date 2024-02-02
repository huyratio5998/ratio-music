using Microsoft.EntityFrameworkCore.Storage;
using RatioMusic.Application.Abstracts;
using RatioMusic.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatioMusic.Infrastructure.Implements
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public RatioMusicContext _context;
        private IDbContextTransaction _transaction;
        public ISongRepository SongRepository { get; private set; }

        public IArtistRepository ArtistRepository { get; private set; }
        public UnitOfWork(RatioMusicContext context)
        {
            _context = context;
            SongRepository = new SongRepository(context);
            ArtistRepository = new ArtistRepository(context); ;
        }

        public async Task CreateTransaction()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Commit()
        {
            await _transaction?.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction?.RollbackAsync();
            _transaction?.Dispose();
            
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
