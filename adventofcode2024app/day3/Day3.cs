using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2024app
{
    internal class Day3 : IAocDay
    {
        public static string Part1(string filename)
        {
            var text = File.ReadAllText(filename);

            string mulPattern = @"mul\((\d+)\,(\d+)\)";
            Regex rg = new Regex(mulPattern);

            var muls = rg.Matches(text);
            int sum = 0;

            foreach (Match match in muls)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }

            return sum.ToString();
        }

        public static string Part2(string filename)
        {
            var text = File.ReadAllText(filename);

            string mulPattern = @"mul\((\d+)\,(\d+)\)|don\'t\(\)|do\(\)";
            Regex rg = new Regex(mulPattern);

            var muls = rg.Matches(text);
            int sum = 0;
            bool enabled = true;

            foreach (Match match in muls)
            {
                if (match.Groups[0].Value == @"don't()")
                {
                    enabled = false;
                }
                else if (match.Groups[0].Value == @"do()")
                {
                    enabled = true;
                }
                else if (enabled)
                {
                    sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }
            }

            return sum.ToString();
        }
    }
}
