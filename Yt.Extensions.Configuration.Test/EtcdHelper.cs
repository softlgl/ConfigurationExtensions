using System;
using System.Threading;
using System.Threading.Tasks;
using dotnet_etcd;
using Etcdserverpb;
using Google.Protobuf;

namespace Yt.Extensions.Configuration.Test
{
    public class EtcdHelper
    {
        private readonly EtcdClient _etcdClient;
        private readonly long grantId; 
        public EtcdHelper(EtcdClient etcdClient)
        {
            _etcdClient = etcdClient;
            //var rsp = _etcdClient.LeaseGrant(new LeaseGrantRequest { ID = 666,TTL= 5});
            //grantId = rsp.ID;
            //CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(); 
            //_etcdClient.LeaseKeepAlive(grantId, cancellationTokenSource.Token).GetAwaiter().GetResult();
        }

        public async Task PutAsync(string key, string value)
        {
            await _etcdClient.PutAsync(key, value);
        }


        public Task<string> GetValAsync(string key)
        {
            Task<string> result = _etcdClient.GetValAsync(key);
            return result;
        }

        public async Task DelAsync(string key)
        {
            await _etcdClient.DeleteRangeAsync(key);
        }

        public void Watch(string key,Action<WatchResponse> action)
        {
            WatchRequest request = new WatchRequest()
            {
                CreateRequest = new WatchCreateRequest()
                {
                    Key = ByteString.CopyFromUtf8(key)
                }
            };
            _etcdClient.Watch(request, action);
        }
    }
}
