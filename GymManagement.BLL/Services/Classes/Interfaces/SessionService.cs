using AutoMapper;
using GymManagement.BLL.Common;
using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;
using GymManagement.DAL.Data.Models.Enums;
using GymManagement.DAL.Resporitory.Inrerface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL.Services.Classes.Interfaces
{
    public class SessionService(IUnitOfWork unitOfWork, IMapper mapper) : ISessionService
    {

        private readonly IMapper mapper = mapper;

        public IUnitOfWork UnitOfWork { get; } = unitOfWork;

        public async Task<bool> CreateSessionAsync(CreateSessionViewModel model, CancellationToken ct)
        {
            if (model.StartDate <= model.EndDate) return false;
            if (model.StartDate <= DateTime.Now) return false;
            if (model.Capacity < 1 || model.Capacity > 25) return false;
            var trainer = await UnitOfWork.Getallrebosit<Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer == null) return false;
            var category = await UnitOfWork.Getallrebosit<Category>().GetByIdAsync(model.CategoryId);
            if (category == null) return false;

            var isvalid = Enum.TryParse<Specialties>(category.CategoryName, true, out var categeoryspeciality);
            if (!isvalid || categeoryspeciality != trainer.Specialties) return false;

            var session = mapper.Map<CreateSessionViewModel, Session>(model);
            await UnitOfWork.Getallrebosit<Session>().AddAsync(session);
            var reuslt = await UnitOfWork.SaveChangesAsync();
            return reuslt > 0;


        }

        public async Task<Result<IEnumerable<SessionViewModel>>> GetallSessionsAsync(CancellationToken ct = default)
        {
            var sessionRepo = UnitOfWork.sessionRepository;

            var sessions = await sessionRepo.GetSessionWithTrainerAndCategory(ct);

            if (sessions is null || !sessions.Any())
                return Result<IEnumerable<SessionViewModel>>.Fail("Cannot find sessions");

            var mappedSessions = sessions.Select(n => new SessionViewModel
            {
                Id = n.id,
                Capacity = n.Capacity,
                CategoryName = n.Category.CategoryName,
                Description = n.Description,
                TrainerName = n.Trainer.Name,
                EndDate = n.EndDate,
                StartDate = n.StartDate,
            }).ToList();

            foreach (var session in mappedSessions)
            {
                session.AvailableSlots =
                    session.Capacity - await sessionRepo.GetAllbokking(session.Id, ct);
            }

            return Result<IEnumerable<SessionViewModel>>.Ok(mappedSessions);
        }

        public async Task<IEnumerable<CategorySelectViews>> GetCategoryDropdownlist(CancellationToken ct = default)
        {
            var result = await UnitOfWork.Getallrebosit<Category>().GetAllAsync(ct: ct);
            return mapper.Map<IEnumerable<CategorySelectViews>>(result);
        }

        public async Task<Result<SessionViewModel>> GetSessionByid(int Sessionid, CancellationToken ct)
        {
            //connect repo and then map
            var session = await UnitOfWork.sessionRepository.GetSessionbyIdAsync(Sessionid, ct);
            if (session is null) return Result<SessionViewModel>.Fail("Cannot Find session Details");

            else
            {

                var mapper2 = mapper.Map<Session, SessionViewModel>(session);
                mapper2.AvailableSlots = mapper2.Capacity - await UnitOfWork.sessionRepository.GetAllbokking(Sessionid, ct);

                return Result<SessionViewModel>.Ok(mapper2);

            }

        }

        public async Task<IEnumerable<TrainerSelectViews>> GetTrainerDropdownlist(CancellationToken ct)
        {
            var result = await UnitOfWork.Getallrebosit<Trainer>().GetAllAsync(ct: ct);
            return mapper.Map<IEnumerable<TrainerSelectViews>>(result);

        }


        public async Task<Result<UpdateSesssionViewModel>> GetSessionUpdate(int id, CancellationToken ct = default)
        {
            var session = await UnitOfWork.sessionRepository.GetSessionbyIdAsync(id, ct);

            if (session is null) return Result<UpdateSesssionViewModel>.Fail("No session with this name");
            if (session.StartDate <= DateTime.Now) return Result<UpdateSesssionViewModel>.Fail("Cannot Update Session that already started");
            if (session.EndDate <= DateTime.Now)
                return Result<UpdateSesssionViewModel>.Fail
                    ("Cannot Update Session that already Ended");

            var bookingslot = await UnitOfWork.sessionRepository.GetAllbokking(id, ct);

            if (bookingslot > 0)
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update booked ");

            else
            {
                var mappedsession = mapper.Map<Session, UpdateSesssionViewModel>(session);
                return Result<UpdateSesssionViewModel>.Ok(mappedsession);
            }
        }
        public async Task<Result<UpdateSesssionViewModel>> UpdateSession(int id, /*ده الي اليوزر عمله*/  UpdateSesssionViewModel model, CancellationToken ct = default)
        {
            var sessiontoupdate = await UnitOfWork.sessionRepository.GetSessionbyIdAsync(id, ct);
            if (sessiontoupdate is null) return Result<UpdateSesssionViewModel>.Fail("Cannot find Session");
            if (sessiontoupdate.StartDate <= DateTime.Now)
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update on going session");
            if (sessiontoupdate.EndDate <= DateTime.Now)
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update Ended session");
            if (model.StartDate <= DateTime.Now)
                return Result<UpdateSesssionViewModel>.Fail("Start Date must be in the future");
            if (model.EndDate <= DateTime.Now)
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update Ended session");
            var bookedsession = await UnitOfWork.sessionRepository.GetAllbokking(id, ct);

            if (bookedsession > 0)
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update booked Session");
            //trainer not matching category  i have speciality that has all trainer specialty so get the category name to compare
            var trainer = await UnitOfWork.Getallrebosit<Trainer>().GetByIdAsync(model.TrainerId);
            if (trainer is null) return Result<UpdateSesssionViewModel>.Fail("No Trainer is found for this session");

            var category = await UnitOfWork.Getallrebosit<Category>().GetByIdAsync(model.CategoryId);
            if (category is null) return Result<UpdateSesssionViewModel>.Fail("No Category is found for this session");

            //trainer and category check one enum and one strtig s use enum.tryparse,ignore cases , 
            var isValid = Enum.TryParse<Specialties>(category.CategoryName, true, out var categorySpecialites);
            if (!isValid || trainer.Specialties != categorySpecialites) return Result<UpdateSesssionViewModel>.Fail("Cannot create this session to this trainer");
            else
            {
                //map update save changes
                mapper.Map(model, sessiontoupdate);
                await UnitOfWork.sessionRepository.UpdateAsync(sessiontoupdate);
                var result = await UnitOfWork.SaveChangesAsync(ct);
                //user
                if (result > 0)
                {
                    var resultviewmodel = mapper.Map<UpdateSesssionViewModel>(sessiontoupdate);

                    return Result<UpdateSesssionViewModel>.Ok(resultviewmodel);

                }
                return Result<UpdateSesssionViewModel>.Fail("Cannot Update");

            }

        }

        public async Task<Result<bool>> DeleteSession(int id, CancellationToken ct = default)
        {
            var sessiontodelete = await UnitOfWork.sessionRepository.GetSessionbyIdAsync(id);
            if (sessiontodelete is null) return Result<bool>.Fail("Cannot Find the session");
            if (sessiontodelete.EndDate <= DateTime.Now && sessiontodelete.StartDate >= DateTime.Now)

                return Result<bool>.Fail("Cannot Delete Ongoing Session");
            var bookedsession = await UnitOfWork.sessionRepository.GetAllbokking(id, ct);
            if (bookedsession < 0) return Result<bool>.Fail($"There are{bookedsession.ToString()} booked");


            else
            {
                //delete save row affected in save
                await UnitOfWork.sessionRepository.DeleteAsync(sessiontodelete);
                var result = await UnitOfWork.SaveChangesAsync(ct);

                return result > 0 ? Result<bool>.Ok(true) : Result<bool>.Fail("cannot delete data");
            }

        }



    }
}
