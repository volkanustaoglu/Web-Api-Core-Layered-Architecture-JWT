using Microsoft.Extensions.Configuration;
using System.IO;

namespace AppointmentApp.API.AppConfig
{
    public class AppConfiguration
    {
        public readonly string _connectionString = string.Empty;
        public readonly string _secretKey = string.Empty;
        public AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);

            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            var appSetting = root.GetSection("ApplicationSettings");

            _secretKey = root.GetSection("SecretKey").Value;

        }
        public static string ConnectionString()
        {
            AppConfiguration appConfiguration = new AppConfiguration();
            return appConfiguration._connectionString;
        }
        public static string SecretKeyString()
        {
            AppConfiguration secretKey = new AppConfiguration();
            return secretKey._secretKey;
        }

        
    }
}