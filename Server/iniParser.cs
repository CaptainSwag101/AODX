using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static List<Evidence> GetEvidenceData()
        {
            List<Evidence> evidence = new List<Evidence>();
            string dirName = "base/cases/";
            if (Directory.Exists(dirName))
            {
                foreach (string dir in Directory.EnumerateDirectories(dirName))
                {
                    if (dir.Substring(11) == GetServerInfo().Split('|')[9])
                    {
                        foreach (string file in Directory.EnumerateFiles(dir))
                        {
                            using (var fs = new FileStream(file, FileMode.Open))
                            {
                                Evidence evi = new Evidence();
                                evi.filename = file.Split('.').First();
                                switch (file.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last().Split('/').Last().Split('.').Last())
                                {
                                    case "gif":
                                    case "png":
                                    case "bmp":
                                        evi.icon = Image.FromFile(file);

                                        bool found = false;
                                        foreach (Evidence data in evidence)
                                        {
                                            if (data.filename == evi.filename)
                                            {
                                                data.icon = evi.icon;
                                                found = true;
                                                break;
                                            }
                                        }

                                        if (!found)
                                        {
                                            evi.index = evidence.Count;
                                            evidence.Add(evi);
                                        }

                                        break;

                                    case "ini":
                                        if (file.Split('\\').Last() == "case.ini")
                                            break;

                                        //load ini data into name and desc
                                        using (StreamReader r = new StreamReader(fs))
                                        {
                                            while (!r.EndOfStream)
                                            {
                                                string line = r.ReadLine();
                                                if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("name", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    evi.name = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                }
                                                else if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("desc", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    evi.desc = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                }
                                                else if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("note", StringComparison.OrdinalIgnoreCase))
                                                {
                                                    evi.note = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                }
                                            }
                                        }

                                        bool found2 = false;
                                        foreach (Evidence data in evidence)
                                        {
                                            if (data.filename == evi.filename)
                                            {
                                                data.name = evi.name;
                                                data.desc = evi.desc;
                                                data.note = evi.note;
                                                found2 = true;
                                                break;
                                            }
                                        }

                                        if (!found2)
                                        {
                                            evi.index = evidence.Count;
                                            evidence.Add(evi);
                                        }

                                        break;
                                }
                            }
                        }

                        foreach (string dir2 in Directory.EnumerateDirectories(dir))
                        {
                            foreach (string file in Directory.EnumerateFiles(dir2))
                            {
                                using (var fs = new FileStream(file, FileMode.Open))
                                {
                                    Evidence evi = new Evidence();
                                    evi.filename = file.Split('.').First();
                                    switch (file.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Last().Split('/').Last().Split('.').Last())
                                    {
                                        case "gif":
                                        case "png":
                                        case "bmp":
                                            evi.icon = Image.FromStream(fs, false, true);

                                            evi.icon.Save("base/test3.gif");

                                            //if (System.Drawing.Imaging.ImageFormat.Gif.Equals(evi.icon.RawFormat))
                                                //MessageBox.Show("GIF");
                                            //else if (System.Drawing.Imaging.ImageFormat.Png.Equals(evi.icon.RawFormat))
                                                //MessageBox.Show("PNG");

                                            bool found = false;
                                            foreach (Evidence data in evidence)
                                            {
                                                if (data.icon != null)
                                                    data.icon.Save("base/test4.gif");
                                                if (data.filename == evi.filename)
                                                {
                                                    data.icon = evi.icon;
                                                    data.icon.Save("base/test4.gif");
                                                    found = true;
                                                    break;
                                                }
                                            }

                                            if (!found)
                                            {
                                                evi.index = evidence.Count;
                                                evidence.Add(evi);
                                            }

                                            evi = null;

                                            break;

                                        case "ini":
                                            //load ini data into name and desc
                                            using (StreamReader r = new StreamReader(fs))
                                            {
                                                while (!r.EndOfStream)
                                                {
                                                    string line = r.ReadLine();
                                                    if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("name", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        evi.name = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                    }
                                                    else if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("desc", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        evi.desc = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                    }
                                                    else if (line.Split(new string[] { " = " }, StringSplitOptions.None)[0].StartsWith("note", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        evi.note = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                                    }
                                                }
                                            }

                                            bool found2 = false;
                                            foreach (Evidence data in evidence)
                                            {
                                                if (data.icon != null)
                                                    data.icon.Save("base/test4.gif");
                                                if (data.filename == evi.filename)
                                                {
                                                    data.name = evi.name;
                                                    data.desc = evi.desc;
                                                    data.note = evi.note;
                                                    found2 = true;

                                                    data.icon.Save("base/test4.gif");
                                                    break;
                                                }
                                            }

                                            if (!found2)
                                            {
                                                evi.index = evidence.Count;
                                                evidence.Add(evi);
                                            }

                                            evi = null;
                                            break;
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
