using AutoMapper;
using WebApplication.Core.Domain;
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

