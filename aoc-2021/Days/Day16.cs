using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;

namespace aoc_2021
{
    public class Day16 : BaseDay
    {
        public class Packet
        {

            string _input;

            public Packet(String input)
            {
                _input = input;

                Version = Convert.ToInt32(_input.Substring(0, 3), 2);
                TypeID = Convert.ToInt32(_input.Substring(3, 3), 2);
                if (TypeID == 4)
                {
                    var cursor = 6;
                    var nextGroup = _input.Substring(cursor, 5);
                    var decimalValue = "";
                    while (nextGroup != null)
                    {
                        decimalValue += nextGroup.Substring(1);
                        cursor += 5;
                        if (nextGroup.Substring(0, 1) == "1")
                        {
                            nextGroup = _input.Substring(cursor, 5);
                        }
                        else
                        {
                            nextGroup = null;
                        }
                    }

                    Value = Convert.ToInt64(decimalValue, 2);
                    Length = cursor;
                    return;
                }
                else
                {
                    LengthType = Convert.ToInt32(_input.Substring(6, 1));

                    if (LengthType == 0)
                    {
                        var subPackagesLenght = Convert.ToInt32(_input.Substring(7, 15), 2);
                        var cursor = 22;
                        var init_index = cursor;
                        while (cursor < init_index + subPackagesLenght)
                        {
                            var packet = new Packet(_input.Substring(cursor, _input.Length - cursor));
                            cursor += packet.Length;
                            SubPackets.Add(packet);
                        }
                        Length = cursor;
                    }
                    else
                    {
                        var numberOfSubPackets = Convert.ToInt32(_input.Substring(7, 11), 2);
                        var cursor = 18;
                        for (int i = 0; i < numberOfSubPackets; i++)
                        {
                            var packet = new Packet(_input.Substring(cursor, _input.Length - cursor ));
                            cursor += packet.Length;
                            SubPackets.Add(packet);
                        }

                        Length = cursor;
                    }
                }

                if(TypeID == 0)
                {
                    Value = SubPackets.Select(x => x.Value).Sum();
                }
                else if(TypeID == 1)
                {
                    Value = SubPackets.Select(x => x.Value).Aggregate((x,y) => x * y);
                }
                else if(TypeID == 2)
                {
                    Value = SubPackets.Select(x => x.Value).Min();
                }
                else if(TypeID == 3)
                {
                    Value = SubPackets.Select(x => x.Value).Max();
                }
                else if(TypeID == 5)
                {
                    Value = (SubPackets[0].Value > SubPackets[1].Value) ? 1 : 0;
                }
                else if(TypeID == 6)
                {
                    Value = (SubPackets[0].Value < SubPackets[1].Value) ? 1 : 0;
                }
                else if(TypeID == 7)
                {
                    Value = (SubPackets[0].Value == SubPackets[1].Value) ? 1 : 0;
                }

            }

            public int Version { get; }
            public int TypeID { get; }
            public long Value { get; }
            public int Length { get; }
            public int LengthType { get; }
            public List<Packet> SubPackets { get; } = new List<Packet>();

            public int VersionSum{
                get{
                    return Version + SubPackets.Select(x => x.VersionSum).Sum();
                }
            }
        }

        Packet p = null;
        public override string Part1()
        {
            var lines = ReadInput("Day16");
            var intt = int.Parse("F", System.Globalization.NumberStyles.HexNumber);
            string binarystring = String.Join(String.Empty, lines[0].Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
            p = new Packet(binarystring);

            return p.VersionSum.ToString();
        }

        public override string Part2()
        {
            return p.Value.ToString();
        }
    }
}
