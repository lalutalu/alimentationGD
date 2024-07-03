using BottinToCsvForm.Parsing;
using System.Diagnostics;
namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        private ViandeParsing viandeParsing;
        private ToolTip toolTip;
        private List<string> circualairePaths = new List<string>();
        private string viandePath;
        private List<string> viandeToKeepOriginal = new List<string>();
        private List<string> viandeToKeepSurgele = new List<string>();
        private List<string> selectedFiles = new List<string>();
        private List<string> extraFiles = new List<string>();
        private List<Product> currentProducts = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }
        private string FormatTooltipText(string text)
        {
            return string.Join(Environment.NewLine, text.Split(new[] { ';' }, StringSplitOptions.None));
        }

        private void effacer_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            selectedFiles.Clear();
        }

        private void effacerCSV_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            extraFiles.Clear();
        }

        private void effacerCirculaire_Click(object sender, EventArgs e)
        {
            circulairePath.Text = "";
            circualairePaths.Clear();
        }

        private void parcourir_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show("Vous avez déjà choisi un bottin...\nVeuillez appuyez sur Effacer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Fichiers PDF|*.pdf|Tous les fichiers|*.*",
                Title = "Selectionner un Bottin format PDF"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedFiles.Add(dialog.FileName);
                textBox1.Text = Path.GetFileName(dialog.FileName);
            }
        }
        private void parcourirCirculaire_Click(object sender, EventArgs e)
        {
            if (circulairePath.Text != "")
            {
                MessageBox.Show("Vous avez déjà choisi des fichiers...\nVeuillez appuyez sur Effacer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Fichiers PDF|*.pdf|Tous les fichiers|*.*",
                Title = "Selectionner un Circulaire format PDF"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                circualairePaths.AddRange(dialog.FileNames);
                circulairePath.Text = string.Join(";", dialog.FileNames.Select(Path.GetFileName));
            }
        }
        private void parcourirCSV_Click(object sender, EventArgs e)
        {
            if (extraFiles.Count > 0)
            {
                MessageBox.Show("Vous avez déjà choisi des fichiers...\nVeuillez appuyez sur Effacer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Fichiers XLSX|*.xlsx|Fichiers CSV|*.csv|Tous les fichiers|*.*",
                Title = "Sélectionner des fichiers CSV ou XLSX"
            };

            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                extraFiles.AddRange(dialog.FileNames);
                textBox2.Text = string.Join(";", dialog.FileNames.Select(Path.GetFileName));
                textBox2.Multiline = true;
            }
        }

        private void soumettre_Click(object sender, EventArgs e)
        {
            if (selectedFiles.Count == 0 && textBox1.Text == "")
            {
                MessageBox.Show("Veuillez choisir un bottin...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PdfDataParsing pdfDataParsing = new PdfDataParsing();
            ViandeParsing viandeParsing = new ViandeParsing();
            FileCreation fileCreation = new FileCreation(selectedFiles[0]);
            CSVDataParsing csvDataParsing = new CSVDataParsing();
            CirculaireParsing circulaireToCSV = new CirculaireParsing();
            int counter = 1;
            try
            {
                currentProducts.Clear();
                foreach (var file in selectedFiles)
                {
                    List<string> parsedData = new List<string>(PdfDataParsing.ParsePdf(file));
                    foreach (var datastring in parsedData)
                    {
                        Product product = pdfDataParsing.ParseProducts(datastring);
                        product.HandleID = $"Produit_{counter}";
                        currentProducts.Add(product);
                        counter++;
                    }
                }
                if (circualairePaths.Count > 0)
                {
                    foreach (var file in circualairePaths)
                    {
                        List<Product> circulaireProducts = circulaireToCSV.ExtractCirculaireProducts(file);
                        counter = CSVDataParsing.UpdatePrices(currentProducts, circulaireProducts, counter);
                    }
                }

                if (extraFiles.Count > 0)
                {
                    foreach (var file in extraFiles)
                    {
                        List<Product> fileProducts = csvDataParsing.ReadFile(file);
                        counter = CSVDataParsing.UpdatePrices(currentProducts, fileProducts, counter);
                    }
                }

                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    if (string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Veuillez choisir une plage de numéros pour la viande souhaitée", "Pas de plage!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    if (string.IsNullOrEmpty(sugelerText.Text))
                    {
                        MessageBox.Show("Veuillez choisir une plage de numéros pour la viande surgelé", "Pas de plage!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var sections = textBox4.Text.Split(";");
                    foreach (var section in sections)
                    {
                        viandeToKeepOriginal.Add(section);
                    }
                    var sectionsSurgeler = sugelerText.Text.Split(";");
                    foreach (var section in sectionsSurgeler)
                    {
                        viandeToKeepSurgele.Add(section);
                    }

                    List<string> viandeStrings = ViandeParsing.ParsePdf(viandePath, viandeToKeepOriginal);
                    List<Product> viandeProductsOriginal = new List<Product>();
                    foreach (var viandeString in viandeStrings)
                    {
                        viandeProductsOriginal.Add(viandeParsing.ParseViande(viandeString.Trim(), "Viande et produit frais"));
                    }

                    List<string> viandeStringSurgeler = ViandeParsing.ParsePdf(viandePath, viandeToKeepSurgele);
                    List<Product> viandeProductsSurgeler = new List<Product>();
                    foreach (var codeSurgeler in viandeStringSurgeler)
                    {
                        viandeProductsSurgeler.Add(viandeParsing.ParseViande(codeSurgeler, "Viande Surgele"));
                    }
                    counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsOriginal, counter);
                    counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsSurgeler, counter);
                }
                string filepath = fileCreation.CreateFile(currentProducts);
                MessageBox.Show($"{filepath} créé sur le bureau!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(textBox2, FormatTooltipText(textBox2.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Fichiers PDF|*.pdf|Tous les fichiers|*.*",
                Title = "Selectionner le fichier de viande format PDF"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                viandePath = dialog.FileName;
                textBox3.Text = Path.GetFileName(dialog.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            viandePath = "";
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            viandeToKeepOriginal.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sugelerText.Text = "";
            viandeToKeepSurgele.Clear();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = Path.Combine(baseDir, "Format des fichiers supplémentaires.pdf");

            try
            {
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while trying to open the PDF file: " + ex.Message);
            }
        }
    }
}
