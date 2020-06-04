# IConfigurationBuilder扩展
针对Configuration扩展Properties，Yaml本地文件数据源，还支持Consul，Etcd远程配置中心数据源

使用方式
```cs
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
