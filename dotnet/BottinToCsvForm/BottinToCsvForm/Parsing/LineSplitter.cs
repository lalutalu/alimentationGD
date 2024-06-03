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
    }
}
