using GymManagement.DAL.Data.Dbcontext;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.DAL.Resporitory.Classes
{
    public class SessionRepository : IGenerricReposirtory<Session>, ISessionRepository
    {
        private readonly GymDbcontext dbcontext1;
        public SessionRepository(GymDbcontext dbcontext ) : base(dbcontext)
        {
             dbcontext1 = dbcontext;
        }

        

        public async Task<IEnumerable<Session>> GetSessionWithTrainerAndCategory(CancellationToken ct = default)
        {
            var sessionsWithTrainerandCategory = dbcontext1.Sessions.AsNoTracking().Include(s => s.Category).Include(s => s.Trainer);
            return await sessionsWithTrainerandCategory.ToListAsync(ct);
        }
        
        public async Task<Session?> GetSessionbyIdAsync(int id , CancellationToken ct)
        {
            var sesssionwithtandc = dbcontext1.Sessions.AsNoTracking().Include(x=>x.Trainer)
                .Include(x=>x.Category).FirstOrDefaultAsync(x=>x.id == id);

            return await sesssionwithtandc;
        }
       public async Task<int>GetAllbokking(int sessionId, CancellationToken ct)
        {
            return await dbcontext1.Bookings.CountAsync(s => s.SessionId == sessionId, ct);
        }
    }
}
