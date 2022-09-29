using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandCommand : IRequest<CreatedBrandDto>,ISecuredRequest
    {
        public string Name { get; set; }

        public string[] Roles => new string[] {""};

        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandDto>
        {
            private IBrandRepository _brandRepository;
            private IMapper _mapper;
            private BrandBusinessRules _businessRules;
            public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper, BrandBusinessRules businessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                await _businessRules.BrandWithSameNameShouldNotExists(request.Name);
                Brand brandToCreate = _mapper.Map<Brand>(request);
                Brand createdBrand = await _brandRepository.AddAsync(brandToCreate);
                CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(createdBrand);
                return createdBrandDto;
            }
        }
    }
}
