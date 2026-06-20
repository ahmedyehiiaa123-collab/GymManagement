using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.Services.Classes.Interfaces;
using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;

namespace GymManagement.BLL.Services.Classes
{
    public class MemeberService : IMemberServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemeberService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateMemberAsync(CreateMemberView createMember, CancellationToken ct = default)
        {
            var member = _mapper.Map<Member>(createMember);

            await _unitOfWork.Getallrebosit<Member>().AddAsync(member, ct);

            var result = await _unitOfWork.SaveChangesAsync(ct);

            return result > 0;
        }

        public Task<MemberViewModel> GetAllAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var members = await _unitOfWork.Getallrebosit<Member>().GetAllAsync(ct: ct);

            if (!members.Any())
                return [];

            var memberViewModels = _mapper.Map<IEnumerable<MemberViewModel>>(members);

            return memberViewModels;
        }

        public async Task<MemberViewModel?> GetMemberDetailsByIdAsync(int memberId, CancellationToken ct = default)
        {
            var member = await _unitOfWork.Getallrebosit<Member>().GetByIdAsync(memberId, ct);

            if (member is null)
                return null;

            var model = _mapper.Map<MemberViewModel>(member);

            return model;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int memberId, CancellationToken ct = default)
        {
            var healthRecord = await _unitOfWork.Getallrebosit<HealthRecord>()
                .FirstOrDefaultAsync(x => x.MemberId == memberId, ct);

            if (healthRecord is null)
                return null;

            return new HealthRecordViewModel
            {
                Weight = healthRecord.Weight,
                Height = healthRecord.Height,
                BloodType = healthRecord.BloodType,
                Note = healthRecord.Note
            };
        }
    }
}