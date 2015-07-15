using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionMergerV2
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ShowHelp();
                return;
            }
            var initialPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string result = "";
            foreach (var mask in args)
            {
                result += GetFilesRecursive(initialPath, string.Format("*.{0}",mask)) + Environment.NewLine + Environment.NewLine;
            }
            Console.WriteLine("Writing to output.txt");
            File.WriteAllText("output.txt", result, Encoding.GetEncoding("windows-1251"));
            Console.WriteLine("Completed!");
        }

        static string GetFilesRecursive(string path, string mask)
        {
            string result = "";
            var directory = new DirectoryInfo(path);
            foreach (var file in directory.GetFiles(mask))
            {
                Console.WriteLine("Processing file {0}", file.FullName);
                result += "File " + file.Name + Environment.NewLine;
                result += File.ReadAllText(file.Name, Encoding.GetEncoding("windows-1251"));
                result += Environment.NewLine + Environment.NewLine;
            }

            foreach (var innerDirectory in directory.GetDirectories())
            {
                result += GetFilesRecursive(innerDirectory.FullName, mask);
            }
            return result;
        }

        static void ShowHelp()
        {
            Console.WriteLine("Usage: SolutionMerverV2.exe [file extensions]");
            Console.WriteLine("Example: SolutionMerverV2.exe cpp h cs");
        }
    }
}
