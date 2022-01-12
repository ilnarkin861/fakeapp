using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Interfaces;
using FakeApp.Infrastructure.Options;
using FakeApp.Api.Auth.Middleware;
using FakeApp.Api.Auth.Services;
using FakeApp.Api.Auth.Services.Jwt;



namespace FakeApp.Api.Auth
{
    /// <summary>
    /// Конфигурация приложения авторизации
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection"));
            });
            
            services.Configure<AuthOptions>(Configuration.GetSection("Auth"));

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<BasicSignInManager>();
            services.AddScoped<SocialSignInManager>();
            services.AddScoped<ISignUpManager, BasicSignUpManager>();
            services.AddScoped<IJwtGenerator, JwtGenerator>();
            
            services.AddControllers();
            
            services.AddCors();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FakeApp.Api.Auth", Version = "v1"});
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FakeApp.Api.Auth v1"));
            }
            
            app.UseCors(
                options => options.WithOrigins(Configuration["AllowedHosts"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
            );
            
            app.UseMiddleware<ApiExceptionsMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}