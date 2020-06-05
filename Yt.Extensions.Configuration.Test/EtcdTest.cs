using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using dotnet_etcd;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Yt.Extensions.Configuration.Etcd;

namespace Yt.Extensions.Configuration.Test
{
    [TestFixture()]
    public class EtcdTest
    {
        [Test()]
        public void TestEtcd()
        {
            EtcdHelper etcdHelper = new EtcdHelper(new EtcdClient("http://127.0.0.1:2379"));
            etcdHelper.PutAsync("service/mydemo", "{\"Id\": 100,   \"Name\": \"KOBE\",   \"Address\": {     \"Country\": \"USA\",     \"City\": \"LOS\"   },   \"consul\": {     \"ServiceName\": \"MyDemo\",     \"ConsulAddress\": \"http://localhost:8500/\",     \"ServiceHealthCheck\": \"http://localhost:5000/api/health\"   } }").GetAwaiter().GetResult();

            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddEtcd("http://127.0.0.1:2379", "service/mydemo", true);

            IConfiguration config = builder.Build();
            foreach (var item in config.AsEnumerable())
            {
                Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
            }

            config.GetReloadToken().RegisterChangeCallback(obj => {
                Debug.WriteLine("===接收到变换通知===");
                foreach (var item in config.AsEnumerable())
                {
                    Debug.WriteLine($"key=[{item.Key}],value=[{item.Value}]");
                }
            }, null);
            Thread.Sleep(2000);
            etcdHelper.PutAsync("service/mydemo", "{\"Id\": 8,   \"Name\": \"KOBE\",   \"Address\": {     \"Country\": \"USA\",     \"City\": \"LOS\"   },   \"consul\": {     \"ServiceName\": \"MyDemo\",     \"ConsulAddress\": \"http://localhost:8500/\",     \"ServiceHealthCheck\": \"http://localhost:5000/api/health\"   } }").GetAwaiter().GetResult();
        }
    }
}
