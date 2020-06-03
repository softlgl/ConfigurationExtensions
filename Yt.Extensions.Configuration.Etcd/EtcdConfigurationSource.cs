using System;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Etcd
{
    public class EtcdConfigurationSource : IConfigurationSource
    {
        public EtcdOptions EtcdOptions { get; set; }

        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new EtcdConfigurationProvider(EtcdOptions);
        }
    }
}
