using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;

namespace GymManagement.BLL.Services.Classes
{
    public interface IMemberServices
    {
        Task<MemberViewModel> GetAllAsync(CancellationToken ct =  default);
        Task<bool> CreateMemberAsync(CreateMemberView Createmember , CancellationToken ct = default);
        Task<MemberViewModel>GetMemberDetailsByIdAsync(int MemberId , CancellationToken ct = default);
        Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int MemberId, CancellationToken ct = default);

    }
}