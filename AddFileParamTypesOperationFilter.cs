using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CMS_appBackend.OperationFilters
{
    public class AddFileParamTypesOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (
                context.ApiDescription.ParameterDescriptions.Any(
                    x => x.ModelMetadata.ContainerType == typeof(IFormFile)
                )
            )
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = { ["multipart/form-data"] = new OpenApiMediaType() },
                    Required = true
                };
            }
        }
    }

    public class AddFileUploadParamsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (
                context.ApiDescription.ParameterDescriptions.Any(
                    x => x.ModelMetadata.ContainerType == typeof(IFormFile)
                )
            )
            {
                var fileParameters = operation.Parameters
                    .Where(p => p.In == ParameterLocation.Query && p.Schema.Type == "string")
                    .ToList();
                foreach (var fileParameter in fileParameters)
                {
                    operation.Parameters.Remove(fileParameter);
                }

                operation.Parameters.Add(
                    new OpenApiParameter
                    {
                        Name = "file",
                        In = ParameterLocation.Query, // Change to Query if using Swashbuckle.AspNetCore version 6.x
                        Description = "Upload File",
                        Required = true,
                        Schema = new OpenApiSchema { Type = "file" }
                    }
                );
            }
        }
    }
}
