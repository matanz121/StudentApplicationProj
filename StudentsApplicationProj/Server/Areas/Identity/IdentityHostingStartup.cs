using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudentsApplicationProj.Server.Data;

[assembly: HostingStartup(typeof(StudentsApplicationProj.Server.Areas.Identity.IdentityHostingStartup))]
namespace StudentsApplicationProj.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<StudentsApplicationProjServerContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("StudentsApplicationProjServerContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<StudentsApplicationProjServerContext>();
            });
        }
    }
}