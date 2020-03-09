using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;

namespace Yt.Extensions.Configuration.Test
{
    [TestFixture()]
    public class PropertiesTest
    {
        //nuget api key:oy2imukyqemoulk5rygat7dl3qwm4zxe7gctv66ij4qa7q
        [Test()]
        public void TestProperties()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddPropertiesFile("myproperties.properties");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
        }
    }
}
