namespace DrV3Debate
{
    public class V3DebateSection
    {
        public ushort ID_Dialouge { get { return Data[0]; } }

        public List<ushort> Data = new List<ushort>();
        public V3DebateSection() { }
        public V3DebateSection(BinaryReader reader)
        {
            for (int i = 0; i < 202; i++)
                Data.Add(BitConverter.ToUInt16(reader.ReadBytes(2)));
        }

        public void ExportToString(StreamWriter writer, string prefix = "")
        {
            for (ushort i = 0; i < Data.Count; i++)
                writer.WriteLine($"{prefix}{(ValueNames.ContainsKey(i) ? ValueNames[i] : $"Unk{i-4}")}: {Data[i]}");
        }
        public static V3DebateSection ReadFromString(StreamReader reader)
        {
            V3DebateSection section = new();

            for (ushort i=0;i<202;i++)
                section.Data.Add(V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader)));

            return section;
        }
        public Stream ToStream()
        {
            Stream stream = new MemoryStream();
            foreach (var a in Data)
                stream.Write(BitConverter.GetBytes(a));
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
