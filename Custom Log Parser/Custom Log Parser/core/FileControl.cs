using Microsoft.VisualBasic;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Custom_Log_Parser.core
{
    public class FileControl
    {
        public string FilePath { get; }
        public FileControl (string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("File path should not be null or empty"); 
            if (!Path.IsPathFullyQualified(filePath))
                throw new ArgumentException("File path should be qualified");
            this.FilePath = filePath;
        }
        public async Task ParseIntoAsync(Collection<LogItem> collection, ProgressControl progressControl)
        {
            progressControl.Init(File.ReadAllLines(FilePath).Length);
            collection.Clear();

            using FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BufferedStream bs = new BufferedStream(fs);
            using StreamReader sr = new StreamReader(bs);
            string line;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                progressControl.SetProgressState(1);

                if (line.EndsWith("-----"))
                    continue;
                if (!line.Contains("ApiPZU.Logging.LogManager"))
                {
                    if (collection.Count() > 0)
                        collection.Last().Content += Environment.NewLine + line;
                    continue;
                }
                collection.Add(new LogItem(line));
            }
        }
        public static FileControl FromDialog()
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt"
            };

            if (!openFileDialog.ShowDialog().Value)
                return null;

            return new FileControl(openFileDialog.FileName);
        }
    }
}
