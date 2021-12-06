using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day04 : BaseDay
    {
        private List<Tuple<int, bool>[,]> parseBoards(string[] input)
        {
            List<Tuple<int, bool>[,]> boards = new List<Tuple<int, bool>[,]>();
            var currentBoard = new Tuple<int, bool>[5, 5];
            var x = 0;

            for (int i = 2; i < input.Length; i++)
            {
                if (((i - 1) % 6 == 0) && i != 1)
                {
                    boards.Add(currentBoard);
                    currentBoard = new Tuple<int, bool>[5, 5];
                    x = 0;
                    continue;
                }

                var splitted = Regex.Matches(input[i], @"\d+");

                for (int y = 0; y < splitted.Count; y++)
                {
                    currentBoard[x, y] = new Tuple<int, bool>(Convert.ToInt32(splitted[y].Value), false);
                }
                x++;
            }

            boards.Add(currentBoard);

            return boards;
        }

        private void markNumberOnBoard(Tuple<int, bool>[,] board, int number)
        {
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    var item = board[x, y];
                    if (item.Item1 == number)
                    {
                        board[x, y] = new Tuple<int, bool>(item.Item1, true);
                    }
                }
            }
        }
        private bool checkBoardCompleted(Tuple<int, bool>[,] board)
        {
            for (int x = 0; x < 5; x++)
            {
                var countSuccesful = 0;
                for (int y = 0; y < 5; y++)
                {
                    if(board[x,y].Item2)
                    {
                        countSuccesful++;
                    }
                }
                if(countSuccesful == 5)
                {
                    return true;
                }
            }

            for (int y = 0; y < 5; y++)
            {
                var countSuccesful = 0;
                for (int x = 0; x < 5; x++)
                {
                    if(board[x,y].Item2)
                    {
                        countSuccesful++;
                    }
                }
                if(countSuccesful == 5)
                {
                    return true;
                }
            }

            return false;
        }

        private int countUnmarked(Tuple<int, bool>[,] board)
        {
            var count = 0;

            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    if(!board[x,y].Item2)
                    {
                        count += board[x,y].Item1;
                    }
                }

            } 

            return count;
        }

        public override string Part1()
        {
            var lines = ReadInput("Day04");
            var boards = parseBoards(lines);
            var numbers = lines[0].Split(',').Select(x => Convert.ToInt32(x));

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    markNumberOnBoard(board, number);
                    var completed = checkBoardCompleted(board);
                    if(completed)
                    {
                        return (countUnmarked(board) * number).ToString();
                    }
                }
            }

            return "";
        }

        public override string Part2()
        {
            var lines = ReadInput("Day04");
            var boards = parseBoards(lines);
            var numbers = lines[0].Split(',').Select(x => Convert.ToInt32(x));
            var amountOfBoards = boards.Count;
            var completedBoards = 0;

            foreach (var number in numbers)
            {
                foreach (var board in boards)
                {
                    var isAlreadyCompleted = checkBoardCompleted(board);
                    if(isAlreadyCompleted)
                    {
                        continue;
                        
                    }
                    markNumberOnBoard(board, number);
                    var completed = checkBoardCompleted(board);
                    if(completed)
                    {
                        if(completedBoards == (amountOfBoards -1))
                        {
                            return (countUnmarked(board) * number).ToString();
                        }

                        completedBoards++;
                    }
                }
            }

            return "".ToString();
        }
    }
}
