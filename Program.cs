using static System.IO.File;

namespace lesson_8_sem_1;

internal class Program
{
    private static string Res { get; set; } = "";
    private static int Count { get; set; }

    private static void Main(string[] args)
    {
        if (args.Length != 3) return;
        Console.WriteLine($"Поиск файлов с расширением {args[1]} в папке {args[0]} с текстом {args[2]}");
        SearchTextInFileMethod(args[0], args[1], args[2]);
        Console.Write(Res);
        Console.WriteLine($"Количество совпадений: {Count}");
    }

    private static void SearchTextInFileMethod(string dir, string f, string searchText)
    {
        var allFileAndDir = Directory.EnumerateFileSystemEntries(dir);
        foreach (var var in allFileAndDir)
        {
            var isDirectory = GetAttributes(var).HasFlag(FileAttributes.Directory);
            var ext = new FileInfo(var).Extension;
            if (isDirectory)
            {
                SearchTextInFileMethod(var, f, searchText);
            }
            else
            {
                if (f != ext.Replace(".", "")) continue;
                using var fileStream = new StreamReader(var);
                var fileToEnd = fileStream.ReadToEnd();
                if (!fileToEnd.Contains(searchText)) continue;
                Res += var + "\n";
                Count++;
            }
        }
    }
}