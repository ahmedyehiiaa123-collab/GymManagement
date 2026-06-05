using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Resporitory.Inrerface
{
    public interface IPlanInterface
    {
        Task<IEnumerable<Plan>> GetAllasync(bool tracking = false, CancellationToken ct = default);
        Task <Plan>  GetByIdAsync(int id , CancellationToken ct = default);
         Task<int>   AddAsync(Plan plan, CancellationToken ct = default);
        Task <int> UpdateAsync(Plan plan, CancellationToken ct = default);
        Task<int> DeleteAsync (Plan plan, CancellationToken ct = default);

    }
}
