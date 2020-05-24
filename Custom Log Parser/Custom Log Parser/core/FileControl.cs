using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
        public async Task<List<LogItem>> ParseAsync(ProgressControl progressControl)
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var lineLength = File.ReadAllLines(FilePath).Length;
            LogItem[] tmpArr = new LogItem[lineLength];
            progressControl.Init(lineLength);

            using FileStream fs = File.Open(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            using BufferedStream bs = new BufferedStream(fs);
            using StreamReader sr = new StreamReader(bs);
            string line;
            int count = 0;
            while ((line = await sr.ReadLineAsync()) != null)
            {
                progressControl.SetProgressState(1);

                if (line.EndsWith("-----"))
                    continue;
                if (!line.Contains("ApiPZU.Logging.LogManager"))
                {
                    if (count > 0)
                        tmpArr[count - 1].Content += Environment.NewLine + line;
                    continue;
                }
                tmpArr[count] = new LogItem(line);
                count++;
            }
            sw.Stop();
            Debug.WriteLine($"ParseIntoAsync time {sw.Elapsed}");
            return tmpArr[..count].ToList();
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
