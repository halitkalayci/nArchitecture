using Core.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Extensions
{
    public static class EntityConfigurationExtensions
    {
        public static EntityTypeBuilder ConfigureBaseEntityProperties(this EntityTypeBuilder entityTypeBuilder)
        {
            Entity fakeEntity = new();
            entityTypeBuilder.Property(nameof(fakeEntity.Id)).HasColumnName("Id");
            entityTypeBuilder.Property(nameof(fakeEntity.CreatedDate)).HasColumnName("CreatedDate");
            entityTypeBuilder.Property(nameof(fakeEntity.UpdatedDate)).HasColumnName("UpdatedDate");
            return entityTypeBuilder;
        }
    }
}
