using System;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Consul
{
    public class ConsulConfigurationSource : IConfigurationSource
    {
        public ConsulOptions ConsulOptions { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new ConsulConfigurationProvider(ConsulOptions);
        }
    }
}
