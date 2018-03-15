using System;
using System.IO;
using System.Runtime.InteropServices;

namespace RemoveSmallAudioFiles
{
    internal class Program
    {
        private const long SizeInKBytes = 500;
        private static string _startingDirectory = string.Empty;
        private static long _filesRemoved = 0;

        private static void Main(string[] args)
        {
            _startingDirectory = Directory.GetCurrentDirectory();
            Console.Title = $"Scanning {_startingDirectory}";

            foreach (var dir in Directory.GetDirectories(_startingDirectory)) { DoLoop(dir); }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"\n{_filesRemoved} file/s removed");
        }

        private static void DoLoop(string directoryToScan)
        {
            if (!Directory.Exists(directoryToScan)) return;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"*{directoryToScan.Substring(_startingDirectory.Length)}\\");

            CheckFiles(directoryToScan);
            foreach (var subDir in Directory.GetDirectories(directoryToScan)) { DoLoop(subDir); }
        }

        private static void CheckFiles(string directoryToCheck)
        {
            if (!Directory.Exists(directoryToCheck)) return;

            foreach (var file in Directory.GetFiles(directoryToCheck))
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.Length > 1024 * SizeInKBytes) continue;
                File.Delete(file);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"*{file.Substring(_startingDirectory.Length)}");
                _filesRemoved += 1;
            }
        }
    }
}
