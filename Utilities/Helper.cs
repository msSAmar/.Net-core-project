using updateApi;
using updateApi.Services;
//using updateApi.Controllers;
using updateApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace  updateApi.Utilities
{
public static class Helper
{
    public static void AddAssiment(this IServiceCollection services)
    {
        services.AddSingleton<IOTaskServices, TaskServices>();
        services.AddSingleton<IOUserService, UserService>();
        
    }
}
}