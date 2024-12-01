using System;
using System.IO;
using System.Linq;

namespace adventofcode2024app
{
    public class Day1
    {
        static public string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);
            int sumOfCalibrationValues = 0;

            foreach (var line in lines)
            {
                var first = (line.First(c => Char.IsDigit(c)));
                var last = (line.Last(c => Char.IsDigit(c)));
                var number = first.ToString() + last.ToString();
                int calibrationValue = Convert.ToInt32(number);
                sumOfCalibrationValues += calibrationValue;
            }

            return sumOfCalibrationValues.ToString();
        }

        static public string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);
            int sumOfCalibrationValues = 0;

            foreach (var line in lines)
            {
                string newLine = line; 

                newLine = newLine.Replace("one", "one1one");
                newLine = newLine.Replace("two", "two2two");
                newLine = newLine.Replace("three", "three3three");
                newLine = newLine.Replace("four", "four4four");
                newLine = newLine.Replace("five", "five5five");
                newLine = newLine.Replace("six", "six6six");
                newLine = newLine.Replace("seven", "seven7seven");
                newLine = newLine.Replace("eight", "eight8eight");
                newLine = newLine.Replace("nine", "nine9nine");

                var first = newLine.First(c => Char.IsDigit(c));
                var last = newLine.Last(c => Char.IsDigit(c));
                var number = first.ToString() + last.ToString();
                int calibrationValue = Convert.ToInt32(number);
                sumOfCalibrationValues += calibrationValue;
            }

            return sumOfCalibrationValues.ToString();
        }
    }
}
