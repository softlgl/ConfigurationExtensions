using System;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Consul
{
    public static class ConsulConfigurationExtensions
    { 
        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string serverAddress,string path)
        {
            return AddConsul(builder, serverAddress:serverAddress, path: path,reloadOnChange: false);
        }

        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string serverAddress, string path, bool reloadOnChange)
        {
            return AddConsul(builder, serverAddress: serverAddress, path: path, reloadOnChange,5000);
        }

        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, string serverAddress, string path, bool reloadOnChange, int waitMillisecond)
        {
            return AddConsul(builder,options => {
                options.Address = serverAddress;
                options.Path = path;
                options.ReloadOnChange = reloadOnChange;
                options.WaitMillisecond = waitMillisecond;
            });
        }

        public static IConfigurationBuilder AddConsul(this IConfigurationBuilder builder, Action<ConsulOptions> options)
        {
            ConsulOptions consulOptions = new ConsulOptions();
            options.Invoke(consulOptions);
            return builder.Add(new ConsulConfigurationSource { ConsulOptions = consulOptions });
        }
    }
}
