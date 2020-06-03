using System;
namespace Yt.Extensions.Configuration.Etcd
{
    public class EtcdOptions
    {
        public string Address { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string Path { get; set; }
        public bool ReloadOnChange { get; set; }
    }
}
