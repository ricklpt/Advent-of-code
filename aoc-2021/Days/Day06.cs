using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day06 : BaseDay
    {
        public override string Part1()
        {
            var data = ReadInput("Day06");
            var days = 81;
            var fish = data[0].Split(',').Select(x => Convert.ToInt32(x)).ToList();

            //printState(fish, 0);
            for (int day = 1; day < days; day++)
            {
                var amountNewFish = fish.Count(x => x == 0);
                fish = fish.Select(x => x == 0 ? 6 : x - 1).ToList();
                for (int i = 0; i < amountNewFish; i++)
                {
                    fish.Add(8);
                }

                //printState(fish, day);
            }

            return fish.Count.ToString();
        }

        private void printState(IEnumerable<int> fish, int days)
        {
            Console.WriteLine($"After\t {days} day(s): {string.Join(',', fish)}");
        }


        public override string Part2()
        {
            var data = ReadInput("Day06");
            var days = 257;
            var fish = data[0].Split(',').Select(x => Convert.ToInt64(x)).ToList();

            var amount8Fish = (long)fish.Count(x => x == 8);
            var amount7Fish = (long)fish.Count(x => x == 7);
            var amount6Fish = (long)fish.Count(x => x == 6);
            var amount5Fish = (long)fish.Count(x => x == 5);
            var amount4Fish = (long)fish.Count(x => x == 4);
            var amount3Fish = (long)fish.Count(x => x == 3);
            var amount2Fish = (long)fish.Count(x => x == 2);
            var amount1Fish = (long)fish.Count(x => x == 1);
            var amount0Fish = (long)fish.Count(x => x == 0);

            //printState(fish, 0);
            for (int day = 1; day < days; day++)
            {
                var amountNewFish = amount0Fish;
                amount0Fish = amount1Fish;
                amount1Fish = amount2Fish;
                amount2Fish = amount3Fish;
                amount3Fish = amount4Fish;
                amount4Fish = amount5Fish;
                amount5Fish = amount6Fish;
                amount6Fish = amount7Fish + amountNewFish;
                amount7Fish = amount8Fish;
                amount8Fish = amountNewFish;
                var totalAmount = amount0Fish +amount1Fish +amount2Fish +amount3Fish +amount4Fish +amount5Fish +amount6Fish +amount7Fish +amount8Fish;

                Console.WriteLine($"day: {day.ToString()} amount: {totalAmount}");
            }

            return fish.Count.ToString();
        }
    }
}
