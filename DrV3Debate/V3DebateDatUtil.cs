using System.Text;
namespace DrV3Debate
{
    internal class V3DebateDatUtil
    {
        internal static string ReadDatString(BinaryReader reader)
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
    }
}
