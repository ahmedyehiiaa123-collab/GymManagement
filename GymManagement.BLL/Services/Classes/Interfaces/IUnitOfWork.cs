using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;

namespace GymManagement.BLL.Services.Classes.Interfaces
{
    public interface IUnitOfWork
    {
        ISessionRepository sessionRepository { get; }

        IGenericRepository<TEntity> Getallrebosit<TEntity>()
            where TEntity : BaseEntity, new();

        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}