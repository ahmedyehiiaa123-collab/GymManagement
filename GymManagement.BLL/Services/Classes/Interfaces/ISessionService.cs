using GymManagement.BLL.Common;
using GymManagement.BLL.Services.Classes.Interfaces;
using GymManagement.BLL.ViewModels;

public interface ISessionService
{
    Task<bool> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct = default);

    Task<Result<IEnumerable<SessionViewModel>>> GetallSessionsAsync(CancellationToken ct = default);

    Task<Result<SessionViewModel>> GetSessionByid(int sessionId, CancellationToken ct = default);

    Task<IEnumerable<CategorySelectViews>> GetCategoryDropdownlist(CancellationToken ct = default);

    Task<IEnumerable<TrainerSelectViews>> GetTrainerDropdownlist(CancellationToken ct = default);

    Task<Result<UpdateSesssionViewModel>> GetSessionUpdate(int id, CancellationToken ct = default);

    Task<Result<UpdateSesssionViewModel>> UpdateSession(int id, UpdateSesssionViewModel model, CancellationToken ct = default);

    Task<Result<bool>> DeleteSession(int id, CancellationToken ct = default);
}