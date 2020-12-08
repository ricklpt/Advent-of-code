using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;

namespace Advent_of_code._2020
{
	public class Bag { 
		public string Colour { get; set; }

		public int Quantifier { get; set; }

		public List<Bag> Bags { get; set; } = new List<Bag>();
				
		public Bag() { }
		public Bag(string input)
		{
			input = input.Replace(".", "");
			var regex = new Regex(@"((?<quanntifier>\d+) )?(?<colour>\w+ \w+) (bag)(s?)");

			if(input.Contains("contain"))
			{
				var splitted = input.Split("contain");
				var result = regex.Match(splitted[0]);
				Colour = result.Groups["colour"].Value;
				Quantifier = 1;
				input = splitted[1];
			}

			if (input.Contains("no other bags"))
				return;

			Bags = input.Split(',').Select(x => {
				var result = regex.Match(x);
				return new Bag
				{ 
					Colour = result.Groups["colour"].Value,
					Quantifier = string.IsNullOrEmpty(result.Groups["quanntifier"].Value) ? 1 : Convert.ToInt32(result.Groups["quanntifier"].Value)
					
				};
			}).ToList();
		}
	}
	public class Day7
	{
		public Day7()
		{
			var path = $"{Environment.CurrentDirectory}\\Input\\2020_07.txt";
			//var filePath = DirectoryPath + "\\" + FileName;
			if (!File.Exists(path))
			{
				return;
			}

			var lines = File.ReadAllText(path).Split(Environment.NewLine);
			var Bags = lines.Select(x => new Bag(x)).ToList();
			var result = inspectBag(Bags, "shiny gold").GroupBy(x => x.Colour).Select(x => x.First()).ToList();
			var resultCOunt = result.Count();


			var shinyGoldBag = Bags.First(x => x.Colour == "shiny gold");
			var result2 = countBags(Bags, shinyGoldBag);
		}

		public int countBags(List<Bag> allbags, Bag bagtoLookIn)
		{
			var result = bagtoLookIn.Quantifier;
			
			foreach(Bag bag in bagtoLookIn.Bags)
			{
				result += bag.Quantifier * countBags(allbags, allbags.First(x => x.Colour == bag.Colour));
			}

			return result;

		}

		public List<Bag> inspectBag(List<Bag> allBags, string colourToLook)
		{
			var directHits = allBags.Where(x => x.Bags.Any(y => y.Colour == colourToLook)).ToList();
			var result = new List<Bag>(directHits);

			foreach(var hit in directHits)
			{
				result = result.Concat(inspectBag(allBags, hit.Colour)).ToList();
			}

			return result;
		}

		
	}
}
