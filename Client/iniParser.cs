using System;
using System.IO;

namespace Client
{
	public static class iniParser
	{
		public static string GetPreAnim(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var count = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
						{
							count = Convert.ToInt32(line.Split(new[] { " = " }, StringSplitOptions.None)[1]);
							continue;
						}

						if (count > 0 && line.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							var parseThis = line.Split(new[] { " = " }, StringSplitOptions.None)[1];
							var animData = parseThis.Split('#');
							if (animData[3] == "1")
							{
								return animData[1];
							}
							return null;
						}
					}
				}
				return null;
			}
			return null;
		}

		public static int GetPreAnimTime(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) &&
				File.Exists(dirName + "char.ini") & anim > 0 & GetPreAnim(charName, anim) != null)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var length = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (length <= 0 && line.StartsWith(GetPreAnim(charName, anim), StringComparison.OrdinalIgnoreCase))
						{
							length = Convert.ToInt32(line.Split(new[] { " = " }, StringSplitOptions.None)[1]);
							return length;
						}
					}
				}
				return 0;
			}
			return 0;
		}

		public static int GetSoundTime(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim > 0)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (line.StartsWith("[SoundT]", StringComparison.OrdinalIgnoreCase))
						{
							while (!r.EndOfStream)
							{
								var line2 = r.ReadLine();
								if (line2.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
								{
									return Convert.ToInt32(line2.Split(new[] { " = " }, StringSplitOptions.None)[1]);
								}
							}
						}
					}
				}
				return 0;
			}
			return 0;
		}

		public static string GetSoundName(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim > 0)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (line.StartsWith("[SoundN]", StringComparison.OrdinalIgnoreCase))
						{
							while (!r.EndOfStream)
							{
								var line2 = r.ReadLine();
								if (line2.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
								{
									return line2.Split(new[] { " = " }, StringSplitOptions.None)[1];
								}
							}
						}
					}
				}
				return "1";
			}
			return "1";
		}

		public static string GetAnim(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var count = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
						{
							count = Convert.ToInt32(line.Split(new[] { " = " }, StringSplitOptions.None)[1]);
							continue;
						}

						if (count > 0 && line.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							var parseThis = line.Split(new[] { " = " }, StringSplitOptions.None)[1];
							var animData = parseThis.Split('#');
							if (animData[3] == "0" | animData[3] == "1")
							{
								return animData[2];
							}
							if (animData[3] == "5")
							{
								return animData[2];
							}
						}
					}
				}
				return null;
			}
			return null;
		}

		public static int GetAnimType(string charName, int anim)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini") & anim >= 0)
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var count = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
						{
							count = Convert.ToInt32(line.Split(new[] { " = " }, StringSplitOptions.None)[1]);
							continue;
						}

						if (count > 0 && line.StartsWith(anim.ToString(), StringComparison.OrdinalIgnoreCase))
						{
							var parseThis = line.Split(new[] { " = " }, StringSplitOptions.None)[1];
							var animData = parseThis.Split('#');
							return Convert.ToInt32(animData[3]);
						}
					}
				}
				return 0;
			}
			return 0;
		}

		public static int GetEmoNum(string charName)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini"))
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var count = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (count <= 0 && line.StartsWith("number", StringComparison.OrdinalIgnoreCase))
						{
							count = Convert.ToInt32(line.Split(new[] { " = " }, StringSplitOptions.None)[1]);
							return count;
						}
					}
				}
			}
			return 0;
		}

		public static string GetSide(string charName)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini"))
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					var count = 0;
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (count <= 0 && line.StartsWith("side", StringComparison.OrdinalIgnoreCase))
						{
							return line.Split(new[] { " = " }, StringSplitOptions.None)[1];
						}
					}
				}
				return "def";
			}
			return "def";
			;
		}

		public static string GetDispName(string charName)
		{
			var dirName = "base/characters/" + charName + "/";
			if (charName != null && Directory.Exists(dirName) && File.Exists(dirName + "char.ini"))
			{
				using (var r = new StreamReader(dirName + "char.ini"))
				{
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (line.StartsWith("showname", StringComparison.OrdinalIgnoreCase))
						{
							return line.Split(new[] { " = " }, StringSplitOptions.None)[1];
						}
					}
				}
			}
			return null;
		}

		public static string GetMasterIP()
		{
			if (File.Exists("base/masterserver.ini"))
			{
				using (var r = new StreamReader("base/masterserver.ini"))
				{
					while (!r.EndOfStream)
					{
						var line = r.ReadLine();
						if (line.StartsWith("[list]", StringComparison.OrdinalIgnoreCase))
						{
							while (!r.EndOfStream)
							{
								var line2 = r.ReadLine().Split(new[] { " = " }, StringSplitOptions.RemoveEmptyEntries);
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

	public class Animation
	{
		public string anim;
		public int index;
		public string preAnim;
		public int preAnimTime;
		public string soundName;
		public int soundTime;
	}
}