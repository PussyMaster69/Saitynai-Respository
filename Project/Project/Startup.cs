using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.WindowsAzure.Storage;
using Project.DbModels;

namespace Project
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
            
            services.AddDbContext<MyDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MyDb")));

            /*services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            });*/
            
            
           /* 
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MyDbContext>()
                .AddDefaultTokenProviders();
            
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = "117124388101-7bt8upvhefdc4j0jlo02bp7a8lpsv61e.apps.googleusercontent.com";
                googleOptions.ClientSecret = "bHC-dYWGJPJ5Zb6_bnGbDgKV";
            });*/
            /*
            ClientId = "CLIENT_ID_GOES_HERE",
            ClientSecret = "CLIENT_SECRET_GOES_HERE",
            Authority = "https://accounts.google.com",
            ResponseType = OpenIdConnectResponseType.Code,
            GetClaimsFromUserInfoEndpoint = true,
            SaveTokens = true,
            Events = new OpenIdConnectEvents()
            {
                OnRedirectToIdentityProvider = (context) =>
                {
                    if (context.Request.Path != "/account/external")
                    {
                        context.Response.Redirect("/account/login");
                        context.HandleResponse();
                    }

                    return Task.FromResult(0);
                }
            }*/
            services.AddAuthentication(options => 
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect(options => 
            {
                options.Authority = "https://accounts.google.com";
                options.ClientId = "117124388101-7bt8upvhefdc4j0jlo02bp7a8lpsv61e.apps.googleusercontent.com";
                options.ClientSecret = "bHC-dYWGJPJ5Zb6_bnGbDgKV";
                options.CallbackPath = "/api/account/authenticated";
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;       
            });;
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
