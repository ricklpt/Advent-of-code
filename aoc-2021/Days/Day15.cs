using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day15 : BaseDay
    {
        public class Tile
        {
            public int X { get; set; }
            public int Y { get; set; }

            public int Cost { get; set; }
            public int Distance { get; set; }

            public int CostDistance => Cost + Distance;

            public Tile Parent { get; set; }

            public void SetDistance(int targetX, int targetY)
            {
                this.Distance = Math.Abs(targetX - X) + Math.Abs(targetY - Y);
            }

            public bool IsSameTile(Tile x)
            {
                return X == x.X && Y == x.Y;
            }
        }
        int maxX = 0;
        int maxY = 0;
        int sum = 0;
        public string Solve(string[] lines)
        {
            var start = new Tile { Y = 0, X = 0 };
            var finish = new Tile { Y = lines.Length - 1, X = lines[0].Length - 1 };
            start.SetDistance(finish.X, finish.Y);

            var distanceMeter = finish.Distance;

            var activeTiles = new List<Tile>();
            activeTiles.Add(start);
            var visitedTiles = new List<Tile>();


            while (activeTiles.Any())
            {
                var checkTile = activeTiles.OrderBy(x => x.CostDistance).First();

                if (checkTile.X == finish.X && checkTile.Y == finish.Y)
                {
                    WriteTile(checkTile, lines.ToList());
                    return "Found!";
                }

                visitedTiles.Add(checkTile);
                activeTiles.Remove(checkTile);

                var nextTiles = GetNextTiles(lines, checkTile, finish);

                foreach (var nextTile in nextTiles)
                {
                    if (visitedTiles.Any(x => x.IsSameTile(nextTile)))
                    {
                        continue;
                    }

                    var existingTile = activeTiles.FirstOrDefault(x => nextTile.IsSameTile(x));

                    if (existingTile != null)
                    {
                        if (existingTile.Parent.Cost > checkTile.Cost)
                        {
                            activeTiles.Remove(existingTile);
                            activeTiles.Add(nextTile);

                            //Console.WriteLine($"From {checkTile.X},{checkTile.Y} to {nextTile.X},{nextTile.Y} Better than existing");
                        }
                        else
                        {
                            // Console.WriteLine($"From {checkTile.X},{checkTile.Y} to {nextTile.X},{nextTile.Y} Drop worse than existing");
                        }
                    }
                    else
                    {
                        // Console.WriteLine($"From {checkTile.X},{checkTile.Y} to {nextTile.X},{nextTile.Y} never seen before");
                        activeTiles.Add(nextTile);
                    }
                }
                // Console.WriteLine("");
            }

            return "NoPath found";

        }
        public override string Part1()
        {
            var lines = ReadInput("Day15");
            return Solve(lines);
        }

        private void WriteTile(Tile tile, List<string> map)
        {
            // Console.WriteLine("Retracing steps");
            Console.WriteLine($"Total risk: {tile.Cost}");

            while (true)
            {
                // Console.WriteLine($"{tile.X} : {tile.Y}");

                var newMap = map[tile.Y].ToCharArray();
                newMap[tile.X] = '*';
                map[tile.Y] = new string(newMap);

                tile = tile.Parent;
                if (tile == null)
                {
                    // map.ForEach(x => Console.WriteLine(x));

                    return;
                }
            }
        }

        private List<Tile> GetNextTiles(string[] map, Tile current, Tile target)
        {
            var tilesPossible = new List<Tile>();
            tilesPossible.Add(new Tile { X = current.X, Y = current.Y - 1, Parent = current });
            tilesPossible.Add(new Tile { X = current.X, Y = current.Y + 1, Parent = current });
            tilesPossible.Add(new Tile { X = current.X - 1, Y = current.Y, Parent = current });
            tilesPossible.Add(new Tile { X = current.X + 1, Y = current.Y, Parent = current });

            tilesPossible.ForEach(tile => tile.SetDistance(target.X, target.Y));

            var maxX = map[0].Length - 1;
            var maxY = map.Length - 1;

            tilesPossible = tilesPossible
                        .Where(x => x.X >= 0 && x.X <= maxX)
                        .Where(x => x.Y >= 0 && x.Y <= maxY)
                        .ToList();

            tilesPossible.ForEach(tile =>
            {
                var weight = Convert.ToInt32(map[tile.Y].ToCharArray()[tile.X].ToString());
                tile.Cost = current.Cost + weight;
            });

            return tilesPossible.ToList();
        }

        public override string Part2()
        {
            var lines = ReadInput("Day15");

            var maxX = lines[0].Length * 5;
            var maxY = lines.Length * 5;

            var map = new string[maxX, maxY];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    int output = Convert.ToInt32(lines[y % 10].ToCharArray()[x % 10].ToString());
                    int distance = (y / 10) + (x / 10);
                    // int output2 = (output + multiplier);
                    map[x, y] =  ((output + distance -1 ) % 9 +1).ToString(); //output2.ToString();
                }
            }

            // for (int y = 0; y < maxY; y++)
            // {
            //     if (y % 10 == 0)
            //         Console.WriteLine(" ");
            //     for (int x = 0; x < maxX; x++)
            //     {
            //         if (x % 10 == 0)
            //             Console.Write("\t");
            //         Console.Write(map[x, y]);
            //     }
            //     Console.Write("\n");
            // }

            var lines2 = new string[maxY];
            for (int y = 0; y < maxY; y++)
            {
                var line = "";
                for (int x = 0; x < maxX; x++)
                {
                    line += map[x, y];
                }
                lines2[y] = line;
            }
            return Solve(lines2);
        }
    }
}
