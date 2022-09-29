using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Model : Entity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public virtual Brand? Brand { get; set; }

        public Model()
        {
        }

        public Model(int id, string name, int brandId) : this()
        {
            Id = id;
            Name = name;
            BrandId = brandId;
        }
    }
}
