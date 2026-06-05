using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Resporitory.Inrerface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Services.Classes
{
    public class MemeberService : IMemberServices
    {
        private readonly IGenericRepository<Member> memberRepositort;

        public MemeberService(IGenericRepository<Member> memberRepositort)

        {
            this.memberRepositort = memberRepositort;
        }

        public Task<bool> CreateMemberAsync(CreateMemberView Createmember, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<MemberViewModel> GetAllAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var member = await memberRepositort.GetAllAsync(ct: ct);
            if (!member.Any()) return [];
            var memberviewmodel = member.Select(m => new MemberViewModel()
            {
                Gender = m.Gender.ToString(),
                Name = m.Name,
                Email = m.Email,
                Photo = m.photo,
                Phone = m.Phone,
                Id = m.id,

            });  
            return memberviewmodel;
        }
    }  
}
