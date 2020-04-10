using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repository;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //* Resolvendo as Dependências            
            //services.AddScoped(); //* Realiza um Singleton por Requisição

            //* Adicionado no última versão do .NetCore, realiza mesma função do AddScoped, porém, já gerenciando o DbContext
            services.AddDbContext<DataContext>(opt => opt.UseInMemoryDatabase("database"));
            //services.AddDbContext<DataContext>(opt => opt.UserSqlServer("Configuration.GetConnectionString("connectionString")"));

            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                    {
                        options.Authority = "https://securetoken.google.com/todo-d2781";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer  =true,
                            ValidIssuer = "https://securetoken.google.com/todo-d2781",
                            ValidateAudience = true,
                            ValidAudience = "todo-d2781",
                            ValidateLifetime = true
                        };
                    }
                );

            // var firebaseConfig = {
            //     apiKey: "AIzaSyBdVoUOzjsq9MV4avy_EMNp_Ejg3Ec_9yk",
            //     authDomain: "todo-d2781.firebaseapp.com",
            //     databaseURL: "https://todo-d2781.firebaseio.com",
            //     projectId: "todo-d2781",
            //     storageBucket: "todo-d2781.appspot.com",
            //     messagingSenderId: "295835182105",
            //     appId: "1:295835182105:web:60825d9da17389e8bef688"
            //   };

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();


            app.UseHttpsRedirection();

            app.UseRouting();

            // Permissão de Aplicação LocalHost
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
