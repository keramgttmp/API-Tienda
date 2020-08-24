using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITienda.Data;
using APITienda.Models;
using APITienda.ModelsMappers;
using APITienda.Repository;
using APITienda.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace APITienda
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //agregamos el contexto de la base de datos usando el string de conexxión en appsettings
            services.AddDbContext<ApplicationDbContext>(Options => Options.UseSqlServer(Configuration.GetConnectionString("DefaultDatabase")));

            //debemos inyectar los scopes para vincular la interfaz de los repositorios
            services.AddScoped < ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProductoRepository, ProductoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IOpinionProductoRepository, OpinionProductoRepository>();
            services.AddScoped<IOrdenRepository, OrdenRepository>();
            services.AddScoped<IDetalleOrdenRepository, DetalleOrdenRepository>();

            //añadimos el servicios del AutoMapper
            services.AddAutoMapper(typeof(ModelsMapper));

            //añadimos el servicios para generar la doc en Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Tienda", Version = "v1" });
            });

            //
            services.AddControllers();

            /*Damos soporte para CORS*/
            services.AddCors(options => options.AddDefaultPolicy(builder => {

                // Fluent API
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Para el uso de Swagger
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Tienda");
            });
            //

            app.UseHttpsRedirection();

            app.UseRouting();

            /*Damos soporte para CORS
             DEspues del Routing y antes de UseAuthorization*/
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
