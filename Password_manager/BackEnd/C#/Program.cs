using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Windows.Forms;
using PasswordManagerAPI.Data;

namespace WinFormsApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
            Application.Run(new Form1());
        }

        private static IServiceCollection ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer("YourConnectionStringHere"));

            return services;
        }
    }
}
