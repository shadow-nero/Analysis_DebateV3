
namespace DrV3Debate
{
    public class V3DebateSection
    {
        public ushort DialogueId;
        public ushort SectionId;
        public ushort Difficulty;
        public List<ushort> UnknownData = new List<ushort>();
        public V3DebateSection() { }
        public V3DebateSection(BinaryReader reader)
        {
            DialogueId = BitConverter.ToUInt16(reader.ReadBytes(2));
            SectionId = BitConverter.ToUInt16(reader.ReadBytes(2));
            Difficulty = BitConverter.ToUInt16(reader.ReadBytes(2));
            for (int i = 0; i < 199; i++)
            {
                byte[] buffer = reader.ReadBytes(2);
                if (buffer.Length < 2)
                    return;
                UnknownData.Add(BitConverter.ToUInt16(buffer));
            }
        }
    }
}
