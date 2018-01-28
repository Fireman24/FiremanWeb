using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

using FiremanApi.DataBase;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FiremanApi
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new IConfigurationBuilder
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            

        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

            }));
            string connection = Configuration.GetConnectionString("PostgresqlConnection");
            services.AddDbContext<FireContext>(options => options.UseNpgsql(connection));

            services
                    .AddMvcCore()
                    .AddAuthorization()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                        options.SerializerSettings.Converters.Add(new StringEnumConverter
                                                                  {
                                                                      AllowIntegerValues = true,
                                                                      CamelCaseText = true
                                                                  });
                    });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Audience = "http://localhost:5001/";
                options.Authority = "http://localhost:5000/";
                options.RequireHttpsMetadata = false;
            });


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseCors("CorsPolicy");
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseMvc();

            var key = Encoding.UTF8
                    .GetBytes("401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1");

            var options = new JwtBearerOptions
                          {

                              TokenValidationParameters = {
                                                              ValidIssuer = "ExampleIssuer",
                                                              ValidAudience = "ExampleAudience",
                                                              IssuerSigningKey = new SymmetricSecurityKey(key),
                                                              ValidateIssuerSigningKey = true,
                                                              ValidateLifetime = true,
                                                              ClockSkew = TimeSpan.Zero
                                                          }
                          };

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
        }
    }
}
