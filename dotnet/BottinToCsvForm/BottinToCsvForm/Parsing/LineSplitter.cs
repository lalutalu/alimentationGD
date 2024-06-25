using System.Text.RegularExpressions;

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

        public static List<string> SplitLinesViande(string dataString)
        {
            List<string> lines = new List<string>();
            string pattern = @"(\d{8}\s)(.*)";
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
