using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
	public class Day4
	{
		public Day4()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_4.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;				
			}

			var lines = File.ReadAllLines(path);
			var results = new List<string>();
			var tempresult = string.Empty;

			foreach(var line in lines)
			{
				if(string.IsNullOrEmpty(line))
				{
					results.Add(tempresult);
					tempresult = string.Empty;
				}
				else
				{
					tempresult += " ";
					tempresult += line;
				}			
			}
			if (!string.IsNullOrEmpty(tempresult))
			{
				results.Add(tempresult);
			}

			Console.WriteLine(results.Where(isValidPassport2).Count());

		}	
		
		private bool isValidPassport(string passport)
		{
			var matches = Regex.Matches(passport, @"\w+:[a-zA-Z0-9#]+");
			var keys = matches.Select(x => x.Value.Split(':')[0]);
			var valid = keys.Contains("byr") 
				&& keys.Contains("iyr") 
				&& keys.Contains("eyr")
				&& keys.Contains("hgt") 
				&& keys.Contains("hcl") 
				&& keys.Contains("ecl") 
				&& keys.Contains("pid");
			return valid;		
		}

		private bool isValidPassport2(string passport)
		{
			var byr = Regex.Match(passport, @"byr:(?<value>\d{4})( |$)");
			var iyr = Regex.Match(passport, @"iyr:(?<value>\d{4})( |$)");
			var eyr = Regex.Match(passport, @"eyr:(?<value>\d{4})( |$)");
			var hgt = Regex.Match(passport, @"hgt:(?<value1>\d+)(?<value2>cm|in)");
			var hcl = Regex.Match(passport, @"hcl:#[0-9a-f]{6}( |$)");
			var ecl = Regex.Match(passport, @"ecl:(?<value>[a-zA-Z0-9#]+)( |$)");
			var pid = Regex.Match(passport, @"pid:\d{9}( |$)");

			var byrValue = !byr.Success ? 0 : Convert.ToInt32(byr.Groups["value"].Value);
			var iyrValue = !iyr.Success ? 0 : Convert.ToInt32(iyr.Groups["value"].Value);
			var eyrValue = !eyr.Success ? 0 : Convert.ToInt32(eyr.Groups["value"].Value);
			var hgtValue1 = !hgt.Success ? 0 : Convert.ToInt32(hgt.Groups["value1"].Value);
			var hgtValue2 = hgt.Groups["value2"].Value;
			var eclValue = ecl.Groups["value"].Value;

			var validByr = (byr.Success && byrValue >= 1920 && byrValue <= 2002);
			var validIyr = (iyr.Success && iyrValue >= 2010 && iyrValue <= 2020);
			var validEyr = (eyr.Success && eyrValue >= 2020 && eyrValue <= 2030);
			var validHgt = (hgt.Success && (hgtValue2 == "cm" && hgtValue1 >= 150 && hgtValue1 <= 193) || (hgtValue2 == "in" && hgtValue1 >= 59 && hgtValue1 <= 76));
			var validHcl = (hcl.Success);
			var validEcl = (ecl.Success && "amb blu brn gry grn hzl oth".Contains(eclValue));
			var validPid = (pid.Success);
				
			return validByr && validIyr && validEyr && validHgt && validHcl && validEcl && validPid;
		}
	}
}
