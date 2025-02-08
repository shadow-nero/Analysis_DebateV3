using DrV3Debate;
public partial class Program
{
    public static void Main(string[] args)
    {
        string filePath = "";
        if (args.Length > 0 )
            filePath = args[0];
        else
        {
            Console.WriteLine("Drag and drop file to extract");
            filePath = Console.ReadLine().Replace("\"", "");
        }
        if (filePath.EndsWith(".txt"))
        {
            Console.WriteLine("Repacking dat file...");
            V3DebateDat.Repack(filePath); // UNFINISHED, PADDING NOT IMPLEMENTED FOR VOICE EFFECT STRINGS
            Console.WriteLine("Repacked dat file...");
            return;
        }
        Console.WriteLine("Exporting Dat to TXT...");
        V3DebateDat dat = new(filePath);
        dat.Extract(filePath.Replace(".dat", ".txt"));
        Console.WriteLine("Done!");
    }
}
