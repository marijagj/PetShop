using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using Microsoft.AspNetCore.Identity;

namespace PetShop
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
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy",
                    policy => policy.RequireClaim("Delete Role"));
                options.AddPolicy("CreateRolePolicy",
                    policy => policy.RequireClaim("Create Role"));
                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireClaim("Edit Role"));
                options.AddPolicy("DeleteProductPolicy",
                    policy => policy.RequireClaim("Delete Product"));
                options.AddPolicy("EditProductPolicy",
                    policy => policy.RequireClaim("Edit Product"));
                options.AddPolicy("CreateSopstvenikPolicy",
                    policy => policy.RequireClaim("Create Sopstvenik"));
                options.AddPolicy("DeleteSopstvenikPolicy",
                    policy => policy.RequireClaim("Delete Sopstvenik"));
                options.AddPolicy("EditSopstvenikPolicy",
                    policy => policy.RequireClaim("Edit Sopstvenik"));
                options.AddPolicy("CreateProductPolicy",
                    policy => policy.RequireClaim("Create Product"));
                options.AddPolicy("PoracajProductPolicy",
                    policy => policy.RequireClaim("Poracaj Product"));
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddIdentity<IdentityUser, IdentityRole>(
                options =>
                {
                    options.Password.RequiredLength = 5;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireNonAlphanumeric = false;
                })
        .AddEntityFrameworkStores<PetShopContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<PetShopContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("PetShopContext")));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Produkts}/{action=Index}/{id?}");
            });
        }
    }
}
