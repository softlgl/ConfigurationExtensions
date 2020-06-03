using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using dotnet_etcd;
using Etcdserverpb;
using Google.Protobuf;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Etcd
{
    public class EtcdConfigurationProvider : ConfigurationProvider
    {
        private readonly string _path;
        private readonly bool _reloadOnChange;
        private readonly EtcdClient _etcdClient;

        public EtcdConfigurationProvider(EtcdOptions options)
        {
            _etcdClient = new EtcdClient(options.Address,username: options.UserName,password: options.PassWord);
            _path = options.Path;
            _reloadOnChange = options.ReloadOnChange;
        }

        public override void Load()
        {
            LoadData();
            if (_reloadOnChange)
            {
                ReloadData();
            }
        }

        private void LoadData()
        {
            string result = _etcdClient.GetValAsync(_path).GetAwaiter().GetResult();
            Data = ConvertData(result);
        }

        private IDictionary<string,string> ConvertData(string result)
        {
            byte[] array = Encoding.UTF8.GetBytes(result);
            MemoryStream stream = new MemoryStream(array);
            return JsonConfigurationFileParser.Parse(stream);
        }

        private void ReloadData()
        {
            WatchRequest request = new WatchRequest()
            {
                CreateRequest = new WatchCreateRequest()
                {
                    Key = ByteString.CopyFromUtf8(_path)
                }
            };
            _etcdClient.Watch(request, rsp =>
            {
                if (rsp.Events.Any())
                {
                    var @event = rsp.Events[0];
                    Data = ConvertData(@event.Kv.Value.ToStringUtf8());
                    OnReload();
                }
            });
        }
    }
}
