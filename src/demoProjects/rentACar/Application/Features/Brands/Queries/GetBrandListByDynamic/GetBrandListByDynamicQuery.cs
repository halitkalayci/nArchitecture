using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetBrandListByDynamic
{
    public class GetBrandListByDynamicQuery : IRequest<BrandListModel>
    {
        public PageRequest PageRequest { get; set; }

        public Dynamic Dynamic { get; set; }

        public class GetBrandListByDynamicQueryHandler : IRequestHandler<GetBrandListByDynamicQuery, BrandListModel>
        {
            private IBrandRepository _brandRepository;
            private IMapper _mapper;


            public GetBrandListByDynamicQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<BrandListModel> Handle(GetBrandListByDynamicQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListByDynamicAsync(index:request.PageRequest.Page,
                                                               size:request.PageRequest.PageSize,
                                                               dynamic:request.Dynamic
                                                               );
                BrandListModel model = _mapper.Map<BrandListModel>(brands);
                return model;
            }
        }
    }
}
