using System;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Yt.Extensions.Configuration.Consul;

namespace Yt.Extensions.Configuration.Test
{
    [TestFixture()]
    public class ConsulTest
    {
        [Test()]
        public void TestConsul()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddConsul("http://localhost:8500/","mydemo/test-dev",true,10*1000);

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }
        }
    }
}
