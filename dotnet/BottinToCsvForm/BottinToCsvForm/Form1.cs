using BottinToCsvForm.Parsing;
namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        private ViandeParsing viandeParsing;
        private List<string> extraFiles = new List<string>();
        private string circulaireFilePath;
        private string viandePath;
        private List<string> viandeToKeep = new List<string>();
        private List<string> selectedFiles = new List<string>();
        private List<Product> currentProducts = new List<Product>();
        private ToolTip toolTip;

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
            circulaireFilePath = "";
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


        private void parcourirCirculaire_Click(object sender, EventArgs e)
        {
            if (circulairePath.Text != "")
            {
                MessageBox.Show("Vous avez déjà choisi des fichiers...\nVeuillez appuyez sur Effacer", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            OpenFileDialog dialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Fichiers PDF|*.pdf|Tous les fichiers|*.*",
                Title = "Selectionner un Circulaire format PDF"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                circulaireFilePath = dialog.FileName;
                circulairePath.Text = Path.GetFileName(dialog.FileName);
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
                if (extraFiles.Count > 0)
                {
                    foreach (var file in extraFiles)
                    {
                        List<Product> fileProducts = csvDataParsing.ReadFile(file);
                        counter = CSVDataParsing.UpdatePrices(currentProducts, fileProducts, counter);
                    }
                }

                if (!string.IsNullOrEmpty(circulaireFilePath))
                {
                    List<Product> circulaireProducts = circulaireToCSV.ExtractCirculaireProducts(circulaireFilePath);
                    counter = CSVDataParsing.UpdatePrices(currentProducts, circulaireProducts, counter);
                }

                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    if (string.IsNullOrEmpty(textBox4.Text))
                    {
                        MessageBox.Show("Veuillez choisir une plage de numéros pour la viande souhaitée", "Pas de plage!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    var sections = textBox4.Text.Split(";");
                    foreach (var section in sections)
                    {
                        viandeToKeep.Add(section);
                    }
                    //MessageBox.Show(sections[0].ToString(), "lol", MessageBoxButtons.OK);

                    List<string> viandeStrings = ViandeParsing.ParsePdf(viandePath, viandeToKeep);
                    List<Product> viandeProducts = new List<Product>();
                    foreach (var viandeString in viandeStrings)
                    {
                        viandeProducts.Add(viandeParsing.ParseViande(viandeString));
                    }
                    counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProducts, counter);
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
            viandeToKeep.Clear();
        }
    }
}
