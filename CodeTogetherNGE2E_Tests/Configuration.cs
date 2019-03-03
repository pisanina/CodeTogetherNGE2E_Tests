using NUnit.Framework;
using System.IO;
using System.Reflection;

namespace CodeTogetherNGE2E_Tests
{
    internal static class Configuration
    {
        public static string WebApiUrl
        {
            get
            {
                var url= TestContext.Parameters["webAppUrl"];
                if (string.IsNullOrWhiteSpace(url))
                    return "https://localhost:44362/";
                return url;
            }
        }

        public static string WebDriverLocation
        {
            get
            {
                var driverLocation = TestContext.Parameters["webDriverLocation"];
                if (string.IsNullOrWhiteSpace(driverLocation))
                    return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return driverLocation;
            }
        }

        public static string ConnectionString
        {
            get
            {
                var connectionString = TestContext.Parameters["connectionString"];
                if (string.IsNullOrWhiteSpace(connectionString))
                    return "Server=DESKTOP-67FEEF1\\SQLEXPRESS;Database=CodeTogetherNG; Trusted_Connection=True";
                return connectionString;
            }
        }
    }
}