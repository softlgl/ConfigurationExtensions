# IConfigurationBuilder扩展
扩展Properties，Yaml，Consul，Etcd等操作

使用方式
```
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("mysettings.json")
    .AddPropertiesFile("myproperties.properties")
    .AddYamlFile("myyaml.yml")
    .AddEtcd("http://127.0.0.1:2379", "service/mydemo", true)
    .AddConsul("http://localhost:8500/","mydemo/test-dev",true,10*1000);
IConfiguration config = configurationBuilder.Build();
foreach (var item in config.AsEnumerable())
{
    System.Diagnostics.Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
}
```
