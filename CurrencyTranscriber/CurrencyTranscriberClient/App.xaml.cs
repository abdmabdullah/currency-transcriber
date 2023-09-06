using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace CurrencyTranscriberClient
{
    
    public partial class App : Application
    {
        private IServiceProvider serviceProvider;

        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient("HttpClient", client => 
            {
                client.BaseAddress = new Uri(CurrencyTranscriberClient.Properties.Settings.Default.ApiUrl.ToString());
            });

            services.AddSingleton(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HttpClient"));
            services.AddTransient(typeof(MainWindow));
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}
