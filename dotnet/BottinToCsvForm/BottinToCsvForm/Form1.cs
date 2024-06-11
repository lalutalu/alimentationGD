using BottinToCsvForm.Parsing;
namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        private string selectedFolder;
        private string circulaireFilePath;
        private List<string> selectedFiles = new List<string>();
        private List<Product> currentProducts = new List<Product>();

        public Form1()
        {
            InitializeComponent();
        }

        private void effacer_click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            selectedFiles.Clear();
        }

        private void effacer_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            selectedFiles.Clear();
        }

        private void effacerCSV_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            selectedFolder = "";
        }

        private void effacerCirculaire_Click(object sender, EventArgs e)
        {
            circulairePath.Text = "";
            circulaireFilePath = "";
        }

        private void parcourir_Click(object sender, EventArgs e)
        {
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
                textBox1.Text = dialog.FileName;
            }
        }

        private void parcourirCSV_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                MessageBox.Show("Vous avez deja choisi un dossier...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "S�lectionner un dossier contenant des fichiers CSV ou XLSX"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedFolder = dialog.SelectedPath;
                textBox2.Text = selectedFolder;
            }
        }

        private void parcourirCirculaire_Click(object sender, EventArgs e)
        {
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
                circulairePath.Text = dialog.FileName;
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
            FileCreation fileCreation = new FileCreation();
            CSVDataParsing csvDataParsing = new CSVDataParsing();
            CirculaireParsing circulaireToCSV = new CirculaireParsing();
            int counter = 1;

            try
            {
                currentProducts.Clear();

                // Process all selected files
                foreach (var file in selectedFiles)
                {
                    List<string> parsedData = new List<string>(PdfDataParsing.ParsePdf(file));
                    foreach (var datastring in parsedData)
                    {
                        Product product = pdfDataParsing.ParseProducts(datastring);
                        product.HandleID = $"Produit_{counter}";
                        product.Category = "Produits Secs";
                        currentProducts.Add(product);
                        counter++;
                    }
                }

                // Process CSV/XLSX files from the selected folder
                if (!string.IsNullOrEmpty(selectedFolder))
                {
                    var files = Directory.GetFiles(selectedFolder, "*.*").Where(s => s.EndsWith(".csv") || s.EndsWith(".xlsx"));
                    foreach (var file in files)
                    {
                        List<Product> fileProducts = csvDataParsing.ReadFile(file);
                        counter = CSVDataParsing.UpdatePrices(currentProducts, fileProducts, counter);
                    }
                }

                // Process the circulaire file
                if (!string.IsNullOrEmpty(circulaireFilePath))
                {
                    List<Product> circulaireProducts = circulaireToCSV.ExtractCirculaireProducts(circulaireFilePath);
                    counter = CSVDataParsing.UpdatePrices(currentProducts, circulaireProducts, counter);
                }

                // Create the new file
                string filepath = fileCreation.CreateFile(currentProducts);
                MessageBox.Show($"{filepath} cr�� sur le bureau!", "Succ�s", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
