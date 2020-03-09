# IConfigurationBuilder扩展
扩展Properties，Yaml文件操作

获取方式
```
Install-Package Yt.Extensions.Configuration -Version 1.0.0
```
或
```
dotnet add package Yt.Extensions.Configuration --version 1.0.0
```
或
```
<PackageReference Include="Yt.Extensions.Configuration" Version="1.0.0" />
```
使用方式
```
IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("mysettings.json")
    .AddPropertiesFile("myproperties.properties")
    .AddYamlFile("myyaml.yml");
IConfiguration config = configurationBuilder.Build();
foreach (var item in config.AsEnumerable())
{
    System.Diagnostics.Debug.WriteLine($"Key:{item.Key}----Value:{item.Value}");
}
  ```
