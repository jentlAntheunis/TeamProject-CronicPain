

using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pebbles.Swagger;

public class AddRequiredHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        // Add the api-version header
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "api-version",
            In = ParameterLocation.Header,
            Required = true, // Set to false if this header is not required
            Schema = new OpenApiSchema
            {
                Type = "string",
                Default = new OpenApiString("1.0") // Set the default version
            }
        });
    }
}