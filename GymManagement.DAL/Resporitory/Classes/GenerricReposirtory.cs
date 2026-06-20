using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GymManagement.DAL.Resporitory.Classes
{
    public class IGenerricReposirtory<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbcontext dbcontext;
        private readonly DbSet<TEntity> _set;
        public IGenerricReposirtory(GymDbcontext dbcontext)
        {
            this.dbcontext = dbcontext;
            _set = dbcontext.Set<TEntity>();

        }
        public async Task<int> AddAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Add(entity);
            return await dbcontext.SaveChangesAsync(ct);

        }

        public async Task<int> DeleteAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Remove(entity);
            return await dbcontext.SaveChangesAsync(ct);

        }

        public async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> Predicate, CancellationToken ct = default, bool tracking = false)
        {
           IQueryable<TEntity> queryable = tracking? _set : _set.AsNoTracking();
             return await queryable.FirstOrDefaultAsync(Predicate);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = false, CancellationToken ct = default)
        {
            IQueryable<TEntity> query2 = tracking ? _set : _set.AsNoTracking();
            return await query2.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(int id, CancellationToken ct = default)
        {

            return await _set.FindAsync(id, ct);

        }  

        public async Task<int> UpdateAsync(TEntity entity, CancellationToken ct = default)
        {
            _set.Update(entity);
            return await dbcontext.SaveChangesAsync(ct);
        }
    }
}
