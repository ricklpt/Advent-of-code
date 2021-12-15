using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace aoc_2021
{
    public abstract class BaseDay
    {
        protected string[] ReadInput(string fileName)
        {
            var path = $"{Environment.CurrentDirectory}/Input/{fileName}.txt";
            //var filePath = DirectoryPath + "\\" + FileName;
            if (!File.Exists(path))
            {
                throw new Exception("File not found");
            }

            return File.ReadAllLines(path);
        }

        public void Run()
        {
             Console.WriteLine($"Solution Part 1 = {Part1()}");
            Console.WriteLine($"Solution Part 2 = {Part2()}");
        }

        public abstract string Part1();
        public abstract string Part2();
    }


}
