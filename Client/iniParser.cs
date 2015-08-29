using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public static class iniParser
    {
        public static string GetPreAnim(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    int count = 0;
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
                        {
                            count = Convert.ToInt32(line.Split(new string[] { " = " }, StringSplitOptions.None)[1]);
                            continue;
                        }

                        if (count > 0 && line.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            string parseThis = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                            string[] animData = parseThis.Split('#');
                            if (animData[3] == "1")
                            {
                                return animData[1];
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public static string GetAnim(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    int count = 0;
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
                        {
                            count = Convert.ToInt32(line.Split(new string[] { " = " }, StringSplitOptions.None)[1]);
                            continue;
                        }

                        if (count > 0 && line.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
                        {
                            string parseThis = line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                            string[] animData = parseThis.Split('#');
                            if (animData[3] == "0" | animData[3] == "1")
                            {
                                return animData[2];
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

        public static int GetEmoNum(string charName)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini"))
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    int count = 0;
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
                        {
                            count = Convert.ToInt32(line.Split(new string[] { " = " }, StringSplitOptions.None)[1]);
                            return count;
                        }
                    }
                }
            }
            return 0;
        }

        public static string GetSide(string charName)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini"))
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    int count = 0;
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (count <= 0 && line.StartsWith("side", StringComparison.OrdinalIgnoreCase))
                        {
                            return line.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                        }
                    }
                }
                return "def";
            }
            else
            {
                return "def"; ;
            }
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
