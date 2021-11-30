using System;

namespace aoc_2021
{
    public class Day01 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day01");

            foreach (var item1 in lines)
            {
                var int1 = Convert.ToInt32(item1);
                foreach (var item2 in lines)
                {
                    var int2 = Convert.ToInt32(item2);
                    if (int1 + int2 == 2020)
                    {
                        return (int1 * int2).ToString();
                    }
                }
            }

            return "0";
        }

        public override string Part2()
        {
            var lines = ReadInput("Day01");

            foreach (var item1 in lines)
            {
                var int1 = Convert.ToInt32(item1);
                foreach (var item2 in lines)
                {
                    var int2 = Convert.ToInt32(item2);
                    foreach (var item3 in lines)
                    {
                        var int3 = Convert.ToInt32(item3);
                        if (int1 + int2 + int3 == 2020)
                        {
                            return (int1 * int2 * int3).ToString();
                        }
                    }
                }
            }

            return "0";
        }
    }
}
