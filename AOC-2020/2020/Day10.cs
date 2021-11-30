using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day10
	{
		public Day10()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_10.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{

				return;
			}

			var lines = File.ReadAllLines(path).Select(x => Convert.ToInt32(x)).ToArray();
			var sorted = lines.OrderBy(x => x).ToList();
			var result1 = new Dictionary<int,int>();
			result1.Add(1, 0);
			result1.Add(2, 0);
			result1.Add(3, 1);
			
			for(int i =0;i< sorted.Count - 1; i++)
			{
				if(i == 0)
				{
					result1[sorted[i]] += 1;
				}
				result1[sorted[i + 1] - sorted[i]] += 1;
			}

			var answer1 = result1[1] * result1[3];

			//Part2
			var result2 = new Dictionary<int, int>();
			foreach(var line in sorted)
			{
				result2.Add(line, lines.Where(x => x > line && x <= line + 3).Count());
			}

			var answer2 = result2.Values.Where(x => x > 1).Sum();

			var answer2b = countArrangements(lines, 0);
		}

		long countArrangements(int[] allNumbers, int startnumber)
		{
			var nearest = allNumbers.Where(x => x > startnumber && x <= startnumber + 3);

			if (startnumber == allNumbers.Max())
				return 1;

			if (answers.ContainsKey(startnumber))
				return answers[startnumber];

			var answer = nearest.Select(x => countArrangements(allNumbers, x)).Sum();


			answers.Add(startnumber, answer);
			Debug.WriteLine(startnumber);
			return answer;
		}

		Dictionary<int, long> answers = new Dictionary<int, long>();
		

	}
}
