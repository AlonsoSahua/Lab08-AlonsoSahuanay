using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Lab08_AlonsoSahuanay.Controllers;

public class OrderEndpointsDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        var paths = swaggerDoc.Paths
            .OrderBy(p => p.Key)
            .ToDictionary(p => p.Key, p => p.Value);
        
        swaggerDoc.Paths = new OpenApiPaths();
        foreach (var path in paths)
        {
            swaggerDoc.Paths.Add(path.Key, path.Value);
        }
    }
}