using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2024app
{
    public class Day7 : IAocDay
    {
        public class Equation
        {
            public long TotalValue { get; private set; }
            private List<long> _operands;
            Func<long, long, long> add = (x, y) => x + y;
            Func<long, long, long> mult = (x, y) => x * y;
            List<List<Func<long, long, long>>> _operators;
            List<long> _possibleValues = new List<long>();


            public Equation(string line)
            {
                TotalValue = long.Parse(line.Split(':')[0]);
                _operands = line.Split(':')[1].Split(' ').Skip(1).Select(i => long.Parse(i)).ToList();
            }

            private void NextLevel(List<long> remainingOperands, long valueSoFar, bool useConcatOperator)
            {
                if (remainingOperands.Count == 0)
                {
                    _possibleValues.Add(valueSoFar);
                }
                else
                {
                    long operand = remainingOperands[0];
                    
                    NextLevel(remainingOperands.Skip(1).ToList(), operand + valueSoFar, useConcatOperator);
                    NextLevel(remainingOperands.Skip(1).ToList(), operand * valueSoFar, useConcatOperator);
                    if (useConcatOperator)
                        NextLevel(remainingOperands.Skip(1).ToList(), long.Parse(valueSoFar.ToString() + operand.ToString()), useConcatOperator);
                }
            }

            public bool EquationExists(bool useConcatOperator = false)
            {
                long operand = _operands[0];
                NextLevel(_operands.Skip(1).ToList(), operand, useConcatOperator);

                return _possibleValues.Contains(TotalValue);
            }
        }
        public static string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var equations = new List<Equation>();

            foreach (var line in lines)
            {
                equations.Add(new Equation(line));
            }

            return equations.Where(e => e.EquationExists()).Sum(e => e.TotalValue).ToString();
        }

        public static string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);
            var equations = new List<Equation>();

            foreach (var line in lines)
            {
                equations.Add(new Equation(line));
            }

            return equations.Where(e => e.EquationExists(true)).Sum(e => e.TotalValue).ToString();
        }
    }
}
