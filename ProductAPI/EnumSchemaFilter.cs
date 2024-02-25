using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ProductAPI;

public class EnumSchemaFilter : ISchemaFilter
{
	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		if (!context.Type.IsSubclassOf(typeof(Enum)))
		{
			return;
		}

		var fields = context.Type.GetFields(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
		
		schema.Enum = fields.Select(f => new OpenApiString(f.Name)).Cast<IOpenApiAny>().ToList();
		schema.Type = "String";
		schema.Properties = null;
		schema.AllOf = null;
	}
}
