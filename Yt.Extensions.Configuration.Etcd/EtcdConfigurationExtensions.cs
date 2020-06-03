using System;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Etcd
{
    public static class EtcdConfigurationExtensions
    { 
        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, string serverAddress,string path)
        {
            return AddEtcd(builder, serverAddress:serverAddress, path: path,reloadOnChange: false);
        }

        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, string serverAddress, string path, bool reloadOnChange)
        {
            return AddEtcd(builder,options => {
                options.Address = serverAddress;
                options.Path = path;
                options.ReloadOnChange = reloadOnChange;
            });
        }

        public static IConfigurationBuilder AddEtcd(this IConfigurationBuilder builder, Action<EtcdOptions> options)
        {
            EtcdOptions etcdOptions = new EtcdOptions();
            options.Invoke(etcdOptions);
            return builder.Add(new EtcdConfigurationSource { EtcdOptions = etcdOptions });
        }
    }
}
