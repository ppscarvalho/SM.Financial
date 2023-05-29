#nullable disable

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using SM.Financial.Apresentation.Api.Filter;
using SM.Financial.Infrastructure.Extensions;
using SM.Util.Extensions;
using System.Dynamic;
using System.Net.Mime;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

const string SwaggerService = "Financial";

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new OpenApiInfo
        {
            Title = $"{SwaggerService} Service",
            Version = "v1",
            Description = $"{SwaggerService} v1 API"
        });

    c.SchemaFilter<EnumDataTypeAttributeSchemaFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddInfrastructureLayer(builder.Configuration);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddHealthChecks()
    .AddMySql(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"], name: "MySQL", tags: new[] { "ready" })
    .AddRabbitMQ(builder.Configuration["RabbitMq:ConnectionString"], name: "RabbitMQ", tags: new[] { "ready" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks($"/financial/health", new HealthCheckOptions
{
    Predicate = (check) => check.Tags.Contains("ready"),
    ResponseWriter = async (context, report) =>
    {
        var components = HandleComponentStatusString(report);
        var result = HandleResultJson(report, components);
        context.Response.ContentType = MediaTypeNames.Application.Json;
        await context.Response.WriteAsync(result);
    }
});

object HandleComponentStatusString(HealthReport report)
{
    dynamic components = new ExpandoObject();
    foreach (var entry in report.Entries)
    {
        AddProperty(components, entry.Key, new
        {
            status = entry.Value.Status.ToString() == "Healthy" ? "up" : entry.Value.Status.ToString()
        });
    }
    return components;
}

string HandleResultJson(HealthReport report, object components)
{
    return new
    {
        microservice = "SM.Financial",
        timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd hh:mm"),
        status = report.Status.ToString() == "Healthy" ? "up" : report.Status.ToString(),
        version = "1",
        components
    }.SerializeObject();
}
void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
{
    var expandoDict = expando as IDictionary<string, object>;
    if (expandoDict.ContainsKey(propertyName))
        expandoDict[propertyName] = propertyValue;
    else
        expandoDict.Add(propertyName, propertyValue);
}

app.Run();
