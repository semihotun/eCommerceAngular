using eCommerceBase.Domain.SeedWork;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace eCommerceBase.Insfrastructure.Utilities.ApiDoc.Swagger
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || context.Type == null)
                return;

            var excludedProperties = context.Type.GetProperties()
                .Where(t => t.GetCustomAttribute<SwaggerIgnore>() != null);

            var propertiesToRemove = schema.Properties.Keys
                .Where(key => excludedProperties
                    .Any(prop => string.Equals(prop.Name, key, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            foreach (var propertyKey in propertiesToRemove)
            {
                schema.Properties.Remove(propertyKey);
            }
        }
    }
}
