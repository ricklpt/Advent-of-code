using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day10 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day10");
            var sum = 0;
            foreach (var line in lines)
            {
                var stack = new Stack<string>();
                var charArray = line.ToCharArray();


                foreach (var letter in charArray)
                {
                    if (letter == '{' || letter == '[' || letter == '<' || letter == '(')
                    {
                        stack.Push(letter.ToString());
                    }
                    else
                    {
                        if (!stack.Any())
                        {
                            Console.WriteLine($"Expected none, but found {letter} instead");
                        }
                        var toMatchChar = stack.Pop().Replace('{', '}').Replace('[', ']').Replace('<', '>').Replace('(', ')');
                        if (letter.ToString() != toMatchChar)
                        {
                            Console.WriteLine($"Expected {toMatchChar}, but found {letter} instead");
                            sum += (letter.ToString() == ")") ? 3 : letter.ToString() == "]" ? 57 : letter.ToString() == "}" ? 1197 : 25137;
                            break;
                        }
                    }
                }

            }
            return sum.ToString();
        }


        public override string Part2()
        {
            var lines = ReadInput("Day10");
            var sums = new List<long>();
            foreach (var line in lines)
            {
                var stack = new Stack<string>();
                var charArray = line.ToCharArray();
                var corruptedLine = false;

                foreach (var letter in charArray)
                {
                    if (letter == '{' || letter == '[' || letter == '<' || letter == '(')
                    {
                        stack.Push(letter.ToString());
                    }
                    else
                    {
                        if (!stack.Any())
                        {
                            Console.WriteLine($"Expected none, but found {letter} instead");
                        }
                        var toMatchChar = stack.Pop().Replace('{', '}').Replace('[', ']').Replace('<', '>').Replace('(', ')');
                        if (letter.ToString() != toMatchChar)
                        {
                            corruptedLine = true;
                            break;
                        }
                    }
                }

                if (corruptedLine)
                    continue;

                var sum = (long)0;
                while (stack.Any())
                {
                    var next = stack.Pop().Replace('{', '}').Replace('[', ']').Replace('<', '>').Replace('(', ')');
                    sum = sum * 5;
                    sum += (next.ToString() == ")") ? 1 : next.ToString() == "]" ? 2 : next.ToString() == "}" ? 3 : 4;
                }
                Console.WriteLine(sum);
                sums.Add(sum); 
            }
            var sumsArray = sums.OrderBy(x => x).ToArray();
            return sumsArray[sumsArray.Length /2].ToString();
        }
    }
}
