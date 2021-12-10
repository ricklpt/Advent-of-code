using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day09 : BaseDay
    {
        public override string Part1()
        {
            var lines = ReadInput("Day09");
            var maxX = lines.Count();
            var maxY = lines[0].Count();

            var array = new int[maxX,maxY];

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    //Console.Write(lines[x].ToCharArray()[y]);
                    array[x,y] = Convert.ToInt32(lines[x].ToCharArray()[y].ToString());
                }
            }

            var sum = 0;
            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    var lowspot = true;
                    var spot = array[x,y];

                    if(lowspot && x > 0)
                        lowspot =  spot < array[x-1,y];

                    if(lowspot && y > 0)
                        lowspot =  spot < array[x,y-1];

                    if(lowspot && x < maxX -1)
                        lowspot =  spot < array[x+1,y];

                    if(lowspot && y < maxY -1 )
                        lowspot =  spot < array[x,y+1];

                    if(lowspot)
                        sum += spot + 1;
                 
                }
            }




            return sum.ToString();
        }


        public override string Part2()
        {
            var lines = ReadInput("Day09");
            var maxX = lines.Count();
            var maxY = lines[0].Count();

            var array = new int[maxX,maxY];

            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    //Console.Write(lines[x].ToCharArray()[y]);
                    array[x,y] = Convert.ToInt32(lines[x].ToCharArray()[y].ToString());
                }
            }

            var basisSizes = new List<int>();
            for (int x = 0; x < maxX; x++)
            {
                //Console.WriteLine(lines[x]);
                for (int y = 0; y < maxY; y++)
                {
                    var lowspot = true;
                    var spot = array[x,y];

                    if(lowspot && x > 0)
                        lowspot =  spot < array[x-1,y];

                    if(lowspot && y > 0)
                        lowspot =  spot < array[x,y-1];

                    if(lowspot && x < maxX -1)
                        lowspot =  spot < array[x+1,y];

                    if(lowspot && y < maxY -1 )
                        lowspot =  spot < array[x,y+1];

                    if(lowspot)
                    {
                        var basinSpots = new List<KeyValuePair<int,int>>();

                        basinSpots.Add(new KeyValuePair<int, int>(x,y));
                        var newSpotsCount = 9999999;
                        var basinSpotsCount = 1;
                        do
                        {
                            var newSpots = new List<KeyValuePair<int,int>>();
                            foreach (var basinSpot in basinSpots)
                            {
                                var distance = 1;
                                while(basinSpot.Key - distance >= 0 )
                                {
                                    if(array[basinSpot.Key - distance, basinSpot.Value] < 9)
                                    {
                                        newSpots.Add(new KeyValuePair<int, int>(basinSpot.Key - distance, basinSpot.Value));
                                        distance++;
                                    }
                                    else
                                    {
                                        distance = 9999;
                                    }
                                }
                                distance = 1;
                                while(basinSpot.Value - distance >= 0 )
                                {
                                    if(array[basinSpot.Key, basinSpot.Value - distance] < 9)
                                    {
                                        newSpots.Add(new KeyValuePair<int, int>(basinSpot.Key, basinSpot.Value - distance));
                                        distance++;
                                    }
                                    else
                                    {
                                        distance = 9999;
                                    }
                                }
                                distance = 1;
                                while(basinSpot.Key + distance < maxX )
                                {
                                    if(array[basinSpot.Key + distance, basinSpot.Value] < 9)
                                    {
                                        newSpots.Add(new KeyValuePair<int, int>(basinSpot.Key + distance, basinSpot.Value));
                                        distance++;
                                    }
                                    else
                                    {
                                        distance = 9999;
                                    }
                                }
                                distance = 1;
                                while(basinSpot.Value + distance < maxY)
                                {
                                    if(array[basinSpot.Key, basinSpot.Value + distance] < 9)
                                    {
                                        newSpots.Add(new KeyValuePair<int, int>(basinSpot.Key, basinSpot.Value + distance));
                                        distance++;
                                    }
                                    else
                                    {
                                        distance = 9999;
                                    }
                                }

                            }
                            newSpots = newSpots.Except(basinSpots).ToList();
                            
                            basinSpotsCount = basinSpots.Count;
                            basinSpots = basinSpots.Union(newSpots).ToList();
                            newSpotsCount = basinSpots.Count;

                        } while (basinSpotsCount < newSpotsCount);
                    
                    basisSizes.Add(basinSpotsCount);
                    }
                        
                 
                }
            }




            return basisSizes.OrderByDescending(x=>x).Take(3).Aggregate((a,b) => a * b).ToString();
        }
    }
}
