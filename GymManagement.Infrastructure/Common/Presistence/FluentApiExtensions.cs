using GymManagement.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GymManagement.Infrastructure.Common.Presistence
{
    public static class FluentApiExtensions
    {
        // SQLite doesn't support JSON columns. Otherwise, we'd prefer calling .ToJson() on the owned entity instead.

        public static PropertyBuilder<T> HasValueJsonConverter<T>(this PropertyBuilder<T> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                new ValueJsonConverter<T>(),
                new ValueJsonComparer<T>());
        }

        public static PropertyBuilder<T> HasListOfIdsConverter<T>(this PropertyBuilder<T> propertyBuilder)
        {
            return propertyBuilder.HasConversion(
                new ListOfIdsConverter(),
                new ListOfIdsComparer());
        }

    }
}
