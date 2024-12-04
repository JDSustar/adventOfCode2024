using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2024app
{
    internal class Day4 : IAocDay
    {
        private enum XMAS
        {
            X = 0,
            M = 1,
            A = 2,
            S = 3,
        }

        private static int FindXMASAt(List<List<Char>> ws, int row, int col)
        {
            int found = 0;

            //Search Right
            for(int coloffset = 1; coloffset <= 3; coloffset++)
            {
                if (col + coloffset >= ws[row].Count)
                {
                    break;
                }

                if (ws[row][col + coloffset].ToString() == ((XMAS)coloffset).ToString())
                {
                    if (coloffset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Left
            for (int coloffset = 1; coloffset <= 3; coloffset++)
            {
                if (col - coloffset < 0)
                {
                    break;
                }

                if (ws[row][col - coloffset].ToString() == ((XMAS)coloffset).ToString())
                {
                    if (coloffset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Up
            for (int rowoffset = 1; rowoffset <= 3; rowoffset++)
            {
                if (row - rowoffset < 0)
                {
                    break;
                }

                if (ws[row - rowoffset][col].ToString() == ((XMAS)rowoffset).ToString())
                {
                    if (rowoffset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Down
            for (int rowoffset = 1; rowoffset <= 3; rowoffset++)
            {
                if (row + rowoffset >= ws.Count)
                {
                    break;
                }

                if (ws[row + rowoffset][col].ToString() == ((XMAS)rowoffset).ToString())
                {
                    if (rowoffset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Down Right
            for (int offset = 1; offset <= 3; offset++)
            {
                if (col + offset >= ws[row].Count || row + offset >= ws.Count)
                {
                    break;
                }

                if (ws[row + offset][col + offset].ToString() == ((XMAS)offset).ToString())
                {
                    if (offset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Down Left
            for (int offset = 1; offset <= 3; offset++)
            {
                if (col - offset < 0 || row + offset >= ws.Count)
                {
                    break;
                }

                if (ws[row + offset][col - offset].ToString() == ((XMAS)offset).ToString())
                {
                    if (offset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Up Right
            for (int offset = 1; offset <= 3; offset++)
            {
                if (col + offset >= ws[row].Count || row - offset < 0)
                {
                    break;
                }

                if (ws[row - offset][col + offset].ToString() == ((XMAS)offset).ToString())
                {
                    if (offset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            //Search Up Left
            for (int offset = 1; offset <= 3; offset++)
            {
                if (col - offset < 0 || row - offset < 0)
                {
                    break;
                }

                if (ws[row - offset][col - offset].ToString() == ((XMAS)offset).ToString())
                {
                    if (offset == 3)
                    {
                        found++;
                    }
                }
                else
                {
                    break;
                }
            }

            return found;
        }

        private static int FindCrossMASAt(List<List<Char>> ws, int row, int col)
        {
            int found = 0;

            if (row == 0 || row == ws.Count - 1 || col == 0 || col == ws[row].Count - 1)
            {
                return 0;
            }

            if (ws[row - 1][col - 1] == 'M' && ws[row + 1][col + 1] == 'S')
            {
                found++;
            }

            if (ws[row + 1][col - 1] == 'M' && ws[row - 1][col + 1] == 'S')
            {
                found++;
            }

            if (ws[row - 1][col + 1] == 'M' && ws[row + 1][col - 1] == 'S')
            {
                found++;
            }

            if (ws[row + 1][col + 1] == 'M' && ws[row - 1][col - 1] == 'S')
            {
                found++;
            }

            if (found == 2)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public static string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);
            List<List<Char>> wordSearch = new List<List<Char>>();
            int found = 0;

            foreach (var line in lines)
            {
                wordSearch.Add(line.ToList());
            }

            for (int row = 0; row < wordSearch.Count; row++)
            {
                for (int col = 0; col < wordSearch[row].Count; col++)
                {
                    if (wordSearch[row][col] == 'X')
                    {
                        found += FindXMASAt(wordSearch, row, col);
                    }
                }
            }

            return found.ToString();
        }

        public static string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);
            List<List<Char>> wordSearch = new List<List<Char>>();
            int found = 0;

            foreach (var line in lines)
            {
                wordSearch.Add(line.ToList());
            }

            for (int row = 0; row < wordSearch.Count; row++)
            {
                for (int col = 0; col < wordSearch[row].Count; col++)
                {
                    if (wordSearch[row][col] == 'A')
                    {
                        found += FindCrossMASAt(wordSearch, row, col);
                    }
                }
            }

            return found.ToString();
        }
    }
}
