using AutoMapper;
using WebApplication.Core.UI.Entities;
using WebApplication.Core.UI.Models;

namespace FootballStats.API.Infrastructure.Profiles
{
    public class MapperProfile : Profile
    {

        public MapperProfile()
        {

            this.CreateMap<Employee, EmployeeViewModel>().ReverseMap();

        }
    }
}

