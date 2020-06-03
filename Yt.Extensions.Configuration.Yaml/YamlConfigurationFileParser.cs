using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;
using YamlDotNet.RepresentationModel;

namespace Yt.Extensions.Configuration.Yaml
{
    internal class YamlConfigurationFileParser
    {
        private YamlConfigurationFileParser() { }
        private readonly IDictionary<string, string> _data = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private readonly Stack<string> _context = new Stack<string>();
        private string _currentPath;
        public static IDictionary<string, string> Parse(Stream input)
            => new YamlConfigurationFileParser().ParseStream(input);

        private IDictionary<string, string> ParseStream(Stream input)
        {
            _data.Clear();
            YamlStream yaml = new YamlStream();
            yaml.Load(new StreamReader(input));
            if (yaml.Documents.Count > 0)
            {
                var rootNode = yaml.Documents[0].RootNode;
                VisitNode("",rootNode);
            }
            return _data;
        }

        private void VisitNode(string context,YamlNode node)
        {
            if (node is YamlScalarNode)
            {
                VisitYamlScalarNode(context, node as YamlScalarNode);
            }
            else if (node is YamlMappingNode)
            {
                VisitYamlMappingNode(context, node as YamlMappingNode);
            }
            else if (node is YamlSequenceNode)
            {
                VisitYamlSequenceNode(context, node as YamlSequenceNode);
            }
        }

        private void VisitYamlScalarNode(string context, YamlScalarNode node)
        {
            EnterContext(context);
            if (_data.ContainsKey(_currentPath))
            {
                throw new Exception($"存在相同节点{_currentPath}");
            }
            _data[_currentPath] = node.Value;
            ExitContext();
        }

        private void VisitYamlMappingNode(string context, YamlMappingNode node)
        {
            EnterContext(context);
            foreach (var yamlNode in node.Children)
            {
                context = ((YamlScalarNode)yamlNode.Key).Value;
                VisitNode(context, yamlNode.Value);
            }
            ExitContext();
        }

        private void VisitYamlSequenceNode(string context, YamlSequenceNode node)
        {
            EnterContext(context);
            for (int i = 0; i < node.Children.Count; i++)
            {
                VisitNode(i.ToString(), node.Children[i]);
            }
            ExitContext();
        }

        private void EnterContext(string context)
        {
            if (string.IsNullOrWhiteSpace(context))
            {
                return;
            }
            _context.Push(context);
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }

        private void ExitContext()
        {
            if (_context.Count == 0)
            {
                return;
            }
            _context.Pop();
            _currentPath = ConfigurationPath.Combine(_context.Reverse());
        }
    }
}
