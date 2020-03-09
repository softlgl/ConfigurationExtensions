using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Yt.Extensions.Configuration.PropertiesConfiguration;

namespace Yt.Extensions.Configuration
{
    public static class PropertiesConfigurationExtensions
    {
        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path)
        {
            return builder.AddPropertiesFile(path, false);
        }

        public static IConfigurationBuilder AddPropertiesFile(this IConfigurationBuilder builder, string path, bool reloadOnChange)
        {
            return builder.AddPropertiesFile(path, reloadOnChange, false);
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
