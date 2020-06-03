using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;

namespace Yt.Extensions.Configuration.Properties
{
    public class PropertiesConfigurationProvider : FileConfigurationProvider
    {
        public PropertiesConfigurationProvider(PropertiesConfigurationSource source)
            : base(source)
        {
        }

        public override void Load(Stream stream)
        {
            Data = ReadProperties(stream);
        }

        private IDictionary<string, string> ReadProperties(Stream stream)
        {
            IDictionary<string, string> data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            using (StreamReader sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    string bufLine = sr.ReadLine();
                    if (bufLine.StartsWith("#") || string.IsNullOrWhiteSpace(bufLine))
                    {
                        continue;
                    }
                    if (Regex.Matches(bufLine, "=").Count == 0)
                    {
                        throw new FormatException("存在配置不正确的信息");
                    }
                    int limit = bufLine.Length;
                    int keyLen = 0;
                    int valueStart = limit;
                    char c;
                    bool hasSep = false;
                    bool precedingBackslash = false;
              
                    while (keyLen < limit)
                    {
                        c = bufLine[keyLen];
                        if ((c == '=' || c == ':') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            hasSep = true;
                            break;
                        }
                        else if ((c == ' ' || c == '\t' || c == '\f') & !precedingBackslash)
                        {
                            valueStart = keyLen + 1;
                            break;
                        }
                        if (c == '\\')
                        {
                            precedingBackslash = !precedingBackslash;
                        }
                        else
                        {
                            precedingBackslash = false;
                        }
                        keyLen++;
                    }
                    while (valueStart < limit)
                    {
                        c = bufLine[valueStart];
                        if (c != ' ' && c != '\t' && c != '\f')
                        {
                            if (!hasSep && (c == '=' || c == ':'))
                            {
                                hasSep = true;
                            }
                            else
                            {
                                break;
                            }
                        }
                        valueStart++;
                    }
                    string key = bufLine.Substring(0, keyLen);
                    if (string.IsNullOrWhiteSpace(key))
                        throw new FormatException("存在未提供key的配置信息");
                    string values = bufLine.Substring(valueStart, limit - valueStart);
                    data.Add(key, values);
                }
            }
            return data;
        }
    }
}
