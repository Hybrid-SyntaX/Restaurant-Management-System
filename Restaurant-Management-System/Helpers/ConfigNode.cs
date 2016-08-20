using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chocolatey.Data.ConfigFile
{
    public class ConfigNode
    {
        public ConfigNodeType Type;
        public string Name;
        public string Value;

        public ConfigNode()
        {

        }

        public ConfigNode(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
        public ConfigNode(string sectionName)
        {
            this.Name = sectionName;
            this.Value = sectionName;
            this.Type = ConfigNodeType.Section;
        }


        public ConfigNode Read(string unparsedString)
        {
            if (!unparsedString.StartsWith(EscapeCharaters.Comment.ToString()))
            {

                if (unparsedString.Contains(EscapeCharaters.BOS) && unparsedString.Contains(EscapeCharaters.EOS))
                {
                    this.Value = unparsedString.Substring(unparsedString.IndexOf(EscapeCharaters.BOS) + 1, unparsedString.IndexOf(EscapeCharaters.EOS) - 1);
                    this.Name = this.Value;
                    this.Type = ConfigNodeType.Section;
                }
                else if (unparsedString.Contains(EscapeCharaters.Delimiter))
                {
                    this.Name = unparsedString.Substring(0, unparsedString.IndexOf(EscapeCharaters.Delimiter)).Trim();
                    this.Value = unparsedString.Substring(unparsedString.IndexOf(EscapeCharaters.Delimiter) + 1).Trim();
                    this.Type = ConfigNodeType.Property;
                }

                if (this.Value != null && this.Value.Contains(EscapeCharaters.Comment))
                {
                    this.Value = this.Value.Remove(this.Value.IndexOf(EscapeCharaters.Comment));
                }
            }
            return this;
        }

        public static implicit operator string(ConfigNode configNode)
        {
            return configNode.Value;
        }
        public static implicit operator ConfigNode(string strConfigNode)
        {
            return new ConfigNode() { Value = strConfigNode };
        }
        public override string ToString()
        {
            if (this.Type == ConfigNodeType.Property)
                return this.Name + EscapeCharaters.Delimiter + this.Value;
            else if (this.Type == ConfigNodeType.Section)
                return EscapeCharaters.BOS + this.Name + EscapeCharaters.EOS;
            else return null;
        }

    }
}
