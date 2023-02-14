using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Description;

namespace SterlingAPI.Authentication
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (apiDescription.RelativePath.ToLower() == "api/employees/upload") {
                operation.parameters = new List<Parameter>();
                operation.parameters.Add(new Parameter
                {
                    name = "file",
                    description = "uploads a file",
                    type = "file",
                    @in = "formData",
                    required = true
                });
                operation.consumes.Add("multipart/form-data");
            }
        }
    }
}