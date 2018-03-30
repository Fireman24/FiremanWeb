using System;
using System.Text;

using FiremanApi2.DataBase;
using FiremanApi2.Model;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using Newtonsoft.Json;

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
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                                                            {
                                                                    ValidateIssuer = true,
                                                                    ValidateAudience = false,
                                                                    ValidateLifetime = true,
                                                                    ValidateIssuerSigningKey = true,
                                                                    ValidIssuer = Configuration["Jwt:Issuer"],
                                                                    ValidAudience = Configuration["Jwt:Issuer"],
                                                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                                                            };
                    });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            
            services.AddMvc();
            services.AddMvcCore().AddJsonFormatters(options =>
            {
                options.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            
            app.UseCors("CorsPolicy").UseForwardedHeaders(new ForwardedHeadersOptions
                                                          {
                                                                  ForwardedHeaders =
                                                                          ForwardedHeaders
                                                                                  .XForwardedFor
                                                                          | ForwardedHeaders
                                                                                  .XForwardedProto
                                                          });
            app.UseMvc();
            app.UseAuthentication();
            app.UseStaticFiles();


        }
    }
}
