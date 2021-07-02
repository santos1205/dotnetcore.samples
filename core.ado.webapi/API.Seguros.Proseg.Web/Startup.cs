using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using API.Seguros.Proseg.Domain.Entidades;
using API.Seguros.Proseg.Domain.Interfaces.Repository;
using API.Seguros.Proseg.Domain.Interfaces.Services;
using API.Seguros.Proseg.Domain.Services;
using API.Seguros.Proseg.Infrastructure.Data;
using API.Seguros.Proseg.Infrastructure.Repository;
using System;
using System.Net;

namespace API.Seguros.Proseg.Web
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
            //Registrar o serviço.
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            services.AddTransient<ISeguroAutoService, SeguroAutoService>();
            services.AddScoped<ISeguroAutoRepository, SeguroAutoRepository>();

            services.AddTransient<ISeguroModalidadeService, SeguroModalidadeService>();
            services.AddScoped<ISeguroModalidadeRepository, SeguroModalidadeRepository>();

            services.AddTransient<ILogService, LogService>();
            services.AddScoped<ILogRepository, LogRepository>();


            services.AddScoped<IUsuarioMultiCalculoRepository, UsuarioMultiCalculoRepository>();




            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                Configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);


            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;

                // Valida a assinatura de um token recebido
                paramsValidation.ValidateIssuerSigningKey = true;

                // Verifica se um token recebido ainda é válido
                paramsValidation.ValidateLifetime = true;

                // Tempo de tolerância para a expiração de um token (utilizado
                // caso haja problemas de sincronismo de horário entre diferentes
                // computadores envolvidos no processo de comunicação)
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });


            // Ativa o uso do token como forma de autorizar o acesso
            // a recursos deste projeto
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build());
            });



            services.AddDbContext<MultCalcSegContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<ApiMultiCalculoContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CustomConnection")));
            services.AddDbContext<MultiSegurosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MSConnection")));
            services.AddDbContext<ApiSegurosContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ApiSegurosConnection")));

            services.AddMvc();
        }

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

                              var error = context.Features.Get<IExceptionHandlerFeature>();
                              if (error != null)
                              {
                                  await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                              }
                          });
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
