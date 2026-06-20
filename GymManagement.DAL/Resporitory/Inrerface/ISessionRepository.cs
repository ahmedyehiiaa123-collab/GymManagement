using GymManagement.DAL.Data.Models;

namespace GymManagement.DAL.Resporitory.Inrerface
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        Task<IEnumerable<Session>> GetSessionWithTrainerAndCategory(CancellationToken ct = default);

        Task<Session?> GetSessionbyIdAsync(int id, CancellationToken ct = default);

        Task<int> GetAllbokking(int sessionId, CancellationToken ct = default);
    }
}