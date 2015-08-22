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
    }
}
