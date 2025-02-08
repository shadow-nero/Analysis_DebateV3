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

        V3DebateDat dat = new(filePath);
        dat.Extract(filePath.Replace(".dat", ".txt"));
    }
}
