using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text.RegularExpressions;

namespace CIrculaireToPdf
{
    public partial class Form1 : Form
    {
        private string circulairePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void parcourir_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Fichiers PDF|*.pdf|Tous les fichiers|*.*",
                Title = "Selectionner le fichier circulaire en format PDF"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                circulairePath = dialog.FileName;
                filePath.Text = System.IO.Path.GetFileName(dialog.FileName);
            }
        }

        private void soumettre_Click(object sender, EventArgs e)
        {
            List<string> InitialProducts = ExtractTextFromPdf(circulairePath);
            MessageBox.Show($"{InitialProducts[1]}", "lol", MessageBoxButtons.OK);
        }

        public List<string> ExtractTextFromPdf(string pdfPath)
        {
            List<string> productStrings = new List<string>();
            using (PdfReader reader = new PdfReader(circulairePath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, i, strategy);
                    string[] lines = pageText.Split('\n');
                    foreach (string line in lines)
                    {
                        if (Regex.IsMatch(line, @"^IMPACT-\d+\s+"))
                        {
                            productStrings.Add(line);
                        }
                    }
                }
            }
            return productStrings;
        }

        public string ExtractOriginalPrice(string productLine)
        {
            MatchCollection matches = Regex.Matches(productLine, @"\b(\d+\.\d+)\s+");

            if (matches.Count >= 3)
            {
                if (productLine.Contains("IMPACT-4") && productLine.Contains(" VIN "))
                {
                    return matches[1].Value;
                }
                return matches[2].Value;
            }
            else
            {
                return "No Original Price";
            }
        }

        public string ExtractQuantity(string productLine)
        {
            Match match = Regex.Match(productLine, @"(\d+D?)\s+(\d+)\s+([A-Za-z]+)");
            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            else
            {
                var matches = Regex.Matches(productLine, @"\d+(?:D)?\s+([0-9.]+)(?:\s+([A-Za-z]+))?");
                for (int i = matches.Count - 1; i >= 0; i--)
                {
                    var m = matches[i];
                    if (m.Success)
                    {
                        if (m.Groups[1].Success)
                        {
                            return m.Groups[1].Value;
                        }
                        else
                        {
                            return "No Quantity - Group 1 not found";
                        }
                    }
                }
            }
            return "No Quantity";
        }

        public Tuple<string, string> ExtractWeight(string productLine)
        {
            //        weight = "No Weight"
            //match = re.search(r"\d+\s+(\d+)\s+([A-Za-z]+)", product_line)
            //if match:
            //    return match.groups(1)
            //else:
            //    weight_match = re.search(r"(\d+)\D*$", product_line)
            //    if weight_match:
            //        weight = weight_match.group(1)
            //    weight_match = re.search(r"(\d+\D+\d+)\D*$", product_line)
            //    if weight_match:
            //        weight = weight_match.group(1)
            //        numbers = weight.split()
            //        if len(numbers) >= 2:
            //            last_digits = numbers[1]
            //            weight = last_digits

            //    unit_match = re.search(r"([^\d]+)$", product_line)
            //    unit = unit_match.group(1).strip() if unit_match else "No Unit"
            //    return (weight, unit)

            string weight = "No Weight";
            string unit = "No Unit";
            var regex = new Regex(@"\d+\s+(\d+)\s+([A-Za-z]+)");
            Match match = regex.Match(productLine);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }

            return new Tuple<string, string>(weight, unit);
        }
    }
}