using System;
namespace Yt.Extensions.Configuration.Consul
{
    public class ConsulOptions
    {
        public string Address { get; set; }
        public string Path { get; set; }
        public int WaitMillisecond { get; set; }
        public bool ReloadOnChange { get; set; }
    }
}
