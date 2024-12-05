using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2024app
{
    internal class Day5 : IAocDay
    {
        private class PageOrderingRuleSet
        {
            private List<PageOrderingRule> _pageOrderingRules = new List<PageOrderingRule>();

            private class PageOrderingRule
            {
                public int PrintedFirst { get; set; }

                public int PrintedSecond { get; set; }

                public PageOrderingRule(string rule)
                {
                    var pages = rule.Split('|');
                    PrintedFirst = int.Parse(pages[0]);
                    PrintedSecond = int.Parse(pages[1]);
                }

                public bool PassesRule(List<int> pages)
                {
                    int f = pages.FindIndex(p => p == PrintedFirst);
                    int s = pages.FindIndex(p => p == PrintedSecond);

                    if (f == -1 || s == -1 || f < s)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            public PageOrderingRuleSet(List<string> lines)
            {
                foreach (string line in lines)
                {
                    _pageOrderingRules.Add(new PageOrderingRule(line));
                }
            }

            public bool UpdateIsValid(List<int> u)
            {
                foreach (var rule in _pageOrderingRules)
                {
                    if (!rule.PassesRule(u))
                    {
                        return false;
                    }
                }

                return true;
            }

            public void Fix(ref List<int> u)
            {
                foreach (var rule in _pageOrderingRules)
                {
                    if (!rule.PassesRule(u))
                    {
                        u.Remove(rule.PrintedSecond);
                        u.Insert(u.IndexOf(rule.PrintedFirst) + 1, rule.PrintedSecond);
                    }
                }
            }
        }

        private class Update
        {
            public List<int> PageUpdates;

            public Update(string update)
            {
                PageUpdates = update.Split(',').Select(u => int.Parse(u)).ToList();
            }

            public bool IsValidWithRuleSet(PageOrderingRuleSet rules)
            {
                return rules.UpdateIsValid(PageUpdates);
            }

            public void MakeValid(PageOrderingRuleSet rules)
            {
                while (!rules.UpdateIsValid(PageUpdates))
                {
                    rules.Fix(ref PageUpdates);
                }
            }

            public int GetMiddle()
            {
                return PageUpdates.ElementAt(PageUpdates.Count / 2);
            }
        }

        public static string Part1(string filename)
        {
            var lines = File.ReadAllLines(filename);

            var ruleText = lines.TakeWhile(l => l != "").ToList();

            var updateText = lines.SkipWhile(l => l != "").ToList();
            updateText.RemoveAt(0);

            var RuleSet = new PageOrderingRuleSet(ruleText);
            var Updates = new List<Update>();

            foreach (var update in updateText)
            {
                Updates.Add(new Update(update));
            }

            int middleSums = 0;

            foreach (var update in Updates)
            {
                if (update.IsValidWithRuleSet(RuleSet))
                {
                    middleSums += update.GetMiddle();
                }
            }

            return middleSums.ToString();
        }

        public static string Part2(string filename)
        {
            var lines = File.ReadAllLines(filename);

            var ruleText = lines.TakeWhile(l => l != "").ToList();

            var updateText = lines.SkipWhile(l => l != "").ToList();
            updateText.RemoveAt(0);

            var RuleSet = new PageOrderingRuleSet(ruleText);
            var Updates = new List<Update>();

            foreach (var update in updateText)
            {
                Updates.Add(new Update(update));
            }

            int middleSums = 0;

            foreach (var update in Updates)
            {
                if (!update.IsValidWithRuleSet(RuleSet))
                {
                    update.MakeValid(RuleSet);
                    middleSums += update.GetMiddle();
                }
            }

            return middleSums.ToString();
        }
    }
}
