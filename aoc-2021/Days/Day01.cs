using System;

namespace aoc_2021
{
    public class Day01 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day01");
            var previousLine = 999999;
            var count = 0;

            foreach (var item1 in lines)
            {
                var int1 = Convert.ToInt32(item1);

                if(previousLine < int1)
                {
                    count++;
                }

                previousLine = int1;
            }

            return count.ToString();
        }

        public override string Part2()
        {
            var lines = ReadInput("Day01");            
            var count = 0;

            var intA = 0;
            var intB = 0;
            var intC = 0;

            var previousSum = 0;

            foreach (var item1 in lines)
            {
                var int1 = Convert.ToInt32(item1);

                intA = intB;
                intB = intC;
                intC = int1;

                var sum = intA + intB + intC;

                if(previousSum < sum && intA != 0 && intB != 0 && intC != 0)
                {
                    count++;
                }

                previousSum = sum;

            }
            count--; //account for the first anwswer that is nog right.
            
            return count.ToString();
        }
    }
}
