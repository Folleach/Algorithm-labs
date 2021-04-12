using System.Globalization;
using System.Xml;

namespace Lab2
{
    public class ExperimentNode
    {
        public string Name { get; set; }
        public char MinElement { get; set; }
        public char MaxElement { get; set; }
        public int StartLength { get; set; }
        public double Diff { get; set; }
        public int MaxLength { get; set; }
        public int Repeat { get; set; }

        public static ExperimentNode FromXmlNode(XmlElement xml)
        {
            var node = new ExperimentNode();
            node.Name = xml.GetAttribute("name");
            node.MinElement = char.Parse(xml.GetAttribute("minElement"));
            node.MaxElement = char.Parse(xml.GetAttribute("maxElement"));
            node.StartLength = int.Parse(xml.GetAttribute("startLength"));
            node.Diff = double.Parse(xml.GetAttribute("diff"), CultureInfo.InvariantCulture);
            node.MaxLength = int.Parse(xml.GetAttribute("maxLength"));
            node.Repeat = int.Parse(xml.GetAttribute("repeat"));
            
            return node;
        }
    }
}