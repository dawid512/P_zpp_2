using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using P_zpp_2.Areas.Identity.Data;
using P_zpp_2.Data;

[assembly: HostingStartup(typeof(P_zpp_2.Areas.Identity.IdentityHostingStartup))]
namespace P_zpp_2.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

        

            builder.ConfigureServices((context, services) => {
                services.AddDbContext<P_zpp_2DbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("P_zpp_2DbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<P_zpp_2DbContext>();
            });
        }
    }
}