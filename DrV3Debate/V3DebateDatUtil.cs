using System.Text;
namespace DrV3Debate
{
    internal class V3DebateDatUtil
    {
        internal static string ReadDatString(BinaryReader reader) // Sorry for the mess, mostly for skipping padding/ zero bytes
        {
            List<byte> stringBytes = new List<byte>();
            while (reader.ReadByte() == 0 && reader.BaseStream.Position < reader.BaseStream.Length) { };
            reader.BaseStream.Position -= 1;
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                byte v = reader.ReadByte();
                if (v == 0)
                    break;
                stringBytes.Add(v);
            }
            if (stringBytes.Count == 0) return null;
            return Encoding.UTF8.GetString(stringBytes.ToArray());
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
        internal static long Get_Padding(long offset, int align = 0x16)
        {
            return ((align - (offset % align)) % align);
        }
    }
}
