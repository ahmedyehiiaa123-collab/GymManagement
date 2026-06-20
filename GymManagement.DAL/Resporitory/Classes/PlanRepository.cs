using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Resporitory.Classes
{
    public class PlanRepository : IPlanInterface
    {
        private readonly GymDbcontext dbcontext;
        public PlanRepository(GymDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
        }

        public async Task<int> AddAsync(Plan plan, CancellationToken ct = default)
        {
            dbcontext.plans.Add(plan);
            return await dbcontext.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(Plan plan, CancellationToken ct = default)
        {
            dbcontext.plans.Remove(plan);
            return await dbcontext.SaveChangesAsync(ct);
        }

        public async Task<IEnumerable<Plan>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<Plan> query = tracking ? dbcontext.plans : dbcontext.plans.AsNoTracking();
            return await query.ToListAsync();
        }

        public Task<IEnumerable<Plan>> GetAllasync(bool tracking = false, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<Plan?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await dbcontext.plans.FindAsync(id, ct);
        }

        public async Task<int> UpdateAsync(Plan plan, CancellationToken ct = default)
        {
            dbcontext.plans.Update(plan);
            return await dbcontext.SaveChangesAsync(ct);
        }
    }
}