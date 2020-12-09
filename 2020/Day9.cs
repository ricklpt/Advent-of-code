using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day9
	{
		public Day9()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_09.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;
			}

			var lines = File.ReadAllLines(path).Select(x => Convert.ToInt64(x)).ToArray();
			var preambleSize = 25;
			var weakness = 0l;
			//Part1
			for(int i = preambleSize; i < lines.Length;i++)
			{
				var answer = lines[i];
				var found = false;
				for (int y = i-1; y >= 0 && y >= (i - preambleSize); y--){
					for(int y2 = i-1; y2 >= 0 && y2 >= (i- preambleSize); y2--){
						if (y == y2)
							continue;
						
						found = (lines[y] + lines[y2]) == answer;
						//Console.WriteLine($"\t {lines[y]}({y}) {lines[y2]}({y2}) {answer} {found}");

						if (found) break;
					}
					if (found) break;
				}

				if (weakness == 0 && !found)
				{
					weakness = lines[i];
				}

				Console.WriteLine($"{lines[i]}({i}) is found {found}");
			}

			//part 2
			var setSize = 2;
			IEnumerable<long> setToEvaluate = new List<long>();
			var cursor = 0;

			do
			{
				if (cursor > lines.Length - setSize)
				{
					cursor = 0;
					setSize++;
				}
				Console.WriteLine($"{cursor},{setSize}");
				setToEvaluate = lines.Skip(cursor).Take(setSize);
				cursor++;
			}
			while (setToEvaluate.Sum() != weakness);

			var answer2 = setToEvaluate.Min() + setToEvaluate.Max();

		}

		

	}
}
