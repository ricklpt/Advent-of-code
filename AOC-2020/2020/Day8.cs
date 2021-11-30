using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Advent_of_code._2020
{
	public class Day8
	{
		Instruction[] instructions = null;
		int accumulator = 0;
		int cursor = 0;

		public Day8()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_08.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;
			}

			var lines = File.ReadAllLines(path).Select(x => {
				var splitted = x.Split(' ');

				return new Instruction {
					Operation = splitted[0],
					Attribute = Convert.ToInt32(splitted[1])
				};
			}).ToArray();

			//Part1
			Execute(lines, 0, 0);

			for (int i = 0; i < lines.Length; i++)
			{
				if (lines[i].Operation == "acc") continue; 
				var lines2 = File.ReadAllLines(path).Select(x => {
					var splitted = x.Split(' ');

					return new Instruction
					{
						Operation = splitted[0],
						Attribute = Convert.ToInt32(splitted[1])
					};
				}).ToArray();

				var lineToMofify = lines2[i];			

				var prevOpreration = lineToMofify.Operation;

				lineToMofify.Operation = prevOpreration == "jmp" ? "nop" : "jmp";

				Execute(lines2, 0, 0);
			}
		}

		private int Execute(Instruction[] instructions, int currentCursor, int accumulator)
		{
			var instruction = instructions[currentCursor];
			Console.WriteLine($"{instruction.Operation}\t{instruction.Attribute}\t{currentCursor}\t{accumulator}");
			if (instruction.HasRun)
				return accumulator;

			instruction.HasRun = true;

			switch(instruction.Operation)
			{
				case "nop":
					return Execute(instructions, currentCursor + 1, accumulator);
				case "acc":
					return Execute(instructions, currentCursor + 1, accumulator += instruction.Attribute);
				case "jmp":
					return Execute(instructions, currentCursor + instruction.Attribute, accumulator);					
			}

			return accumulator;
		}
	}

	public class Instruction
	{
		public string Operation { get; set; } = string.Empty;

		public int Attribute { get; set; } = 0;

		public bool HasRun { get; set; } = false;
	}
}
