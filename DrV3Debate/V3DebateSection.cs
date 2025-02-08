
namespace DrV3Debate
{

    public class V3DebateSection
    {
        public ushort ID_Dialouge;
        public ushort ID_Section;
        public List<ushort> Data = new List<ushort>();
        public V3DebateSection() { }
        public V3DebateSection(BinaryReader reader)
        {
            ID_Dialouge = BitConverter.ToUInt16(reader.ReadBytes(2));
            ID_Section = BitConverter.ToUInt16(reader.ReadBytes(2));
            Data.Add(ID_Dialouge);
            Data.Add(ID_Section);
            for (int i = 0; i < (200); i++)
            {
                byte[] buffer = reader.ReadBytes(2);
                if (buffer.Length < 2)
                    return;
                Data.Add(BitConverter.ToUInt16(buffer));
            }
        }

        public void ExportToString(StreamWriter writer, string prefix = "")
        {
            int unk = 1;
            for (ushort i = 0; i < Data.Count; i++)
            {
                writer.WriteLine($"{prefix}{(ValueNames.ContainsKey(i) ? ValueNames[i] : $"Unk{unk}")}: {Data[i]}");
                if (!ValueNames.ContainsKey(i)) { unk++; };
            }
        }
        public static V3DebateSection ReadFromString(StreamReader reader)
        {
            V3DebateSection section = new();
            section.ID_Dialouge = V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader));
            section.ID_Section = V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader));
            section.Data.Add(section.ID_Dialouge);
            section.Data.Add(section.ID_Section);
            for (ushort i=0;i<200;i++)
                section.Data.Add(V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader)));

            return section;
        }
        public Stream ToStream()
        {
            Stream stream = new MemoryStream();
            stream.Position = 0;
            foreach (var a in Data)
            {
                stream.Write(BitConverter.GetBytes(a));
            }
            return stream;
        }

        public static Dictionary<ushort, string> ValueNames = new()
        {
            [0] = "ID_Dialogue",
            [1] = "ID_Section",
            [2] = "ID_Chk",
            [3] = "Min_Difficulty",
            [5] = "Time_WhiteNoise_Recovery",
            [6] = "Text_Health",
            [8] = "NeededTruthBullet",
            [15] = "Reverse_Delay",
            [17] = "Transition_End_Timing",
            [19] = "Transition_End_Timing2",
            [171] = "ID_Char",
            [172] = "ID_Char_Anim",
        };
    }
}
