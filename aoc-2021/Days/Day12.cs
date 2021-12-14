using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day12 : BaseDay
    {
        private int traverseRoute2(string start, IEnumerable<Tuple<string, string>> directions, IEnumerable<string> takenNodes, string routeSoFar, Dictionary<string, int> smallCaves, bool programmstart = false)
        {
            var directInstructions = directions.Where(x => x.Item1 == start);
            if (routeSoFar == ",start,A,b,A,c,A")
            {
                Console.Write("");
            }
            routeSoFar = $"{routeSoFar},{start}";
            if (!programmstart && start == "start")
            {
                return 0;
            }
            if (takenNodes.Any(x => start == x) || !directInstructions.Any())
            {
                // Console.WriteLine($"Found a dead end {start} already taken");
                return 0;
            }
            if (start == "end")
            {
                Console.WriteLine($"{routeSoFar}");
                return 1;
            }
            var internalSmallCaves = new Dictionary<string, int>(smallCaves);

            var newList = new List<string>();
            if(start == "c")
            {
                Console.Write("");
            }
            if (Char.IsLower(start.ToCharArray()[0]))
            {
                var fuelUsed = internalSmallCaves.Any(c => c.Key != "start" && c.Value > 1);
                if (fuelUsed && internalSmallCaves.ContainsKey(start) && internalSmallCaves[start] > 0)
                {
                    return 0;
                }
                else
                {
                    if (internalSmallCaves.ContainsKey(start))
                        internalSmallCaves[start] = internalSmallCaves[start] + 1;
                    else
                        internalSmallCaves.Add(start, 1);
                }
            }

            int sumSoFar = (directInstructions.Select(x => traverseRoute2(x.Item2, directions, takenNodes.Union(newList), routeSoFar, internalSmallCaves)).Sum());
            // Console.WriteLine($"start => {start}\t, destinations => {String.Join(',', directInstructions.Select(x => x.Item2))} sumOfDestinations =>{sumSoFar}\t");
            return sumSoFar;
        }
        private int traverseRoute(string start, IEnumerable<Tuple<string, string>> directions, IEnumerable<string> takenNodes)
        {
            var directInstructions = directions.Where(x => x.Item1 == start);

            if (takenNodes.Any(x => start == x) || !directInstructions.Any())
            {
                // Console.WriteLine($"Found a dead end {start} already taken");
                return 0;
            }
            if (start == "end")
            {
                // Console.WriteLine($"End found {start}");
                return 1;
            }

            var newList = new List<string>();
            if (Char.IsLower(start.ToCharArray()[0]))
            {
                newList.Add(start);

            }

            int sumSoFar = (directInstructions.Select(x => traverseRoute(x.Item2, directions, takenNodes.Union(newList))).Sum());
            // Console.WriteLine($"start => {start}\t, destinations => {String.Join(',', directInstructions.Select(x => x.Item2))} sumOfDestinations =>{sumSoFar}\t");
            return sumSoFar;
        }
        public override string Part1()
        {
            var lines = ReadInput("Day12");
            var instructions = lines.Select(x => new Tuple<string, string>(x.Split('-')[0], x.Split('-')[1]));
            var reversed = instructions.Select(x => new Tuple<string, string>(x.Item2, x.Item1));
            instructions = instructions.Union(reversed);
            return traverseRoute("start", instructions, new List<string>()).ToString();
        }
        public override string Part2()
        {
            var lines = ReadInput("Day12");
            var instructions = lines.Select(x => new Tuple<string, string>(x.Split('-')[0], x.Split('-')[1]));
            var reversed = instructions.Select(x => new Tuple<string, string>(x.Item2, x.Item1));
            instructions = instructions.Union(reversed).ToList();
            return traverseRoute2("start", instructions, new List<string>(), "", new Dictionary<string, int>(), true).ToString();
        }
    }
}
