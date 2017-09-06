using System.IO;
using Microsoft.Extensions.Configuration;
using Perka.Apply.Client.Models;

namespace Perka.Apply.Client.Adapters
{
    public static class ApplicationSettingsAdapter
    {
        static ApplicationSettingsAdapter()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public static ApplicationSettings ApplicationSettings => LoadSettings();

        private static IConfigurationRoot Configuration { get; }

        private static ApplicationSettings LoadSettings()
        {
            return new ApplicationSettings
            {
                FirstName = Configuration["Name:First"],
                LastName = Configuration["Name:Last"],
                Email = Configuration["Contact:Email"],
                PositionId = Configuration["Position:Id"],
                Explanation = Configuration["Position:Explanation"],
                Source = Configuration["Position:Source"],
                GithubApi = new Endpoint
                {
                    Name = Configuration["Endpoints:0:Name"],
                    Uri = Configuration["Endpoints:0:Uri"]
                },
                PerkaApi = new Endpoint
                {
                    Name = Configuration["Endpoints:1:Name"],
                    Uri = Configuration["Endpoints:1:Uri"]
                },
                Resume = new Resume
                {
                    Name = Configuration["File:Name"],
                    Location = Configuration["File:Location"],
                    Extension = Configuration["File:Extension"]
                }
            };
        }
    }
}