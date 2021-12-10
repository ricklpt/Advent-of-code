using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day08 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day08");
            var data = lines.Select(x => x.Split('|')[1]);

            var sum = data.Select(x => Regex.Matches(x, @"(?<!\w)(\w{2}|\w{3}|\w{4}|\w{7})(?!\w)").Count).Sum();

            return sum.ToString();
        }


        public override string Part2()
        {
            var lines = ReadInput("Day08");
            var sum = 0;

            foreach (var line in lines)
            {
                var numbers = new Dictionary<string,int>();
                var letters = new Dictionary<int, string>();
                var splitted = line.Split('|');
                var options = splitted[0];

                //look for the 1
                letters.Add(1, Regex.Match(options, @"(?<!\w)(\w{2})(?!\w)").Value);
                letters.Add(4, Regex.Match(options, @"(?<!\w)(\w{4})(?!\w)").Value);
                letters.Add(7, Regex.Match(options, @"(?<!\w)(\w{3})(?!\w)").Value);
                letters.Add(8, Regex.Match(options, @"(?<!\w)(\w{7})(?!\w)").Value);

                var nr5s = Regex.Matches(options, @"(?<!\w)(\w{5})(?!\w)").Select(x => x.Value);
                var nr6s = Regex.Matches(options, @"(?<!\w)(\w{6})(?!\w)").Select(x => x.Value);

                var nr6 = nr6s.Single(x => !letters[1].All(y => x.Contains(y)));
                letters.Add(6,nr6);
                var nr9 = nr6s.Single(x => letters[4].All(y => x.Contains(y)));
                letters.Add(9,nr9);
                var nr0 = nr6s.Single(x => x != letters[6] && x != letters[9]);
                letters.Add(0,nr0);
                var nr3 = nr5s.Single(x => letters[1].All(y => x.Contains(y)));
                letters.Add(3,nr3);
                var nr5 = nr5s.Single(x => x.All(y => letters[6].Contains(y)));
                letters.Add(5,nr5);
                var nr2 = nr5s.Single(x => x != letters[3] && x != letters[5]);
                letters.Add(2,nr2);

                var answer = 0;
                var power = 1000;
                foreach (var match in Regex.Matches(splitted[1], @"\w+").Select(x => x.Value))
                {
                    var combination = letters.First(x=> x.Value.ToCharArray().Except(match.ToCharArray()).Count() == 0 && match.ToCharArray().Except(x.Value.ToCharArray()).Count() == 0 );
                    answer += combination.Key * power;
                    power /= 10;
                }

                sum += answer;
            }



            //var sum = data.Select(x => Regex.Matches(x, @"(?<!\w)(\w{2}|\w{3}|\w{4}|\w{7})(?!\w)").Count).Sum();

            return sum.ToString();
        }
    }
}
