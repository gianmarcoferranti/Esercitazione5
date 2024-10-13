
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Task_Autofficina.Models;
using Task_Autofficina.Repos;
using Task_Autofficina.Services;

namespace Task_Autofficina
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            

            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });
                c.MapType<DateOnly?>(() => new OpenApiSchema { Type = "string", Format = "date" });
            });


            #region Importanti per la configurazione del context

#if DEBUG
            builder.Services.AddDbContext<AutofficinaContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseTest"))
                );

            builder.Services.AddScoped<ClienteRepo>();
            builder.Services.AddScoped<VeicoloRepo>();

            builder.Services.AddScoped<ClienteService>();
            builder.Services.AddScoped<VeicoloService>();
#else
            builder.Services.AddDbContext<AutofficinaContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseProd"))
                );
#endif

            #endregion
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            }

            app.UseHttpsRedirection();


            app.UseAuthorization();


            app.MapControllers();

            #region Configurazione di dev per CORS
#if DEBUG
            app.UseCors(builder =>
            builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
                );
#endif

            #endregion

            app.Run();
        }
    }
}
