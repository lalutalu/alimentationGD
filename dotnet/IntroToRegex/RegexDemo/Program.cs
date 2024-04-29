/*
 * ^ - Starts with
 * $ - Ends with
 * [] - Range
 * () - Group
 * . - Single character once
 * + - one or more characters in a row
 * ? - optional preceding character match
 * \ - escape character
 * \n - New line
 * \d - Digit
 * \D - Non-digit
 * \s - White space
 * \S - non-white space
 * \w - alphanumeric/underscore character (word chars)
 * \W - non-word characters
 * {x,y} - Repeat low (x) to high (y) (no "y" means at least x, no ",y" means that many)
 * (x|y) - Alternative - x or y
 * 
 * [^x] - Anything but x (where x is whatever character you want)
 */

using System.Diagnostics;
using System.Text.RegularExpressions;

// (440) 555-1212
// 12-34567890
// 123 87876

string toSearch = File.ReadAllText("test.txt");

// This pattern matches phone numbers in the following patterns
string pattern = @"\(?\d{3}\)?(-|.|\s)?\d{3}(-|.)?\d{4}";

MatchCollection matches = Regex.Matches(toSearch, pattern);

foreach (Match match in matches)
{
    Console.WriteLine(match.Value);
}

//string pattern = @"(\s|^)Tim(\s|$)";
//string toSearch = "Tim Corey";

//Console.WriteLine("Tim Corey: " + Regex.IsMatch("Tim Corey", pattern));
//Console.WriteLine("Timothy Corey: " + Regex.IsMatch("Timothy Corey", pattern));
//Console.WriteLine("Always Tim: " + Regex.IsMatch("Always Tim", pattern));
//Console.WriteLine("I Am Tim Corey: " + Regex.IsMatch("I Am Tim Corey", pattern));

//Stopwatch stopwatch = new();

//stopwatch.Start();
//Regex test = new Regex(pattern);

//for (int i = 0; i < 100000; i++)
//{
//    //Regex.IsMatch("I Am Tim Corey", pattern);
//    test.IsMatch("I Am Tim Corey");
//}

//stopwatch.Stop();

//Console.WriteLine($"Time Elapsed in ms: {stopwatch.ElapsedMilliseconds}");