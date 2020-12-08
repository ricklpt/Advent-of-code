using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day6
	{
		readonly string nl = Environment.NewLine;

		public Day6()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_06.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;
			}

			var lines = File.ReadAllText(path).Split($"{nl}{nl}");
			
			var count = 0;

			foreach(var line in lines)
			{
				count += line.Replace(nl, "").ToCharArray().Distinct().Count();
			}

			var count2 = 0;
			foreach (var line in lines)
			{
				var ppl = line.Split(nl).Select(x => x.ToCharArray());
				IEnumerable<char> result = ppl.SelectMany(x => x).Distinct();
				foreach(var person in ppl)
				{
					result = result.Intersect(person);
				}
				count2 += result.Count();
			}


		}

	}
}
