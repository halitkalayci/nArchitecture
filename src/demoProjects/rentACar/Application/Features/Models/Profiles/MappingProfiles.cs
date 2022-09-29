using Application.Features.Models.Commands.CreateModel;
using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Model, CreateModelCommand>().ReverseMap();
            CreateMap<Model, CreatedModelDto>().ReverseMap();
            CreateMap<ModelListWithBrandDto, Model>().ReverseMap();
            CreateMap<ModelListWithBrandModel, IPaginate<Model>>().ReverseMap().ForMember(i => i.Brands, opt => opt.MapFrom(d => d.Items));
        }
    }
}
