using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day11 : BaseDay
    {
        int[,] array = null;
        bool[,] alreadyFlashed = null;

        int maxX = 0;
        int maxY = 0;
        int sum = 0;
        public override string Part1()
        {
            var lines = ReadInput("Day11");
            maxX = lines.Count();
            maxY = lines[0].Count();

            array = new int[maxX, maxY];
            alreadyFlashed = new bool[maxX, maxY];

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    //Console.Write(lines[x].ToCharArray()[y]);
                    array[x, y] = Convert.ToInt32(lines[x].ToCharArray()[y].ToString());
                }
            }

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    Console.Write($" {array[x, y]}");
                    //Console.Write(lines[x].ToCharArray()[y]);
                }
                Console.Write("\n");

            }
            Console.WriteLine("");

            for (int steps = 0; steps < 100; steps++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        //Console.Write(lines[x].ToCharArray()[y]);
                        array[x, y] += 1;
                        alreadyFlashed[x,y] = false;
                    }

                }

                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        if (array[x, y] > 9)
                            Flash(x, y);
                        //Console.Write(lines[x].ToCharArray()[y]);
                    }
                }
                var resetSum = 0;
                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        if (alreadyFlashed[x, y])
                        {
                            resetSum++;
                            array[x, y] = 0;
                        }
                    }
                }
                if(resetSum == 100)
                    Console.WriteLine($"Step found {steps}");

                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        Console.Write($" {array[x, y]}");
                        //Console.Write(lines[x].ToCharArray()[y]);
                    }
                    Console.Write("\n");

                }
                Console.WriteLine("");
            }

            return sum.ToString();
        }

        private void Flash(int x, int y)
        {
            array[x, y] += 1;

            if (array[x, y] <= 9 || alreadyFlashed[x, y])
                return;

            alreadyFlashed[x, y] = true;
            sum++;

            if (x > 0 && y > 0)
                Flash(x - 1, y - 1);

            if (x > 0)
                Flash(x - 1, y);

            if (x > 0 && y < maxY - 1)
                Flash(x - 1, y + 1);

            if (y > 0)
                Flash(x, y - 1);

            if (y < maxY - 1)
                Flash(x, y + 1);

            if (x < maxX - 1 && y > 0)
                Flash(x + 1, y - 1);

            if (x < maxX - 1)
                Flash(x + 1, y);

            if (x < maxX - 1 && y < maxY - 1)
                Flash(x + 1, y + 1);

        }

        public override string Part2()
        {

            var lines = ReadInput("Day11");
            maxX = lines.Count();
            maxY = lines[0].Count();

            array = new int[maxX, maxY];
            alreadyFlashed = new bool[maxX, maxY];
            bool finished = false;

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    //Console.Write(lines[x].ToCharArray()[y]);
                    array[x, y] = Convert.ToInt32(lines[x].ToCharArray()[y].ToString());
                }
            }

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    Console.Write($" {array[x, y]}");
                    //Console.Write(lines[x].ToCharArray()[y]);
                }
                Console.Write("\n");

            }
            Console.WriteLine("");
            var steps = 0;
            while(!finished)
            {
                steps++;
                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        //Console.Write(lines[x].ToCharArray()[y]);
                        array[x, y] += 1;
                        alreadyFlashed[x,y] = false;
                    }

                }

                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        if (array[x, y] > 9)
                            Flash(x, y);
                        //Console.Write(lines[x].ToCharArray()[y]);
                    }
                }
                var resetSum = 0;
                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        if (alreadyFlashed[x, y])
                        {
                            resetSum++;
                            array[x, y] = 0;
                        }
                    }
                }
                if(resetSum == 100)
                {
                    finished = true;
                    Console.WriteLine($"Step found {steps}");
                }

                for (int x = 0; x < maxX; x++)
                {
                    //Console.WriteLine(lines[x]);
                    for (int y = 0; y < maxY; y++)
                    {
                        Console.Write($" {array[x, y]}");
                        //Console.Write(lines[x].ToCharArray()[y]);
                    }
                    Console.Write("\n");

                }
                Console.WriteLine("");
            }

            return sum.ToString();
        }
    }
}
