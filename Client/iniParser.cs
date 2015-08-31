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

        public static int GetPreAnimTime(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim > 0)
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    int length = 0;
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (length <= 0 && line.StartsWith(GetPreAnim(charName, anim), StringComparison.OrdinalIgnoreCase))
                        {
                            length = Convert.ToInt32(line.Split(new string[] { " = " }, StringSplitOptions.None)[1]);
                            return length;
                        }
                    }
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public static int GetSoundTime(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim > 0)
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (line.StartsWith("[SoundT]", StringComparison.OrdinalIgnoreCase))
                        {
                            while (!r.EndOfStream)
                            {
                                string line2 = r.ReadLine();
                                if (line2.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    return Convert.ToInt32(line2.Split(new string[] { " = " }, StringSplitOptions.None)[1]);

                                }
                            }
                        }
                    }
                }
                return 0;
            }
            else
            {
                return 0;
            }
        }

        public static string GetSoundName(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim > 0)
            {
                using (var r = new StreamReader(dirName + "char.ini"))
                {
                    while (!r.EndOfStream)
                    {
                        string line = r.ReadLine();
                        if (line.StartsWith("[SoundN]", StringComparison.OrdinalIgnoreCase))
                        {
                            while (!r.EndOfStream)
                            {
                                string line2 = r.ReadLine();
                                if (line2.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
                                {
                                    return line2.Split(new string[] { " = " }, StringSplitOptions.None)[1];
                                    
                                }
                            }
                        }
                    }
                }
                return "1";
            }
            else
            {
                return "1";
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
                            else if (animData[3] == "5")
                            {
                                return animData[2];
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

        public static int GetAnimType(string charName, int anim)
        {
            string dirName = "base/characters/" + charName + "/";
            if(charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
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
                            return Convert.ToInt32(animData[3]);
                        }
                    }
                }
                return 0;
            }
            else
            {
                return 0;
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
