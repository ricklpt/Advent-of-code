using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day07 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day07");
            var data = lines[0].Split(',').ToList().Select(x => Convert.ToInt32(x));
            var lowestFuel = 9999999;

            for (int i = data.Min() ; i <= data.Max(); i++)
            {
                var fuelNeeded = data.Select(x => Math.Abs(x - i )).Sum();

                if(fuelNeeded < lowestFuel)
                    lowestFuel = fuelNeeded;
            }

            return lowestFuel.ToString();
        }


        public override string Part2()
        {
            var lines = ReadInput("Day07");
            var data = lines[0].Split(',').ToList().Select(x => Convert.ToInt64(x));
            var lowestFuel = (long)999999999999;

            for (int i = (int)data.Min() ; i <= data.Max(); i++)
            {
                var fuelNeeded = data.Select(x => Math.Abs(x - i ) * (Math.Abs(x - i) + 1) / 2 ).Sum();

                if(fuelNeeded < lowestFuel)
                    lowestFuel = fuelNeeded;
            }

            return lowestFuel.ToString();
        }
    }
}
