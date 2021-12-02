using System;
using System.Text.RegularExpressions;

namespace aoc_2021
{
    public class Day02 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day02");

            var depth = 0;
            var position = 0;
            var regex = new Regex(@"(?<direction>forward|down|up) (?<amount>\d+)");

            foreach (var line in lines)
            {
                var match = regex.Match(line);

                if (match.Success)
                {
                    var direction = match.Groups["direction"].Value;
                    var amount = match.Groups["amount"].Value;

                    if (direction == "forward")
                    {
                        position += Convert.ToInt32(amount);
                    }

                    if (direction == "down")
                    {
                        depth += Convert.ToInt32(amount);
                    }

                    if (direction == "up")
                    {
                        depth -= Convert.ToInt32(amount);
                    }
                }
            }

            Console.WriteLine($"Depth = {depth}");
            Console.WriteLine($"Position = {position}");
            return (depth * position).ToString();
        }

        public override string Part2()
        {
            var lines = ReadInput("Day02");

            var depth = 0;
            var position = 0;
            var aim = 0;

            var regex = new Regex(@"(?<direction>forward|down|up) (?<amount>\d+)");

            foreach (var line in lines)
            {
                var match = regex.Match(line);

                if (match.Success)
                {
                    var direction = match.Groups["direction"].Value;
                    var amount = match.Groups["amount"].Value;

                    if (direction == "forward")
                    {
                        position += Convert.ToInt32(amount);
                        depth += (Convert.ToInt32(amount) * aim);
                    }

                    if (direction == "down")
                    {
                        aim += Convert.ToInt32(amount);
                    }

                    if (direction == "up")
                    {
                        aim -= Convert.ToInt32(amount);
                    }
                }
            }

            Console.WriteLine($"Depth = {depth}");
            Console.WriteLine($"Position = {position}");
            return (depth * position).ToString();
        }
    }
}
