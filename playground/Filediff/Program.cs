using System;
using System.Collections.Concurrent;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

namespace Filediff
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sourcePath = @"C:\vs-tinyos\console\bin\Debug\net8.0\linux-x64";
            var fileName = @"C:\vs-tinyos\console\bin\Debug\net8.0\project.rundiff.json";

            var files = GetFileInfo(sourcePath);
            var fileCache = ReadFile(fileName);
            SyncDirectory(files, fileCache);

            WriteFile(fileName, files);
        }

        private byte[] GetHash(string inputString)
        {
            using (var algorithm = SHA256.Create())
            {
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
            }
        }

        private string GetHashString(string inputString)
        {
            var sb = new StringBuilder("S", 65);
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));
            Console.WriteLine($"{inputString} => {sb.ToString()}");
            return sb.ToString();
        }


        static void WriteFile(string filename, List<FileCacheInfo> hashList)
        {
            try
            {
                var json = JsonSerializer.Serialize(hashList);
                File.WriteAllText(filename, json);
            }
            catch { }
        }

        static List<FileCacheInfo> ReadFile(string filename)
        {
            try
            {
                if (File.Exists(filename))
                {
                    var json = File.ReadAllText(filename);

                    var hashlist = JsonSerializer.Deserialize<List<FileCacheInfo>>(json);
                    if (hashlist != null)
                    {
                        return hashlist;
                    }
                }

                return new List<FileCacheInfo>();
            }
            catch
            {
                return new List<FileCacheInfo> ();
            }
        }

        static public void SyncDirectory(List<FileCacheInfo> files, List<FileCacheInfo> fileCache)
        {
            foreach (FileCacheInfo file in fileCache)
            {
                if (!files.Any(x => x.Name == file.Name))
                {
                    Console.WriteLine("Delete => " + file.Name);
                }
            }

            foreach (FileCacheInfo file in files)
            {
                if (!fileCache.Any(x => x.Hash.SequenceEqual(file.Hash)))
                {
                    Console.WriteLine("Copy => " + file.Name);
                }
            }
        }

        static public List<FileCacheInfo> GetFileInfo(string path)
        {
            var sourceDirectory = new DirectoryInfo(path);
            FileInfo[] fileList = sourceDirectory.GetFiles();

            ParallelOptions options = new ParallelOptions();
            options.MaxDegreeOfParallelism = 4;

            var fileHashList = new ConcurrentBag<FileCacheInfo>();

            Parallel.ForEach(fileList, options, file =>
            {
                using (var md5 = MD5.Create())
                {
                    var properties = new FileCacheInfo();
                    properties.Name = file.Name;

                    using (var stream = new BufferedStream(File.OpenRead(file.FullName), 1200000))
                    {
                        properties.Hash = md5.ComputeHash(stream);
                        fileHashList.Add(properties);
                    }
                }
            });

            return fileHashList.ToList();
        }   
    }
}
