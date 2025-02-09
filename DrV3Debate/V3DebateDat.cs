﻿using System.Text;

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
            UnknownValues = new ushort[5];

            stream.Position = 0;
            using BinaryReader reader = new BinaryReader(stream);

            Time = reader.ReadUInt16();

            ushort NumberOfSections = reader.ReadByte();
    
            for (byte i = 0; i < 5; i++)
                UnknownValues[i] = i==0 ? reader.ReadByte() : reader.ReadUInt16();

            for (ushort i = 0; i < NumberOfSections; i++) // Read Individual Dialogue Sections
                Sections.Add(new V3DebateSection(reader));

            while (reader.BaseStream.Position < reader.BaseStream.Length) // Read voice effect strings
            {
                string str = V3DebateDatUtil.ReadDatString(reader);
                if (str == null) break;
                VoiceEffects.Add(str);
            }
        }
        public void Extract(string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);

            writer.WriteLine($"--Header--");
            writer.WriteLine($"Time: {Time}");
            writer.WriteLine($"NumberOfSections: {Sections.Count}");

            for (byte i = 0; i < UnknownValues.Length; i++)
                writer.WriteLine($"UnknownValue{i}: {UnknownValues[i]}");

            for (ushort i = 0; i < Sections.Count; i++)
            {
                writer.WriteLine($"--Section {i} --");
                Sections[i].ExportToString(writer, "\t");
            }

            writer.WriteLine($"--Voice Effects--");
            for (int i = 0; i < VoiceEffects.Count; i++)
                writer.WriteLine($"{VoiceEffects[i]}");

            writer.Dispose();
            writer.Close();
        }

        public static void Repack(string filePath, string outPath = "")
        {
            if (outPath == "") outPath = filePath.Replace(".txt", ".dat");
            StreamReader reader = new StreamReader(filePath);
            FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write);
  
            //Time
            fs.Write(BitConverter.GetBytes(V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader))));

            //Number of sections
            byte numOfSections = V3DebateDatUtil.ReadStringByteValue(V3DebateDatUtil.ReadLine(reader));
            fs.WriteByte(numOfSections);

            //Unknown0 (Byte)
            fs.WriteByte(V3DebateDatUtil.ReadStringByteValue(V3DebateDatUtil.ReadLine(reader)));
            // Unknown 1-4(UShort)
            for (byte i = 0; i < 4; i++)
                fs.Write(BitConverter.GetBytes(V3DebateDatUtil.ReadStringUshortValue(V3DebateDatUtil.ReadLine(reader))));

            for (byte i=0; i<numOfSections; i++) // Text sections
            {
                var sectionStream = V3DebateSection.ReadFromString(reader).ToStream();
                sectionStream.CopyTo(fs);sectionStream.Dispose();sectionStream.Close();
            }
            while (!reader.EndOfStream) // Write all voice effect lines in
            {
                string line = V3DebateDatUtil.ReadLine(reader);
                if (line == null) break;
                if (line.StartsWith("--")) continue;
                var textbytes = Encoding.UTF8.GetBytes(line);
                Array.Resize(ref textbytes, 64);
                fs.Write(textbytes);
            }
            reader.Dispose();
            reader.Close();
            fs.Dispose();
            fs.Close();
        }

        public void Dispose()
        {
            stream.Dispose();
            stream.Close();
            Sections.Clear();
        }
    }

}
