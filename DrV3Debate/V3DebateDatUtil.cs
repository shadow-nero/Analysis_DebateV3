using System.Text;
namespace DrV3Debate
{
    internal class V3DebateDatUtil
    {
        internal static string ReadDatString(BinaryReader reader)
        {
            return Encoding.UTF8.GetString(reader.ReadBytes(64)).TrimEnd('\0');
        }
        internal static ushort ReadStringUshortValue(string inp)
        {
            if (inp == null || !inp.Contains(": ")) return 0;
            return ushort.Parse(inp.Split(": ")[1]);
        }
        internal static byte ReadStringByteValue(string inp)
        {
            if (inp == null || !inp.Contains(": ")) return 0;
            return byte.Parse(inp.Split(": ")[1]);
        }
        internal static string? ReadLine(StreamReader reader)
        {
            string? line = line = reader.ReadLine();
            while (reader.BaseStream.Position < reader.BaseStream.Length && line.StartsWith("--") && line != null)
                line = reader.ReadLine();
            return line;
        }
    }
}
