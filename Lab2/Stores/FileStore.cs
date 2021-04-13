using System.IO;

namespace Lab2.Stores
{
    public class FileStore : IStore
    {
        private string destination;
        
        public FileStore(string directory, string filename)
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            destination = Path.Combine(directory, $"{filename}.txt");
            File.WriteAllText(destination, $"{filename}:\n");
        }
        
        public void Store(string title, IterationCounter counter)
        {
            File.AppendAllText(destination, $"{title}\t{counter.Flush()}\n");
        }
    }
}