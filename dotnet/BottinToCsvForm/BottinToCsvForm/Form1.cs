using BottinToCsvForm.Parsing;

namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        private List<String> selectedFiles = new List<String>();
        public Form1()
        {
            InitializeComponent();
        }

        private void effacer_click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            selectedFiles.Clear();
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
                selectedFiles.Clear();
                selectedFiles.Add(dialog.FileName);
                textBox1.Text = dialog.FileName;
            };
        }

        private void soumettre_Click(object sender, EventArgs e)
        {
            if (selectedFiles.Count == 0 && textBox1.Text == "")
            {
                MessageBox.Show("Veuillez choisir un bottin...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (selectedFiles.Count > 1)
            {
                MessageBox.Show("Veuillez sélectionner un bottin à la fois.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            PdfDataParsing pdfDataParsing = new PdfDataParsing();
            FileCreation fileCreation = new FileCreation();
            CSVFile file = new CSVFile();
            int counter = 0;
            List<string> parsedData = new List<string>(PdfDataParsing.ParsePdf(selectedFiles[0]));
            try
            {
                //    List<Product> productsToBeDeleted = new List<Product>();
                //    foreach (var datastring in deleteData)
                //    {
                //        Product product = pdfDataParsing.ParseProducts(datastring);
                //        productsToBeDeleted.Add(product);
                //    }

                List<Product> products = new List<Product>();
                foreach (var datastring in parsedData)
                {
                    counter++;
                    Product product = pdfDataParsing.ParseProducts(datastring);
                    product.HandleID = $"Produit_{counter}";
                    products.Add(product);
                }
                //CSVFile.UpdatePrices(lastWeek, products);
                //CSVFile.DeleteProducts(products, productsToBeDeleted);
                Console.WriteLine(products.Count);
                string filepath = fileCreation.CreateFile(products);
                MessageBox.Show($"{filepath} créé sur le bureau!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
