using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class iniParser
    {
        public static List<string> GetCharList()
        {
            List<string> chars = new List<string>();
            string dirName = "base/characters/";
            if (Directory.Exists(dirName))
            {
                foreach (string dir in Directory.EnumerateDirectories(dirName))
                {
                    if (File.Exists(dir + "/char.ini"))
                    {
                        using (var r = new StreamReader(dir + "/char.ini"))
                        {
                            while (!r.EndOfStream)
                            {
                                string line = r.ReadLine();
                                if (line.StartsWith("name", StringComparison.OrdinalIgnoreCase))
                                {
                                    chars.Add(line.Split(new string[] { " = " }, StringSplitOptions.None)[1]);
                                }
                            }
                        }
                    }
                }
                return chars;
            }
            return null;
        }

        public static List<string> GetMusicList()
        {
            List<string> songs = new List<string>();
            string dirName = "base/sounds/music/";
            if (Directory.Exists(dirName))
            {
                foreach (string file in Directory.EnumerateFiles(dirName))
                {
                    songs.Add(file.Substring(18));
                }
                return songs;
            }
            return null;
        }

        //Currently not called because it would be time-consuming and use lots of data and I haven't implemented evidence yet, so it's pointless
        public static List<byte> GetEvidenceList()
        {
            List<byte> evidence = new List<byte>();
            string dirName = "base/evidence/";
            if (Directory.Exists(dirName))
            {
                foreach (string dir in Directory.EnumerateDirectories(dirName))
                {
                    foreach (string file in Directory.EnumerateFiles(dir))
                    {
                        //TO DO (IMPORTANT!!!): ONLY SEND TEXT FILES AND IMAGES TO PREVENT THE TRANSFER OF MALICIOUS DATA!!!
                        using (var fs = new FileStream(file, FileMode.Open))
                        {
                            using (var b = new BinaryReader(fs))
                            {
                                evidence.AddRange(BitConverter.GetBytes((int)fs.Length));
                                evidence.AddRange(b.ReadBytes((int)fs.Length));
                            }
                        }
                    }
                }
                return evidence;
            }
            return null;
        }
    }
}
