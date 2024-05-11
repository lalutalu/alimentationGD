using System.Text.RegularExpressions;

namespace BottinToCSV
{
    public class LineSplitter
    {
        public static List<string> SplitLines(string dataString)
        {
            List<string> lines = new List<string>();

            // Modified regex pattern with assertions (same as before)
            //string pattern = @"(?<!\*)(\d{7}\s)(?=\S)(.*?)(?=\*\s*|$|\/\½$)";
            //string pattern = @"(?<!\*)(\d{7}\s)(?=\S)(.*?)(?=\*\s*|$|\/$)";
            //string pattern = @"(\d{7}\s)(.*?)(?=\*\s*|$)";
            string pattern = @"(\d{7}\s)(.*)";
            MatchCollection matches = Regex.Matches(dataString, pattern);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    // Get captured string (code and line content)
                    string line = match.Value;
                    //Console.WriteLine(line);
                    //// Find the index of the last asterisk
                    //int asteriskIndex = line.LastIndexOf('*');

                    //// If asterisk found, remove everything after it
                    //if (asteriskIndex > -1)
                    //{
                    //    line = line.Substring(0, asteriskIndex);
                    //}

                    lines.Add(line);
                }
            }
            return lines;
        }
    }
}
