using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;

namespace Yt.Extensions.Configuration.Properties
{
    public static class PropertiesConfigurationExtensions
    {
        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path)
        {
            return builder.AddPropertiesFile(path, false);
        }

        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path, bool optional)
        {
            return builder.AddPropertiesFile(path, optional, false);
        }

        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path, bool optional, bool reloadOnChange)
        {
            return builder.AddPropertiesFile(provider:null, path, optional, reloadOnChange);
        }

        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder ,IFileProvider provider, string path, bool optional, bool reloadOnChange)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path不能为空");
            }
            return builder.AddPropertiesFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, Action<PropertiesConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }
    }
}
