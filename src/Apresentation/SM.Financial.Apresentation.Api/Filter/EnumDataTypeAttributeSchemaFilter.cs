using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;

namespace SM.Financial.Apresentation.Api.Filter
{
    /// <summary>
    /// EnumDataTypeAttributeSchemaFilter
    /// </summary>
    public class EnumDataTypeAttributeSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply
        /// </summary>
        /// <param name="schema"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            CustomAttributeData customAttribute = context.MemberInfo?.CustomAttributes.FirstOrDefault(a => a.AttributeType.Name == nameof(EnumDataTypeAttribute));
            if (customAttribute != null)
            {
                var type = customAttribute.ConstructorArguments.FirstOrDefault().Value as Type;

                schema.Enum.Clear();
                var enumItems = type!.GetFields(BindingFlags.Static | BindingFlags.Public)
                    .Where(e => e.GetCustomAttribute(typeof(JsonIgnoreAttribute)) == null);

                foreach (var item in enumItems)
                {
                    schema.Enum.Add(new OpenApiString(item.Name));
                }
            }
        }
    }
}
