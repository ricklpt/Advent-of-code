using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day13 : BaseDay
    {
        int maxX = 0;
        int maxY = 0;
        int sum = 0;
        public override string Part1()
        {
            var lines = ReadInputToSingleString("Day13");

            var coords = Regex.Matches(lines, @"(?<x>\d+),(?<y>\d+)").Select(x => new Tuple<int, int>(Convert.ToInt32(x.Groups["x"].Value), Convert.ToInt32(x.Groups["y"].Value)));
            maxX = coords.Select(x => x.Item1).Max();
            maxY = coords.Select(x => x.Item2).Max();

            var array = new string[maxX + 1, maxY + 1];
            foreach (var coord in coords)
            {
                array[coord.Item1, coord.Item2] = "#";
            }

            // for (int y = 0; y < maxY; y++)
            // {
            //     //Console.WriteLine(lines[x]);
            //     for (int x = 0; x < maxX; x++)
            //     {
            //         Console.Write($" {array[x, y]}");
            //         //Console.Write(lines[x].ToCharArray()[y]);
            //     }
            //     Console.Write("\n");

            // }
            // Console.WriteLine("");

            var instructions = Regex.Matches(lines, @"(?<direction>\w+)=(?<amount>\d+)").Select(x => new Tuple<string, int>(x.Groups["direction"].Value, Convert.ToInt32(x.Groups["amount"].Value)));

            foreach (var instruction in instructions)
            {
                Console.WriteLine("------------------------");
                // var instruction = instructions.First();
                array = Fold(array, instruction.Item1, instruction.Item2);

            }

                for (int y = 0; y <= maxY; y++)
                {
                    // Console.WriteLine(lines[x]);
                    for (int x = 0; x <= maxX; x++)
                    {
                        if(array[x,y] == "#") sum++;
                        Console.Write($" {array[x, y]}");
                        // Console.Write(lines[x].ToCharArray()[y]);
                    }
                    Console.Write("\n");

                }
                Console.WriteLine("");

                return sum.ToString();
        }

        private string[,] Fold(string[,] array, string direction, int lineNR)
        {
            if (direction == "y")
            {
                var newY = Math.Max(lineNR - 0, maxY - (maxY - lineNR));

                var newArray = new string[maxX + 1, newY];
                for (int y = 0; y <= maxY; y++)
                {
                    for (int x = 0; x <= maxX; x++)
                    {
                        if (y < newY)
                        {
                            newArray[x, y] = array[x, y];
                        }
                        else if (y != lineNR)
                        {
                            var difference = y - lineNR;
                            var offset = difference * 2;
                            var newInternalY = y - offset;
                            if (newInternalY >= 0)
                            {
                                if (array[x, y] == "#" || array[x, newInternalY] == "#")
                                    newArray[x, newInternalY] = "#";
                                else
                                    newArray[x, newInternalY] = ".";
                                //newArray[newInternalY, y] = array[x,y];
                            }
                        }

                    }

                }

                maxY = newY - 1;
                return newArray;
            }
            else
            {
                var newX = Math.Max(lineNR - 0, maxX - (maxX - lineNR));

                var newArray = new string[newX, maxY +1];
                for (int y = 0; y <= maxY; y++)
                {
                    for (int x = 0; x <= maxX; x++)
                    {
                        if (x < newX)
                        {
                            newArray[x, y] = array[x, y];
                        }
                        else if (x != lineNR)
                        {
                            var difference = x - lineNR;
                            var offset = difference * 2;
                            var newInternalX = x - offset;
                            if (newInternalX >= 0)
                            {
                                if (array[x, y] == "#" || array[newInternalX, y] == "#")
                                    newArray[newInternalX, y] = "#";
                                else
                                    newArray[newInternalX, y] = ".";
                                //newArray[newInternalY, y] = array[x,y];
                            }
                        }

                    }

                }

                maxX = newX - 1;
                return newArray;


            }
        }

        public override string Part2()
        {
            return "";
        }
    }
}
