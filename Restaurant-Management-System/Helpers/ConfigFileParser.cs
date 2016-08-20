using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Chocolatey.Data.ConfigFile
{
    public enum ConfigNodeType { Property = 0, Section = 1 }

    public struct EscapeCharaters
    {
        public const char Comment = ';';
        public const char Delimiter = '=';
        public const char BOS = '[';
        public const char EOS = ']';
    }




    public class ConfigFileParser
    {
        List<ConfigNode> _nodes;
        public List<ConfigNode> Nodes
        {
            set
            {
                _nodes = value;
            }
            get
            {
                if (_nodes == null)
                    _nodes = new List<ConfigNode>();

                return _nodes;
            }
        }

        public string FileName;

        public ConfigFileParser()
        {
            
        }
        public ConfigFileParser(string fileName, bool open = true)
        {
            this.FileName = fileName;
            if (open)
                this.Open(fileName);
        }


        public void Open(string fileName = null)
        {
            if (fileName == null)
                fileName = FileName;
            else this.FileName = fileName;

            StreamReader sr = new StreamReader(fileName);



            Parse(sr);

            sr.Close();

        }

        private void Parse(StreamReader sr)
        {
            while (!sr.EndOfStream)
            {
                ConfigNode configNode = new ConfigNode();

                configNode.Read(sr.ReadLine());
                if (configNode.Name != null && configNode.Name != string.Empty)
                    Nodes.Add(configNode);
            }
        }

        public void Write(ConfigNode configNode)
        {
            StreamWriter sw;
            
             sw= new StreamWriter(FileName, true);
            
            
            sw.BaseStream.Position = sw.BaseStream.Length;
            
            sw.Write(configNode.ToString());
            sw.Write(sw.NewLine);
          
            sw.Close();
        }
        public void Write(string name, string value)
        {
            this.Write(new ConfigNode(name, value));
        }
        public void Write(string section)
        {
            this.Write(new ConfigNode(section));
        }
       
        public ConfigNode this[string index]
        {
            get
            {
                return Nodes.Find((confgNode => confgNode.Name.ToLower() == index.ToLower()));
            }
            set
            {
                if (this[index] == null)
                    this.Write(index, value.Value);
                else
                    this.Edit(index, value.Value);
            }

        }
        public Dictionary<string, string> getDictionary()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            foreach (ConfigNode configNode in this.Nodes)
                dictionary[configNode.Name] = configNode.Value;

            return dictionary;
        }
        public void Edit(string key, string newValue)
        {
            if(Nodes.Count==0)
                this.Open(FileName);

            this[key].Value = newValue;
            StreamWriter streamWriter = new StreamWriter(FileName);
            foreach (ConfigNode configNode in Nodes)
            {
                streamWriter.Write(configNode.ToString());
                streamWriter.Write(streamWriter.NewLine);
            }
            streamWriter.Close();

        }

    }
}