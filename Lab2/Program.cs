using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Lab2.Sorts;
using Lab2.Stores;

namespace Lab2
{
    public class Program
    {
        private static readonly string PathToData = "sources/data.xml";
        private static readonly string ResultPath = "results/";
        private static readonly string SortName = "Radix";

        private static readonly string ArithmeticProgression = "Arithmetic Progression";
        private static readonly string GeometricProgression = "Geometric Progression";
        
        private static readonly Dictionary<string, Func<double, double, int>> progressions = new Dictionary<string, Func<double, double, int>>()
        {
            { ArithmeticProgression, (current, diff) => (int)(current + diff) },
            { GeometricProgression, (current, diff) => (int)(current * diff) }
        };

        private static readonly IterationCounter iterationCounter = new IterationCounter();
        
        public static void Main(string[] args)
        {
            var xml = new XmlDocument();
            xml.Load(PathToData);
            var nodes = GetExperimentNodes(GetXmlNodesFromExperiment(xml, SortName));
            
            ExecuteNode(nodes[ArithmeticProgression], new FileStore(ResultPath, ArithmeticProgression));
            
            ExecuteNode(nodes[GeometricProgression], new FileStore(ResultPath, GeometricProgression));
            
            Console.WriteLine($"Results has been write to {ResultPath}");
        }

        private static void ExecuteNode(ExperimentNode experiment, IStore store)
        {
            var alphabet = CreateAlphabet(experiment.MinElement, experiment.MaxElement);
            var random = new Random();
            var currentLength = experiment.StartLength;
            
            while (currentLength <= experiment.MaxLength)
            {
                var texts = new string[experiment.Repeat];
                for (var i = 0; i < experiment.Repeat; i++)
                    texts[i] = random.NextString(currentLength, alphabet);
                
                Radix.Sort(new FakeArray<string>(texts, iterationCounter));
                store.Store(currentLength.ToString(), iterationCounter);
                
                currentLength = progressions[experiment.Name](currentLength, experiment.Diff);
            }
        }

        private static char[] CreateAlphabet(char from, char to)
        {
            var list = new List<char>();
            for (var i = from; i <= to; i++)
                list.Add(i);
            return list.ToArray();
        }

        private static XmlNodeList GetXmlNodesFromExperiment(XmlDocument doc, string sortName)
        {
            return doc.GetElementsByTagName("experiment")
                .Cast<XmlElement>()
                .FirstOrDefault(experiment => experiment.GetAttribute("name") == sortName)
                ?.GetElementsByTagName("node");
        }

        private static Dictionary<string, ExperimentNode> GetExperimentNodes(XmlNodeList xml)
        {
            var result = new Dictionary<string, ExperimentNode>();
            foreach (XmlElement item in xml)
            {
                var node = ExperimentNode.FromXmlNode(item);
                result.Add(node.Name, node);
            }

            return result;
        }
    }
}