using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FiremanApi2.DataBase;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace FiremanApi2
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();

            }));
            string connection = Configuration["PostgresqlConnection"];
            services.AddDbContext<FireContext>(options => options.UseNpgsql(connection));
            services.AddMvcCore()
                .AddJsonFormatters(options =>
                    {
                        options.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                        options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    })
                .AddAuthorization();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.Audience = "http://localhost:5001/";
                options.Authority = "http://localhost:5000/";
                options.RequireHttpsMetadata = false;
            }); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorsPolicy");
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
