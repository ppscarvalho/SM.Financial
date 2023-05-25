#nullable disable

using SM.Financial.Infrastructure.Extensions;
using SM.Tools.Configuration;

namespace SM.Financial.Apresentation.Api
{
    public class Startup : Tools.Configuration.StartupBase
    {
        public Startup(WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructureLayer(builder.Configuration);

            var startupBuild = new StartupBuilder(
                builder: builder,
                nameMicroservice: "SM.Financial",
                pathServer: builder.Configuration["pathServer"])
            {
                Swagger = new SwaggerBuilder
                (
                    apiVersionSwagger: "1",
                    apiVersionDefault: "1",
                    pathBaseApi: "swagger",
                    descriptionSwagger: "Description Financial"
                    ),
                //Redis = new(urlRedis: builder.Configuration["Redis:ConnectionString"]),
                HealthChecksBuilder = new()
                {
                    //Redis = new
                    //    (
                    //        builder.Configuration["Redis:ConnectionString"]
                    //    ),
                    RabbitMQ = new
                        (
                            builder.Configuration["RabbitMq:ConnectionString"]
                        ),
                    MySql = new(
                            builder.Configuration["ConnectionStrings:DefaultConnection"]
                        )
                }
            };

            Build(startupBuild);
        }
    }
}
