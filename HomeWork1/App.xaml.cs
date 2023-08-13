using System;
using RestEase;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using System.Windows;
using System.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase.HttpClientFactory;
using HomeWork1.Clients;

namespace HomeWork1
{   

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            var builder = Host.CreateApplicationBuilder();

            builder.Services.AddSingleton<MainWindow>();           

            builder.Services.AddRestEaseClient(new AddRestEaseClientOptions<IUsersApi>()
            {
                RestClientConfigurer = client => client.ResponseDeserializer = new JsonResponseDeserializer()
            })
            .ConfigureHttpClient((provider, http) =>
            {
                http.Timeout = TimeSpan.FromSeconds(60);
                http.BaseAddress = new Uri("https://random-data-api.com");
            });

            builder.Services.AddSingleton<IEmployeeClient,  EmployeeClient>();

            _host = builder.Build();
        }

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            await _host.StartAsync();
            MainWindow mainWindow = _host.Services!.GetService<MainWindow>();
            mainWindow!.Show();
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _host.Dispose();
        }
    }    
}
