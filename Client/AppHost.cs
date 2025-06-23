using Client.Services;
using Client.ViewModels;
using Client.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureClientServices(this IHostBuilder builder)
    {
        builder.ConfigureServices((context, services) =>
        {
            services.AddSingleton(new HttpClient {  });
            services.AddSingleton<ApiService>();

            // ViewModels
            services.AddSingleton<EmployeeManagingVM>();
            services.AddTransient<EditEmployeeViewModel>();

            // Windows
            services.AddSingleton<EmployeeManagingWindow>();
            services.AddSingleton<EditEmployeeWindow>();
        });

        return builder;
    }
}
