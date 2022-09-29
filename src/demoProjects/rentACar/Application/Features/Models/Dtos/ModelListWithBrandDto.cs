using Application.Features.Brands.Dtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Dtos
{
    public class ModelListWithBrandDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public BrandListDto? Brand { get; set; }
    }
}
