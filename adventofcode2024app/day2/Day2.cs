using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2024app
{
    public class Day2 : IAocDay
    {
        private class Report
        {
            public List<int> reportLevels = new List<int>();

            public Report(string line)
            {
                reportLevels.AddRange(line.Split(' ').ToList().FindAll(l => int.TryParse(l, out _)).Select(l => Convert.ToInt32(l)));
            }

            public bool IsSafe(List<int> levels = null)
            {
                if (levels == null)
                {
                    levels = reportLevels;
                }

                return IsSafeAscending(levels) || IsSafeDescending(levels);
            }

            private static bool IsSafeAscending(List<int> levels)
            {
                bool valid = true;

                for (int i = 0; i < levels.Count - 1; i++)
                {
                    if (!(levels[i] < levels[i + 1] && levels[i] + 3 >= levels[i + 1]))
                    {
                        valid = false;
                        break;
                    }
                }

                return valid;
            }

            private static bool IsSafeDescending(List<int> levels)
            {
                bool valid = true;

                for (int i = 0; i < levels.Count - 1; i++)
                {
                    if (!(levels[i] > levels[i + 1] && levels[i] <= levels[i + 1] + 3))
                    {
                        valid = false;
                        break;
                    }
                }

                return valid;
            }

            public bool IsSafeDampened(List<int> levels = null)
            {
                if (levels == null)
                {
                    levels = reportLevels;
                }

                if (IsSafe(levels))
                {
                    return true;
                }

                for (int i = 0; i < reportLevels.Count; i++)
                {
                    List<int> removedOneLevels = levels.ToList();
                    removedOneLevels.RemoveAt(i);

                    if (IsSafe(removedOneLevels))
                    {
                        return true;
                    }
                }

                return false;
            }

        }
        public static string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var reports = new List<Report>();

            foreach (var line in lines)
            {
                reports.Add(new Report(line));
                //Console.WriteLine("Line: " + line + " - " + reports.Last().IsSafe().ToString());
            }

            return reports.Count(r => r.IsSafe()).ToString();
        }

        public static string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var reports = new List<Report>();

            foreach (var line in lines)
            {
                reports.Add(new Report(line));
                //Console.WriteLine("Line: " + line + " - " + reports.Last().IsSafeDampened().ToString());
            }

            return reports.Count(r => r.IsSafeDampened()).ToString();
        }
    }
}
