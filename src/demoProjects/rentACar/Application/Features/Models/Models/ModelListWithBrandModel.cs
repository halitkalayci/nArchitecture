using Application.Features.Models.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Models
{
    public class ModelListWithBrandModel : BasePageableModel
    {
        public List<ModelListWithBrandDto> Brands { get; set; }
    }
}
