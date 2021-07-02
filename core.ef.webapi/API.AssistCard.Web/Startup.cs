using API.Viagem.Domain.Interfaces.Repository;
using API.Viagem.Domain.Interfaces.Services;
using API.Viagem.Domain.Models;
using API.Viagem.Domain.Services;
using API.Viagem.Infrastructure;
using API.Viagem.Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Net;
using System.Text;

namespace API.Viagem.Web
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
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<ICotacaoService, CotacaoService>();
            services.AddScoped<ICotacaoRepository, CotacaoRepository>();

            services.AddTransient<IEmissaoService, EmissaoService>();
            services.AddScoped<IEmissaoRepository, EmissaoRepository>();


            services.AddScoped<IPassageiroRepository, PassageiroRepository>();

            services.AddMvc().AddXmlSerializerFormatters();


            //SigningConfigurations
            SigningConfigurations signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            //TokenConfigurations
            TokenConfigurations tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);



            string Issuer = Configuration.GetSection("SecuritySettings").GetSection("Issuer").Value;
            string Audience = Configuration.GetSection("SecuritySettings").GetSection("Audience").Value;
            string SecurityKey = Configuration.GetSection("SecuritySettings").GetSection("SecurityKey").Value;
            SymmetricSecurityKey SymmSecKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
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

            services.AddDbContext<MultCalcSegContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(builder =>
            {
                builder.Run(
                          async context =>
                          {
                              context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                              context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                              IExceptionHandlerFeature error = context.Features.Get<IExceptionHandlerFeature>();
                              if (error != null)
                              {
                                  await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                              }
                          });
            });

            app.UseMvc();
        }
    }
}
