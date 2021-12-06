using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Line
    {
        public int x1 { get; set; }
        public int y1 { get; set; }
        public int x2 { get; set; }
        public int y2 { get; set; }
    }
    public class Day05 : BaseDay
    {

        public override string Part1()
        {
            var data = ReadInput("Day05");
            var regex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");
            var lines = new List<Line>();

            foreach (var line in data)
            {
                var match = regex.Match(line);
                lines.Add(new Line
                {
                    x1 = Convert.ToInt32(match.Groups["x1"].Value),
                    y1 = Convert.ToInt32(match.Groups["y1"].Value),
                    x2 = Convert.ToInt32(match.Groups["x2"].Value),
                    y2 = Convert.ToInt32(match.Groups["y2"].Value),
                });
            }

            var maxX = lines.Select(x => Math.Max(x.x1, x.x2)).Max();
            var maxY = lines.Select(y => Math.Max(y.y1, y.y2)).Max();

            var grid = new int[maxX + 1, maxY + 1];

            foreach (var line in lines)
            {
                drawLine(line, grid);
            }

            return printGrid(grid).ToString();
        }

        private void drawLine(Line line, int[,] grid)
        {
            if (line.x1 != line.x2 && line.y1 != line.y2)
                return;

            var startX = Math.Min(line.x1, line.x2);
            var endX = Math.Max(line.x1, line.x2);
            var startY = Math.Min(line.y1, line.y2);
            var endY = Math.Max(line.y1, line.y2);

            for (int x = startX; x <= endX; x++)
            {
                for (int y = startY; y <= endY; y++)
                {
                    grid[x, y]++;
                }
            }
            
        }

        private void drawLine2(Line line, int[,] grid)
        {
            var cursorX = line.x1;
            var cursorY = line.y1;

            var directionX = Math.Min(1, Math.Max(-1, (line.x2 - line.x1)));
            var directionY = Math.Min(1, Math.Max(-1, (line.y2 - line.y1)));

            grid[cursorX,cursorY]++;

            do
            {
                cursorX += directionX;
                cursorY += directionY;

                grid[cursorX,cursorY]++;

            } while (cursorX != line.x2 || cursorY != line.y2);
        }

        private int printGrid(int[,] input)
        {
            var count = 0;
            Console.WriteLine(" 0  1  2  3  4  5  6  7  8  9");
            for (int y = 0; y < input.GetLength(1); y++)
            {
                for (int x = 0; x < input.GetLength(0); x++)
                {
                    var output = input[x, y] != 0 ? input[x,y].ToString() : ".";
                    Console.Write($" {output} ");
                    if(input[x,y] > 1)
                        count ++;
                }
                Console.Write($"\t\t{y}\n");

            }

            return count;
        }
        public override string Part2()
        {
            var data = ReadInput("Day05");
            var regex = new Regex(@"(?<x1>\d+),(?<y1>\d+) -> (?<x2>\d+),(?<y2>\d+)");
            var lines = new List<Line>();

            foreach (var line in data)
            {
                var match = regex.Match(line);
                lines.Add(new Line
                {
                    x1 = Convert.ToInt32(match.Groups["x1"].Value),
                    y1 = Convert.ToInt32(match.Groups["y1"].Value),
                    x2 = Convert.ToInt32(match.Groups["x2"].Value),
                    y2 = Convert.ToInt32(match.Groups["y2"].Value),
                });
            }

            var maxX = lines.Select(x => Math.Max(x.x1, x.x2)).Max();
            var maxY = lines.Select(y => Math.Max(y.y1, y.y2)).Max();

            var grid = new int[maxX + 1, maxY + 1];

            foreach (var line in lines)
            {
                drawLine2(line, grid);
            }

            return printGrid(grid).ToString();
        }
    }
}
