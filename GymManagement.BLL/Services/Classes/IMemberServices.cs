using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;

namespace GymManagement.BLL.Services.Classes
{
    public interface IMemberServices
    {
        public  Task<MemberViewModel> GetAllAsync(CancellationToken ct =  default);
        Task<bool> CreateMemberAsync(CreateMemberView Createmember , CancellationToken ct = default);

    }
}