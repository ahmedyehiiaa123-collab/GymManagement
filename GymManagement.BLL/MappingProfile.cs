using AutoMapper;
using GymManagement.BLL.Services.Classes.Interfaces;
using GymManagement.BLL.ViewModels;
using GymManagement.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagement.BLL
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        }

      public void MemberMap()
{
    CreateMap<Member, MemberViewModel>()
        .ForMember(dest => dest.DateofBirth,
            opt => opt.MapFrom(src => src.DateofBirth.ToString("yyyy-MM-dd")))
        .ForMember(dest => dest.Adress,
            opt => opt.MapFrom(src =>
                src.Address != null
                    ? $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}"
                    : string.Empty
            ));
}


        public void SessionMap()
        {
            CreateMap<Session, CreateSessionViewModel>();
            CreateMap<Trainer, TrainerSelectViews>();
            CreateMap<Session, SessionViewModel>().
                ForMember(dest => dest.TrainerName, opt => opt.MapFrom(src => src.Trainer.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName));
            
            CreateMap<Session,UpdateSesssionViewModel>().ReverseMap();
        }

    }
    }

