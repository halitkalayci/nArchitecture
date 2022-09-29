using Application.Features.Models.Dtos;
using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetModelListWithBrand
{
    public class GetModelListWithBrandQuery : IRequest<ModelListWithBrandModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetModelListWithBrandQueryHandler : IRequestHandler<GetModelListWithBrandQuery, ModelListWithBrandModel>
        {
            private IModelRepository _modelRepository;
            private IMapper _mapper;

            public GetModelListWithBrandQueryHandler(IModelRepository modelRepository, IMapper mapper)
            {
                _modelRepository = modelRepository;
                _mapper = mapper;
            }

            public async Task<ModelListWithBrandModel> Handle(GetModelListWithBrandQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Model> models = await _modelRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize,include:i =>i.Include(i=>i.Brand));
                ModelListWithBrandModel modelList = _mapper.Map<ModelListWithBrandModel>(models);
                return modelList;
            }
        }
    }
}
