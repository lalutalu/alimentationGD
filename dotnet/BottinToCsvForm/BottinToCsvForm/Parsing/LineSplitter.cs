﻿using System.Text.RegularExpressions;

namespace BottinToCsvForm.Parsing
{
    public class LineSplitter
    {
        public static List<string> SplitLines(string dataString)
        {
            List<string> lines = new List<string>();
            string pattern = @"(\d{7}\s)(.*)";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    string line = match.Value;
                    lines.Add(line);
                }
            }
            return lines;
        }

        //public static List<string> SplitLinesViande(string dataString, string sectionCode)
        //{
        //    List<string> lines = new List<string>();
        //    string pattern = @"(\d{8}\s.*)";
        //    string sectionPattern = @"^\s*\d{5}\s";
        //    MatchCollection matches = Regex.Matches(dataString, pattern);
        //    MatchCollection collectionMatches = Regex.Matches(sectionCode, pattern);
        //    foreach (Match match in matches)
        //    {
        //        if (match.Success)
        //        {
        //            string line = match.Value;
        //            lines.Add(line);
        //        }
        //    }
        //    return lines;
        //}

        public static List<string> SplitLinesViande(string dataString, string sectionCode, List<string> oldCodes)
        {
            List<string> lines = new List<string>();
            string productCodePattern = @"\s\d{5}\s.*";
            string code39Digits = @"\d{8}";
            foreach (string line in dataString.Split('\n'))
            {
                if (Regex.IsMatch(line, productCodePattern) && !line.Contains(sectionCode) && !oldCodes.Any(code => line.Contains(code)))
                {
                    return lines;
                }
                if (Regex.IsMatch(line, code39Digits))
                {
                    lines.Add(line);
                }
            }
            return lines;
        }

        public static string GetProductTaxes(string line)
        {
            Match match = Regex.Match(line, @"\*{1,2}");

            if (match.Success)
            {
                string asterisks = match.Value;
                switch (asterisks)
                {
                    case "*":
                        return "FederalTaxes";
                    case "**":
                        return "BothTaxes";
                    default:
                        break;
                }
            }
            return "NoTaxes";
        }
    }
}
