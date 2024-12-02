using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace adventofcode2024app
{
    public class Day1 : IAocDay
    {
        static public string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            foreach (var line in lines)
            {
                var parts = line.Split("   ");
                left.Add(Convert.ToInt32(parts[0]));
                right.Add(Convert.ToInt32(parts[1]));
            }

            left.Sort();
            right.Sort();

            int totalDistance = 0;

            for (int i = 0; i < left.Count; i++)
            {
                totalDistance += Math.Abs(left[i] - right[i]);
            }

            return totalDistance.ToString();
        }

        static public string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);
            List<int> left = new List<int>();
            List<int> right = new List<int>();

            foreach (var line in lines)
            {
                var parts = line.Split("   ");
                left.Add(Convert.ToInt32(parts[0]));
                right.Add(Convert.ToInt32(parts[1]));
            }

            int similarityScore = 0;

            foreach (var n in left)
            {
                int same = right.Count(i => i == n);
                similarityScore += same * n;
            }

            return similarityScore.ToString();
        }
    }
}
