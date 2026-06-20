using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Classes;
using GymManagement.DAL.Resporitory.Inrerface;

namespace GymManagement.BLL.Services.Classes.Interfaces
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GymDbcontext _dbContext;
        private readonly Dictionary<string, object> _repositories = new();

        public ISessionRepository sessionRepository { get; }

        public UnitOfWork(GymDbcontext dbContext, ISessionRepository sessionRepo)
        {
            _dbContext = dbContext;
            sessionRepository = sessionRepo;
        }

        public IGenericRepository<TEntity> Getallrebosit<TEntity>()
            where TEntity : BaseEntity, new()
        {
            var entityName = typeof(TEntity).Name;

            if (_repositories.TryGetValue(entityName, out var repository))
                return (IGenericRepository<TEntity>)repository;

            var newRepository = new IGenerricReposirtory<TEntity>(_dbContext);

            _repositories[entityName] = newRepository;

            return newRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _dbContext.SaveChangesAsync(ct);
        }
    }
}