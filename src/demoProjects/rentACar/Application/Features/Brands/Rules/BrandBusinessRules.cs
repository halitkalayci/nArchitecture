using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Rules
{
    public class BrandBusinessRules
    {
        private readonly IBrandRepository _brandRepository;

        public BrandBusinessRules(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public async Task BrandWithSameNameShouldNotExists(string name)
        {
            Brand brandWithSameName = await _brandRepository.GetAsync(b => b.Name == name);
            if (brandWithSameName != null)
                throw new BusinessException(Messages.BrandWithSameNameExists);
        }
    }
}
