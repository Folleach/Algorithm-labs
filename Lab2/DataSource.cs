using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Lab2
{
    public class DataSource
    {
        private readonly string path;
        private readonly string sortName;

        public DataSource(string path, string sortName)
        {
            this.path = path;
            this.sortName = sortName;
        }

        public Dictionary<string, ExperimentNode> GetExperimentNodes()
        {
            var xml = new XmlDocument();
            xml.Load(path);
            
            return GetExperimentNodes(GetXmlNodesFromExperiment(xml, sortName));
        }

        private Dictionary<string, ExperimentNode> GetExperimentNodes(XmlNodeList xml)
        {
            var result = new Dictionary<string, ExperimentNode>();
            foreach (XmlElement item in xml)
            {
                var node = ExperimentNode.FromXmlNode(item);
                result.Add(node.Name, node);
            }

            return result;
        }

        private XmlNodeList GetXmlNodesFromExperiment(XmlDocument doc, string sortName)
        {
            return doc.GetElementsByTagName("experiment")
                .Cast<XmlElement>()
                .FirstOrDefault(experiment => experiment.GetAttribute("name") == sortName)
                ?.GetElementsByTagName("node");
        }
    }
}