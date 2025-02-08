using System.Reflection;
using System.Text;
namespace DrV3Debate
{
    public class V3DebateDat
    {
        private Stream stream;

        public ushort Time;
        public ushort[] UnknownValues;
        public List<V3DebateSection> Sections = new List<V3DebateSection>();
        public List<string> VoiceEffects = new List<string>();
        public V3DebateDat(Stream s) 
        {
            stream = s;
            ReadHeader();
        }
        public V3DebateDat(string path)
        {
            stream = File.OpenRead(path);
            ReadHeader();
        }

        private void ReadHeader()
        {
            Sections.Clear();
            VoiceEffects.Clear();
            stream.Position = 0;
            using BinaryReader reader = new BinaryReader(stream);

            Time = reader.ReadUInt16();

            ushort NumberOfSections = reader.ReadByte();


            UnknownValues = new ushort[5];

            for (byte i = 0; i < 5; i++)
                UnknownValues[i] = reader.ReadUInt16();

            for (ushort i = 0; i < NumberOfSections; i++)
                Sections.Add(new V3DebateSection(reader));

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                string str = V3DebateDatUtil.ReadDatString(reader);
                if (str == null) break;
                VoiceEffects.Add(str);//Encoding.UTF8.GetString(reader.ReadBytes(64)));//.TrimEnd('\0'));
            }
        }
        public void Extract(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine($"Time: {Time}");
            writer.WriteLine($"NumberOfSections: {Sections.Count}");
            for (byte i = 0; i < UnknownValues.Length; i++)
                writer.WriteLine($"UnknownValue{i}: {UnknownValues[i]}");
            for (ushort i = 0; i < Sections.Count; i++)
            {
                var section = Sections[i];
                writer.WriteLine($"--Section {i} --");
                writer.WriteLine($"     DialougeId: {section.DialogueId}");
                writer.WriteLine($"     SectionId: {section.SectionId}");
                writer.WriteLine($"     DifficultyId: {section.Difficulty}");
                foreach (var a in section.UnknownData)
                    writer.WriteLine($"     Unknown: {a}");
            }
            writer.WriteLine($"--Voice Effects--");
            for (int i=0; i< VoiceEffects.Count; i++)
                writer.WriteLine($"{i}: {VoiceEffects[i]}");
            writer.Dispose();
            writer.Close();
        }

    }

}
