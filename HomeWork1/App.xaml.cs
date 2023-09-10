using System;
using RestEase;
using Microsoft.Extensions.Configuration;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestEase.HttpClientFactory;
using HomeWork1.Clients;
using DatabaseModel;
using DatabaseModel.Repositories;
using HomeWork1.ViewModels;
using HomeWork1.Views;
using HomeWork1.AppCommon;

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

            builder.Services.AddSingleton<INavigationService, NavigationService>(
                x =>
                {
                    var navigationService = new NavigationService(x);
                    navigationService.Configure(nameof(MainWindow), typeof(MainWindow));
                    navigationService.Configure(nameof(EmployeeView), typeof(EmployeeView));
                    navigationService.Configure(nameof(EmployeeCreateView), typeof(EmployeeCreateView));

                    return navigationService;
                });

            builder.Services.AddScoped<MainWindowViewModel>();
            builder.Services.AddScoped<EmployeeViewModel>();
            builder.Services.AddScoped<EmployeeCreateViewModel>();

            builder.Services.AddTransient(sp => new MainWindow() { DataContext = sp.GetRequiredService<MainWindowViewModel>() });
            builder.Services.AddTransient(sp => new EmployeeView() { DataContext = sp.GetRequiredService<EmployeeViewModel>() });
            builder.Services.AddTransient(sp => new EmployeeCreateView() { DataContext = sp.GetRequiredService<EmployeeCreateViewModel>() });

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
