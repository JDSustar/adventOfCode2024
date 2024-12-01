using System;

namespace adventofcode2024app
{
    class Program
    {
        static void Main(string[] args)
        {
            string output;

            Day1.Part1("day1/day1.txt");
            output = Day1.Part2("day1/day1.txt");


            Console.WriteLine(output);
        }
    }
}
