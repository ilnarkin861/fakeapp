using System.IO;
using FakeApp.Api.Hubs;
using FakeApp.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using FakeApp.Application;
using FakeApp.Application.Albums.Services;
using FakeApp.Application.Posts.Services;
using FakeApp.Application.Services;
using FakeApp.Application.Services.Users;
using FakeApp.Application.Todos.Services;
using FakeApp.Infrastructure.Data;
using FakeApp.Infrastructure.Interfaces;
using FakeApp.Infrastructure.Options;
using FakeApp.Infrastructure.Services.FileSystem;



namespace FakeApp.Api
{
    /// <summary>
    /// Конфигурация основного приложения
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
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
                
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddHttpContextAccessor();

            services.AddApplication(Configuration);
            
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IAlbumService, AlbumService>();
            services.AddScoped<IPhotosService, PhotosService>();
            services.AddScoped<IUserManager, UserManager>();
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<IFileSystemService, FileSystemService>();
            services.AddScoped<IHttpService, HttpService>();
            
            var authOptions = Configuration.GetSection("Auth").Get<AuthOptions>();

            services.Configure<MediaOptions>(Configuration.GetSection("Media"));
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    // true in production
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = authOptions.Issuer,
                        ValidateAudience = true,
                        ValidAudience = authOptions.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddControllers();
            
            services.AddCors();
            
            services.AddSignalR();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "FakeApp.Api", Version = "v1"});
            });
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Configuration["Logging:File:Path"]);
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FakeApp.Api v1"));
            }
            
            app.UseMiddleware<ApiExceptionsMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Configuration["Media:MediaRootPath"])),
                RequestPath = ""
            });

            app.UseCors(
                options => options.WithOrigins("http://localhost:4200")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<CommentsHub>("/posts/comments/notify");
            });
        }
    }
}