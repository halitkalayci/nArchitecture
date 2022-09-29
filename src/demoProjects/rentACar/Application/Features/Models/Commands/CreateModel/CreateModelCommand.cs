using Application.Features.Models.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Commands.CreateModel
{
    public class CreateModelCommand : IRequest<CreatedModelDto>
    {
        public string Name { get; set; }
        public int BrandId { get; set; }

        public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, CreatedModelDto>
        {
            private IMapper _mapper;
            private IModelRepository _modelRepository;

            public CreateModelCommandHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<CreatedModelDto> Handle(CreateModelCommand request, CancellationToken cancellationToken)
            {
                Model modelToCreate = _mapper.Map<Model>(request);
                Model createdModel = await _modelRepository.AddAsync(modelToCreate);
                CreatedModelDto createdModelDto = _mapper.Map<CreatedModelDto>(createdModel);
                return createdModelDto;
            }
        }
    }
}
