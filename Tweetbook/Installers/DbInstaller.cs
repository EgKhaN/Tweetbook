﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweetbook.Data;

namespace Tweetbook.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultCollection")));
            //services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<DataContext>();
        }
    }
}
