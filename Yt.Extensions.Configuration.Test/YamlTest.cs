using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Yt.Extensions.Configuration.Test
{
    [TestFixture()]
    public class YamlTest
    {
        [Test()]
        public void TestYaml()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddYamlFile("myyaml.yml",true);

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
        }

        [Test()]
        public void TestAll()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddPropertiesFile("myproperties.properties")
                .AddYamlFile("myyaml.yml");

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
        }
    }
}
