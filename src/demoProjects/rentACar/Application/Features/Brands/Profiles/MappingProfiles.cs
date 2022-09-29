using Application.Features.Brands.Commands.CreateBrand;
using Application.Features.Brands.Dtos;
using Application.Features.Brands.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateBrandCommand, Brand>().ReverseMap();
            CreateMap<Brand, CreatedBrandDto>().ReverseMap();
            CreateMap<Brand, BrandListDto>().ReverseMap();
            CreateMap<BrandListModel,IPaginate<Brand>>().ReverseMap().ForMember(i=>i.Brands,opt=>opt.MapFrom(c=>c.Items));
        }
    }
}
