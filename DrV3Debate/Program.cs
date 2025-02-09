using DrV3Debate;
public partial class Program
{
    private static void ProcessFile(string filePath, string outPath = "")
    {
        if (filePath.EndsWith(".txt"))
        {
            Console.WriteLine("Repacking dat file...");
            V3DebateDat.Repack(filePath, outPath.Replace(".txt", ".dat"));
            Console.WriteLine("Repacked dat file...");
            return;
        }
        Console.WriteLine("Exporting Dat to TXT...");
        V3DebateDat dat = new(filePath);
        dat.Extract(outPath == "" ? filePath.Replace(".dat", ".txt") : outPath.Replace(".dat", ".txt"));
        dat.Dispose();
        Console.WriteLine("Finished Exporting to TXT!");
    }
    private static void Loop(string filePath, string outPath ="") 
    {
        if (Directory.Exists(filePath))
        {
            if (outPath != "") { Directory.CreateDirectory(outPath); }
            foreach (var subFile in Directory.GetFiles(filePath))
                ProcessFile(subFile, outPath != "" ? Path.Combine(outPath, Path.GetFileName(subFile)) : "");
            return;
        }
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"File {filePath} does not exist!");
            return;
        }
        ProcessFile(filePath, outPath);
    }

    public static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            Loop(args[0], args.Length > 1 ? args[1] : "");
            return;
        }
        while (true)
        {
            Console.WriteLine("Drag and drop file to extract");
            Loop(Console.ReadLine().Replace("\"", ""));
        }
    }
}
