using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Yt.Extensions.Configuration.PropertiesConfiguration;
using Yt.Extensions.Configuration.YamlConfiguration;

namespace Yt.Extensions.Configuration
{
    public static class YamlConfigurationExtensions
    {
        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path)
        {
            return builder.AddYamlFile(path, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return builder.AddYamlFile(path, optional, false);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return builder.AddYamlFile(provider: null, path, optional, reloadOnChange);
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path不能为空");
            }
            return builder.AddYamlFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddYamlFile(this IConfigurationBuilder builder, Action<YamlConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }
    }
}
