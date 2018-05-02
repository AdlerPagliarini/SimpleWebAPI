using AutoMapper;
using SimpleWebApi.DTO;
using SimpleWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleWebApi.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<JsonString, JsonStringDto>();
            Mapper.CreateMap<JsonStringDto, JsonString>();
            // Dto to Domain
            Mapper.CreateMap<JsonStringDto, JsonString>()
                .ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}