using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CodeTogetherNGE2E_Tests
{
    static class Configuration
    {
        public static string WebApiUrl {
            get {
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
                    return "Server=DESKTOP-67FEEF1\\SQLEXPRESS;Database=CodeTogetherNG;User Id=codetogetherng;Password=#EDC2wsx$RFV;";
                return connectionString;
            }
        }
    }
}
