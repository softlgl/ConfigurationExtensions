using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.YamlConfiguration
{
    public class YamlConfigurationProvider: FileConfigurationProvider
    {
        public YamlConfigurationProvider(YamlConfigurationSource source)
            :base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = YamlConfigurationFileParser.Parse(stream);
        }
    }
}
