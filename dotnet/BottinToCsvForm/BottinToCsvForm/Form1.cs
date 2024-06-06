using BottinToCsvForm.Parsing;

namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        private List<String> selectedFiles = new List<String>();
        private string selectedFolder;
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

        private void effacerCSV_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            selectedFolder = "";
        }

        private void parcourir_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                MessageBox.Show("Vous avez deja choisis un bottin...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                selectedFiles.Clear();
                selectedFiles.Add(dialog.FileName);
                textBox1.Text = dialog.FileName;
            }
        }
        private void parcourirCSV_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "Sélectionner un dossier contenant des fichiers CSV ou XLSX"
            };
            DialogResult result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                selectedFolder = dialog.SelectedPath;
                textBox2.Text = selectedFolder;
                MessageBox.Show($"Selected Folder: {selectedFolder}");
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
            int counter = 1;

            try
            {
                List<string> parsedData = new List<string>(PdfDataParsing.ParsePdf(selectedFiles[0]));
                currentProducts.Clear();

                foreach (var datastring in parsedData)
                {
                    Product product = pdfDataParsing.ParseProducts(datastring);
                    product.HandleID = $"Produit_{counter}";
                    product.Category = "Produits Secs";
                    currentProducts.Add(product);
                    counter++;
                }

                if (!string.IsNullOrEmpty(selectedFolder))
                {
                    var files = Directory.GetFiles(selectedFolder, "*.*").Where(s => s.EndsWith(".csv") || s.EndsWith(".xlsx"));
                    foreach (var file in files)
                    {
                        List<Product> fileProducts = csvDataParsing.ReadFile(file);
                        counter = CSVDataParsing.UpdatePrices(currentProducts, fileProducts, counter);
                    }
                }

                string filepath = fileCreation.CreateFile(currentProducts);
                MessageBox.Show($"{filepath} créé sur le bureau!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void effacer_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            selectedFiles.Clear();
        }
    }
}
