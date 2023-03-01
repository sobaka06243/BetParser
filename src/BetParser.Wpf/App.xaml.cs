using BetParser.Services;
using BetParser.Wpf.Services;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace BetParser.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        static App()
        {
            Ioc.Default.ConfigureServices(ConfigureServices());
            Services = Ioc.Default;
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IMatchProvider>(new MatchProvider(new Client.BetParserClient("https://localhost:7295/", new HttpClient())));

            return services.BuildServiceProvider();
        }

        public static IServiceProvider Services { get; private set; }
    }


}
