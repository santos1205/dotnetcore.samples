using API.AssistCard.Domain.Interfaces.Repository;
using API.AssistCard.Domain.Interfaces.Services;
using API.AssistCard.Domain.Models;
using API.AssistCard.Domain.Services;
using API.AssistCard.Infrastructure;
using API.AssistCard.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace API.AssistCard.Web
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
            string Issuer = Configuration.GetSection("SecuritySettings").GetSection("Issuer").Value;
            string Audience = Configuration.GetSection("SecuritySettings").GetSection("Audience").Value;
            string SecurityKey = Configuration.GetSection("SecuritySettings").GetSection("SecurityKey").Value;
            var SymmSecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {                        
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,                        
                        ClockSkew = TimeSpan.FromSeconds(10),
                        ValidIssuer = Issuer,
                        ValidAudience = Audience,                        
                        IssuerSigningKey = SymmSecKey
                    };
                });

            services.AddDbContext<ApiAssistCardContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
