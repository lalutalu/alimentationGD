using BottinToCsvForm.Parsing;
using System.Diagnostics;
namespace BottinToCsvForm
{
    public partial class Form1 : Form
    {
        PdfDataParsing pdfDataParsing;
        PdfViandeParsing viandeParsing;
        CSVFileCreation fileCreation;
        CSVDataParsing csvDataParsing;
        CirculaireParsing circulaireToCSV;
        int counter;
        private ToolTip toolTip;

        private List<string> circualairePaths = new List<string>(); // Chemin des circulaires

        private string viandePath; // Chemin du Pdf des viandes
        private List<string> viandeToKeepOriginal = new List<string>(); // Liste des viandes originale
        private List<string> viandeToKeepSurgele = new List<string>(); // Liste des viandes surgelés

        private List<string> bottinPaths = new List<string>(); // Chemin du bottin

        private List<string> extraFiles = new List<string>();
        private List<Product> currentProducts = new List<Product>(); // Liste des produits actuellement traités


        public Form1()
        {
            InitializeComponent();
        }
        #region Tooltip
        private string FormatTooltipText(string text)
        {
            return string.Join(Environment.NewLine, text.Split(new[] { ';' }, StringSplitOptions.None));
        }
        private void extrasPath_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(extrasPath, FormatTooltipText(extrasPath.Text));
        }
        #endregion
        #region Parcourir
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
                bottinPaths.Add(dialog.FileName);
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
                extrasPath.Text = string.Join(";", dialog.FileNames.Select(Path.GetFileName));
                extrasPath.Multiline = true;
            }
        }

        private void parcourirViande_Click(object sender, EventArgs e)
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
        #endregion
        #region Soumettre

        private void InitializeParsers()
        {
            pdfDataParsing = new PdfDataParsing();
            viandeParsing = new PdfViandeParsing();
            fileCreation = new CSVFileCreation(bottinPaths[0]);
            csvDataParsing = new CSVDataParsing();
            circulaireToCSV = new CirculaireParsing();
            counter = 1;
        }

        //private bool AlertBottinEmpty()
        //{
        //    if (bottinPaths.Count == 0 && textBox1.Text == "")
        //    {
        //        MessageBox.Show("Veuillez choisir un bottin...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return true;
        //    }
        //    return false;
        //}

        //private void ValidateBottin()
        //{
        //    currentProducts.Clear();
        //    foreach (var file in bottinPaths)
        //    {
        //        List<string> parsedData = new List<string>(PdfDataParsing.ParsePdf(file));
        //        foreach (var datastring in parsedData)
        //        {
        //            Product product = pdfDataParsing.ParseProducts(datastring);
        //            product.HandleID = $"Produit_{counter}";
        //            currentProducts.Add(product);
        //            counter++;
        //        }
        //    }
        //}

        //private void ValidateCirculaire()
        //{
        //    if (circualairePaths.Count > 0)
        //    {
        //        foreach (var file in circualairePaths)
        //        {
        //            List<Product> circulaireProducts = circulaireToCSV.ExtractCirculaireProducts(file);
        //            counter = CSVDataParsing.UpdatePrices(currentProducts, circulaireProducts, counter);
        //        }
        //    }
        //}

        //private void ValidateExtraFiles()
        //{
        //    if (extraFiles.Count > 0)
        //    {
        //        foreach (var file in extraFiles)
        //        {
        //            List<Product> fileProducts = csvDataParsing.ReadFile(file);
        //            counter = CSVDataParsing.UpdatePrices(currentProducts, fileProducts, counter);
        //        }
        //    }
        //}

        //private async Task<bool> ValidateViande()
        //{
        //    if (!string.IsNullOrEmpty(textBox3.Text))
        //    {
        //        if (string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(sugelerText.Text))
        //        {
        //            MessageBox.Show("Veuillez choisir une plage de numéros pour la viande souhaitée dans les deux catégories", "Pas de plage!", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            return false;
        //        }
        //        var sections = textBox4.Text.Split(";");
        //        foreach (var section in sections)
        //        {
        //            viandeToKeepOriginal.Add(section);
        //        }
        //        var sectionsSurgeler = sugelerText.Text.Split(";");
        //        foreach (var section in sectionsSurgeler)
        //        {
        //            viandeToKeepSurgele.Add(section);
        //        }

        //        List<string> viandeStrings = PdfViandeParsing.ParsePdf(viandePath, viandeToKeepOriginal);
        //        List<Product> viandeProductsOriginal = new List<Product>();
        //        foreach (var viandeString in viandeStrings)
        //        {
        //            viandeProductsOriginal.Add(viandeParsing.ParseViande(viandeString.Trim(), "Viande et produit frais"));
        //        }

        //        List<string> viandeStringSurgeler = PdfViandeParsing.ParsePdf(viandePath, viandeToKeepSurgele);
        //        List<Product> viandeProductsSurgeler = new List<Product>();
        //        foreach (var codeSurgeler in viandeStringSurgeler)
        //        {
        //            viandeProductsSurgeler.Add(viandeParsing.ParseViande(codeSurgeler, "Viande Surgele"));
        //        }
        //        counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsOriginal, counter);
        //        counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsSurgeler, counter);
        //        return true;
        //    }
        //    return false;
        //}

        //private async void soumettre_Click(object sender, EventArgs e)
        //{
        //    if (AlertBottinEmpty())
        //    {
        //        return;
        //    }
        //    InitializeParsers();
        //    ValidateBottin();
        //    ValidateCirculaire();
        //    ValidateExtraFiles();
        //    if (!await ValidateViande())
        //    {
        //        return;
        //    }
        //    string filepath = fileCreation.CreateFile(currentProducts);
        //    MessageBox.Show($"{filepath} créé sur le bureau!", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        private void soumettre_Click(object sender, EventArgs e)
        {
            if (bottinPaths.Count == 0 && textBox1.Text == "")
            {
                MessageBox.Show("Veuillez choisir un bottin...", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //PdfDataParsing pdfDataParsing = new PdfDataParsing();
            //PdfViandeParsing viandeParsing = new PdfViandeParsing();
            //CSVFileCreation fileCreation = new CSVFileCreation(bottinPaths[0]);
            //CSVDataParsing csvDataParsing = new CSVDataParsing();
            //CirculaireParsing circulaireToCSV = new CirculaireParsing();
            //int counter = 1;
            InitializeParsers();

            try
            {
                currentProducts.Clear();
                foreach (var file in bottinPaths)
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

                    List<string> viandeStrings = PdfViandeParsing.ParsePdf(viandePath, viandeToKeepOriginal);
                    List<Product> viandeProductsOriginal = new List<Product>();
                    foreach (var viandeString in viandeStrings)
                    {
                        viandeProductsOriginal.Add(viandeParsing.ParseViande(viandeString.Trim(), "Viande et produit frais"));
                    }

                    List<string> viandeStringSurgeler = PdfViandeParsing.ParsePdf(viandePath, viandeToKeepSurgele);
                    List<Product> viandeProductsSurgeler = new List<Product>();
                    foreach (var codeSurgeler in viandeStringSurgeler)
                    {
                        viandeProductsSurgeler.Add(viandeParsing.ParseViande(codeSurgeler, "Viande Surgele"));
                    }
                    counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsOriginal, counter);
                    counter = CSVDataParsing.UpdatePrices(currentProducts, viandeProductsSurgeler, counter);
                }


                List<List<Product>> products = fileCreation.SplitListIntoChunks(currentProducts, 4999);
                string message = "";
                foreach (var list in products)
                {
                    string filepath = fileCreation.CreateFile(list);
                    MessageBox.Show($"{list.Count}", "Fichiers créé sur le bureau!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    message += $"{filepath}\n";
                }
                MessageBox.Show(message, "Fichiers créé sur le bureau!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Oops, une erreur est survenue: {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Effacer
        private void effacer_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "";
            bottinPaths.Clear();
        }

        private void effacerCSV_Click(object sender, EventArgs e)
        {
            extrasPath.Text = "";
            extraFiles.Clear();
        }

        private void effacerCirculaire_Click(object sender, EventArgs e)
        {
            circulairePath.Text = "";
            circualairePaths.Clear();
        }
        private void effacerCheminViande_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            viandePath = "";
        }
        private void effacerViandeFrais_Click(object sender, EventArgs e)
        {
            textBox4.Text = "";
            viandeToKeepOriginal.Clear();
        }

        private void effacerViandeSurgele_Click(object sender, EventArgs e)
        {
            sugelerText.Text = "";
            viandeToKeepSurgele.Clear();
        }
        #endregion
        #region Label
        private void formatCSVLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = Path.Combine(baseDir, "Format des fichiers supplémentaires.pdf");

            try
            {
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors de la lecture du PDF:" + ex.Message);
            }
        }


        private void instructionsLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string pdfPath = Path.Combine(baseDir, "Instructions.pdf");

            try
            {
                Process.Start(new ProcessStartInfo(pdfPath) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue lors de la lecture du PDF:" + ex.Message);
            }
        }
        #endregion
    }
}
