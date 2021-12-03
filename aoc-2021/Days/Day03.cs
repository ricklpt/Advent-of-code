using System;
using System.Text.RegularExpressions;
using System.Linq;

namespace aoc_2021
{
    public class Day03 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day03");

            var gammaRateArray = new char[lines[0].Length];
            var epsilonRateArray = new char[lines[0].Length]; ;

            for (int i = 0; i < lines[0].Length; i++)
            {
                var chars = lines.Select(x => x.ToCharArray()[i]);

                gammaRateArray[i] = (chars.Count(x => x == '0') > chars.Count(x => x == '1')) ? '0' : '1';
                epsilonRateArray[i] = (chars.Count(x => x == '0') > chars.Count(x => x == '1')) ? '1' : '0';
            }

            var gammaRate = Convert.ToInt32(new String(gammaRateArray), 2);
            var epsilonRate = Convert.ToInt32(new String(epsilonRateArray), 2);
            return (gammaRate * epsilonRate).ToString();
        }

        public override string Part2()
        {
            var lines = ReadInput("Day03");
        
            var ratingfound = false;
            var position = 0;
            var oxygenLines = lines.ToList();

            while (!ratingfound)
            {
                var chars = oxygenLines.Select(x => x.ToCharArray()[position]);

                var mostCommon = chars.Count(x => x == '0') > chars.Count(x => x == '1') ? '0' : '1';
                oxygenLines = oxygenLines.Where(x => x[position] == mostCommon).ToList();

                if(oxygenLines.Count == 1)
                {
                    ratingfound = true;
                }
                position++;
            }
            var oxygenRating = oxygenLines.First();
            position = 0;
            ratingfound = false;

            var co2Lines = lines.ToList();

            while (!ratingfound)
            {
                var chars = co2Lines.Select(x => x.ToCharArray()[position]);

                var leastCommon = chars.Count(x => x == '0') > chars.Count(x => x == '1') ? '1' : '0';
                co2Lines = co2Lines.Where(x => x[position] == leastCommon).ToList();

                if(co2Lines.Count == 1)
                {
                    ratingfound = true;
                }
                position++;
            }

            var co2Rating = co2Lines.First();

            var oxygen = Convert.ToInt32(oxygenRating, 2);
            var co2 = Convert.ToInt32(co2Rating, 2);
            return (oxygen * co2).ToString();
        }
    }
}
