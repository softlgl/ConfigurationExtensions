using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Consul
{
    public class ConsulConfigurationProvider:ConfigurationProvider
    {
        private readonly ConsulClient _consulClient;
        private readonly string _path;
        private readonly int _waitMillisecond;
        private readonly bool _reloadOnChange;
        private ulong _currentIndex;
        private Task _pollTask;

        public ConsulConfigurationProvider(ConsulOptions options)
        {
            _consulClient = new ConsulClient(x =>
            {
                // consul 服务地址
                x.Address = new Uri(options.Address);
            });
            _path = options.Path;
            _waitMillisecond = options.WaitMillisecond;
            _reloadOnChange = options.ReloadOnChange;
        }

        public override void Load()
        {
            if (_pollTask != null)
            {
                return;
            }
            //加载数据
            LoadData(GetData().GetAwaiter().GetResult());
            //处理数据变更
            PollReaload();
        }

        //设置数据
        private void LoadData(QueryResult<KVPair> queryResult)
        {
            _currentIndex = queryResult.LastIndex;
            Stream stream = new MemoryStream(queryResult.Response.Value);
            Data = JsonConfigurationFileParser.Parse(stream);
        }

        //获取consul配置中心数据
        private async Task<QueryResult<KVPair>> GetData()
        {
            var res = await _consulClient.KV.Get(_path);
            if (res.StatusCode == HttpStatusCode.OK)
            {
                return res;
            }
            throw new Exception($"Error loading configuration from consul. Status code: {res.StatusCode}.");
        }

        //处理数据变更
        private void PollReaload()
        {
            if (_reloadOnChange)
            {
                _pollTask = Task.Run(async () =>
                {
                    while (true)
                    {
                        QueryResult<KVPair> queryResult = await GetData();
                        if (queryResult.LastIndex != _currentIndex)
                        {
                            LoadData(queryResult);
                            OnReload();
                        }
                        await Task.Delay(_waitMillisecond);
                    }
                });
            }
        }
    }
}
