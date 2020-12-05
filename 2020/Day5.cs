using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day5
	{
		public Day5()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_5.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;
			}

			var lines = File.ReadAllLines(path);
			var higest = lines.Select(x =>
			{
				var row = x.Substring(0, 7);
				row = row.Replace('F', '0').Replace('B', '1');
				var rowInt = Convert.ToInt32(row, 2);

				var seat = x.Substring(7, 3);
				seat = seat.Replace('L', '0').Replace('R', '1');
				var seatInt = Convert.ToInt32(seat, 2);

				return rowInt * 8 + seatInt;
			}).Max();

			var seats = new char[128,8];

			for(var x = 0;x< 128; x++)
			{
				for(var y = 0; y <8; y++) 
				{
					seats[x, y] = '.';
				}
			}

			foreach(var line in lines)
			{
				var row = line.Substring(0, 7);
				row = row.Replace('F', '0').Replace('B', '1');
				var rowInt = Convert.ToInt32(row, 2);

				var seat = line.Substring(7, 3);
				seat = seat.Replace('L', '0').Replace('R', '1');
				var seatInt = Convert.ToInt32(seat, 2);

				seats[rowInt, seatInt] = 'X';
			}

			for (var x = 0; x < 128; x++)
			{
				var line = $"{x}\t";
				for (var y = 0; y < 8; y++)
				{
					line += seats[x, y] + " ";
				}
				Console.WriteLine(line);
			}

			Console.ReadLine();
		}
	}
}
