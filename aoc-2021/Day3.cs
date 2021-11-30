using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day3
	{
		public Day3()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_3.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;				
			}

			var lines = File.ReadAllLines(path);
			var map = readMapFile(lines);
			var mapWidth = lines[0].Length;
			var mapHeight = lines.Length;
			var trees = traverseMap(map, mapWidth, mapHeight);

			var results2 = Convert.ToInt64(traverseMap2(map, mapWidth, mapHeight, 1, 1));
			results2 *= Convert.ToInt64(traverseMap2(map, mapWidth, mapHeight, 3, 1));
			results2 *= Convert.ToInt64(traverseMap2(map, mapWidth, mapHeight, 5, 1));
			results2 *= Convert.ToInt64(traverseMap2(map, mapWidth, mapHeight, 7, 1));
			results2 *= Convert.ToInt64(traverseMap2(map, mapWidth, mapHeight, 1, 2));
			Console.WriteLine(trees);
			Console.WriteLine(results2);
		}

		private char[,] readMapFile(string[] file)
		{
			var lines = file.Length;
			var colums = file[0].Length;

			var array = new char[lines, colums];

			for(var x = 0; x < lines;x++)
			{
				var chars = file[x].ToCharArray();
				for(var y= 0; y < colums;y++)
				{
					array[x, y] = chars[y];
				}
			}
			return array;
		}

		private int traverseMap(char[,] map, int mapWidth, int mapHeight)
		{
			var trees = 0;
			var positionX = 0;
			var positionY = 0;

			while (positionY < mapHeight -1)
			{
				positionX = (positionX + 3) % mapWidth;
				positionY += 1;

				trees += (map[positionY, positionX] == '#') ? 1 : 0;				
			}

			return trees;
		}

		private int traverseMap2(char[,] map, int mapWidth, int mapHeight, int stepsX = 3, int stepsY = 1,int trees = 0, int positionX = 0, int positionY = 0)
		{			
			positionX = (positionX + stepsX) % mapWidth;
			positionY += stepsY;

			if (positionY >= mapHeight)
				return trees;

			var treefound = (map[positionY, positionX] == '#') ? 1 : 0;

			return traverseMap2(map, mapWidth, mapHeight, stepsX, stepsY, trees + treefound, positionX, positionY);
		}

		
	}
}
