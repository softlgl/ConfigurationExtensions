# IConfigurationBuilder扩展
针对Configuration扩展Properties，Yaml本地文件数据源，还支持Consul，Etcd远程配置中心数据源
### 引入方式
Properties文件扩展包引入方式
```
Install-Package Yt.Extensions.Configuration.Properties -Version 1.0.0
```
或
```
dotnet add package Yt.Extensions.Configuration.Properties --version 1.0.0
```
或
```
<PackageReference Include="Yt.Extensions.Configuration.Properties" Version="1.0.0" />
```
Yaml文件扩展包引入方式
```
Install-Package Yt.Extensions.Configuration.Yaml -Version 1.0.0
```
或
```
dotnet add package Yt.Extensions.Configuration.Yaml --version 1.0.0
```
或
```
<PackageReference Include="Yt.Extensions.Configuration.Yaml" Version="1.0.0" />
```
Consul配置中心扩展包引入方式
```
Install-Package Yt.Extensions.Configuration.Consul -Version 1.0.0
```
或
```
dotnet add package Yt.Extensions.Configuration.Consul --version 1.0.0
```
或
```
<PackageReference Include="Yt.Extensions.Configuration.Consul" Version="1.0.0" />
```
Etcd配置中心扩展包引入方式
```
Install-Package Yt.Extensions.Configuration.Etcd -Version 1.0.0
```
或
```
dotnet add package Yt.Extensions.Configuration.Etcd --version 1.0.0
```
或
```
<PackageReference Include="Yt.Extensions.Configuration.Etcd" Version="1.0.0" />
```
### 使用方式
```cs
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    //同json使用方式一致
    .AddPropertiesFile("myproperties.properties")
    .AddYamlFile("myyaml.yml")
    //etcd地址 读取目录 变更是否刷新
    .AddEtcd("http://127.0.0.1:2379", "service/mydemo", true)
    //consul地址 读取目录 变更是否刷新 刷新时间间隔
    .AddConsul("http://localhost:8500/","mydemo/test-dev",true,10*1000);
IConfiguration config = configurationBuilder.Build();
foreach (var item in config.AsEnumerable())
{
    System.Diagnostics.Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
}
```
