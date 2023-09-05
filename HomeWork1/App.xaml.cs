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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase.HttpClientFactory;
using HomeWork1.Clients;
using DatabaseModel;
using DatabaseModel.Repositories;
using HomeWork1.EmployeeDirectory;
using HomeWork1.ViewModels;

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

            builder.Configuration.AddJsonFile("appsettings.json");            

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

            builder.Services.AddDbContext<MySqlContext>(
            x => x.UseMySql(builder.Configuration.GetConnectionString("Main")!, new MySqlServerVersion(new Version(5, 7, 26))));
            builder.Services.AddSingleton<IDataContext, MySqlContext>();
            builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton(typeof(IRepository<>), typeof(EfCoreRepository<>));
            builder.Services.AddAutoMapper(typeof(IClientAssemblyMarker).Assembly);

            builder.Services.AddScoped<MainWindowViewModel>();

            builder.Services.AddSingleton(sp => new MainWindow() { DataContext = sp.GetRequiredService<MainWindowViewModel>() });

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
