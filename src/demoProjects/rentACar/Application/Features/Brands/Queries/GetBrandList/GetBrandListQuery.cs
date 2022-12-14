using Application.Features.Brands.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Performance;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Queries.GetBrandList
{
    public class GetBrandListQuery : IRequest<BrandListModel>,ICachableRequest,IPerformanceRequest
    {
        public PageRequest PageRequest { get; set; }

        public bool BypassCache { get; set; }

        public string CacheKey => "GetBrandListQuery";

        public TimeSpan? SlidingExpiration { get; set; }

        public class GetBrandListQueryHandler : IRequestHandler<GetBrandListQuery, BrandListModel>
        {
            private IBrandRepository _brandRepository;
            private IMapper _mapper;

            public GetBrandListQueryHandler(IBrandRepository brandRepository, IMapper mapper)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
            }

            public async Task<BrandListModel> Handle(GetBrandListQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Brand> brands = await _brandRepository.GetListAsync(index:request.PageRequest.Page,size:request.PageRequest.PageSize);
                BrandListModel model = _mapper.Map<BrandListModel>(brands);
                return model;
            }
        }
    }
}
