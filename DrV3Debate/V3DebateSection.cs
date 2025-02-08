
namespace DrV3Debate
{

    public class V3DebateSection
    {
        public ushort ID_Dialouge;
        public ushort ID_Section;
        public ushort ID_CHK; // Minimum_Difficulty?
        public List<ushort> Data = new List<ushort>();
        public V3DebateSection() { }
        public V3DebateSection(BinaryReader reader)
        {
            ID_Dialouge = BitConverter.ToUInt16(reader.ReadBytes(2));
            ID_Section = BitConverter.ToUInt16(reader.ReadBytes(2));
            ID_CHK = BitConverter.ToUInt16(reader.ReadBytes(2));
            Data.Add(ID_Dialouge);
            Data.Add(ID_Section);
            Data.Add(ID_CHK);
            for (int i = 0; i < (199); i++)
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

        public static Dictionary<ushort, string> ValueNames = new()
        {
            [0] = "ID_Dialogue",
            [1] = "ID_Section",
            [2] = "ID_Chk",
            [3] = "Min_Difficulty",
            //4
            [5] = "Time_WhiteNoise_Recovery",
            [6] = "Text_Health",
            //7
            [8] = "NeededTruthBullet",
            //9
            //10
            //11
            //12
            //13
            //14
            [15] = "Reverse_Delay",
            //16
            [17] = "Transition_End_Timing",
            //18
            [19] = "Transition_End_Timing2",
            //20
            //21
            //22
            [171] = "ID_Char",
            [172] = "ID_Char_Anim",



        };
    }
}
