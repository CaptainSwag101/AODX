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
        public static string GetServerInfo()
        {
            string result = "A New Server|Server Description|1|1000|||1|"; //Name|Description|public|port|password|oppassword|musicmode|case
            if (File.Exists("base/settings.ini"))
            {
                string name = "";
                string desc = "";
                string isPublic = "";
                string port = "";
                string password = "";
                string oppassword = "";
                string musicmode = "";
                string loop = "";
                string useCase = "";
                string caseName = "";

                using (var r = new StreamReader("base/settings.ini"))
                {
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (line.StartsWith("name", StringComparison.OrdinalIgnoreCase))
                        {
                            name = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("desc", StringComparison.OrdinalIgnoreCase))
                        {
                            desc = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("public", StringComparison.OrdinalIgnoreCase))
                        {
                            isPublic = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("port", StringComparison.OrdinalIgnoreCase))
                        {
                            port = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("password", StringComparison.OrdinalIgnoreCase))
                        {
                            password = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("oppassword", StringComparison.OrdinalIgnoreCase))
                        {
                            oppassword = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("musicmode", StringComparison.OrdinalIgnoreCase))
                        {
                            musicmode = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("loopmusic", StringComparison.OrdinalIgnoreCase))
                        {
                            loop = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("usecase", StringComparison.OrdinalIgnoreCase))
                        {
                            useCase = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }

                        if (line.StartsWith("case", StringComparison.OrdinalIgnoreCase))
                        {
                            caseName = line.Split(new string[] { " = " }, StringSplitOptions.None)[1] + "|";
                        }
                    }

                    result = name + desc + isPublic + port + password + oppassword + musicmode + loop + useCase + caseName;
                }
            }
            return result;
        }

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
        public static List<EvidenceFile> GetEvidenceData()
        {
            List<EvidenceFile> evidence = new List<EvidenceFile>();
            string dirName = "base/cases/";
            if (Directory.Exists(dirName))
            {
                foreach (string dir in Directory.EnumerateDirectories(dirName))
                {
                    if (dir.Substring(11) == GetServerInfo().Split('|')[9])
                    {
                        foreach (string file in Directory.EnumerateFiles(dir))
                        {
                            //TO DO (IMPORTANT!!!): ONLY SEND TEXT FILES AND IMAGES TO PREVENT THE TRANSFER OF MALICIOUS DATA!!!
                            using (var fs = new FileStream(file, FileMode.Open))
                            {
                                using (var b = new BinaryReader(fs))
                                {
                                    EvidenceFile evi = new EvidenceFile();
                                    evi.filename = file.Replace('\\', '/');
                                    evi.data = b.ReadBytes((int)fs.Length);
                                    evi.size = evi.data.Length;
                                    evidence.Add(evi);
                                }
                            }
                        }
                        foreach (string dir2 in Directory.EnumerateDirectories(dir))
                        {
                            foreach (string file in Directory.EnumerateFiles(dir2))
                            {
                                //TO DO (IMPORTANT!!!): ONLY SEND TEXT FILES AND IMAGES TO PREVENT THE TRANSFER OF MALICIOUS DATA!!!
                                using (var fs = new FileStream(file, FileMode.Open))
                                {
                                    using (var b = new BinaryReader(fs))
                                    {
                                        EvidenceFile evi = new EvidenceFile();
                                        evi.filename = file.Replace('\\', '/');
                                        evi.data = b.ReadBytes((int)fs.Length);
                                        evi.size = evi.data.Length;
                                        evidence.Add(evi);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return evidence;
        }

        public static string GetMasterIP()
        {
            if (File.Exists("base/masterserver.ini"))
            {
                using (var r = new StreamReader("base/masterserver.ini"))
                {
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (line.StartsWith("[list]", StringComparison.OrdinalIgnoreCase))
                        {
                            while (!r.EndOfStream)
                            {
                                string[] line2 = r.ReadLine().Split(new string[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
                                if (line2.Length > 1)
                                {
                                    return line2[1];
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
