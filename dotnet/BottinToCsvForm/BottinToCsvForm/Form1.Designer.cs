namespace BottinToCsvForm
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            effacer = new Button();
            soumettre = new Button();
            parcourir = new Button();
            titre = new Label();
            textBox1 = new TextBox();
            pictureBox1 = new PictureBox();
            textBox2 = new TextBox();
            parcourirCSV = new Button();
            effacerCSV = new Button();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // effacer
            // 
            effacer.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            effacer.Location = new Point(554, 126);
            effacer.Name = "effacer";
            effacer.Size = new Size(94, 28);
            effacer.TabIndex = 0;
            effacer.Text = "Effacer";
            effacer.UseVisualStyleBackColor = true;
            effacer.Click += effacer_Click_1;
            // 
            // soumettre
            // 
            soumettre.BackColor = Color.FromArgb(0, 192, 0);
            soumettre.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            soumettre.Location = new Point(325, 334);
            soumettre.Name = "soumettre";
            soumettre.Size = new Size(114, 51);
            soumettre.TabIndex = 1;
            soumettre.Text = "Soumettre";
            soumettre.UseVisualStyleBackColor = false;
            soumettre.Click += soumettre_Click;
            // 
            // parcourir
            // 
            parcourir.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            parcourir.Location = new Point(554, 92);
            parcourir.Name = "parcourir";
            parcourir.Size = new Size(94, 28);
            parcourir.TabIndex = 2;
            parcourir.Text = "Parcourir";
            parcourir.UseVisualStyleBackColor = true;
            parcourir.Click += parcourir_Click;
            // 
            // titre
            // 
            titre.AutoSize = true;
            titre.BackColor = Color.White;
            titre.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            titre.Location = new Point(472, 26);
            titre.Name = "titre";
            titre.Size = new Size(289, 30);
            titre.TabIndex = 3;
            titre.Text = "Convertir Bottin en CSV Wix";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(228, 92);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(281, 58);
            textBox1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(317, 74);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.White;
            textBox2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox2.Location = new Point(228, 205);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(281, 60);
            textBox2.TabIndex = 6;
            // 
            // parcourirCSV
            // 
            parcourirCSV.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            parcourirCSV.Location = new Point(554, 207);
            parcourirCSV.Name = "parcourirCSV";
            parcourirCSV.Size = new Size(94, 28);
            parcourirCSV.TabIndex = 9;
            parcourirCSV.Text = "Parcourir";
            parcourirCSV.UseVisualStyleBackColor = true;
            parcourirCSV.Click += parcourirCSV_Click;
            // 
            // effacerCSV
            // 
            effacerCSV.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            effacerCSV.Location = new Point(554, 241);
            effacerCSV.Name = "effacerCSV";
            effacerCSV.Size = new Size(94, 28);
            effacerCSV.TabIndex = 8;
            effacerCSV.Text = "Effacer";
            effacerCSV.UseVisualStyleBackColor = true;
            effacerCSV.Click += effacerCSV_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label1.Location = new Point(228, 161);
            label1.Name = "label1";
            label1.Size = new Size(294, 23);
            label1.TabIndex = 10;
            label1.Text = "Sélectionnez le bottin en format PDF";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(139, 276);
            label2.Name = "label2";
            label2.Size = new Size(409, 23);
            label2.TabIndex = 11;
            label2.Text = "Dossier contenant des fichiers CSV supplémentaires";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(parcourirCSV);
            Controls.Add(effacerCSV);
            Controls.Add(textBox2);
            Controls.Add(pictureBox1);
            Controls.Add(textBox1);
            Controls.Add(titre);
            Controls.Add(parcourir);
            Controls.Add(soumettre);
            Controls.Add(effacer);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Convertisseur CSV Wix";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button effacer;
        private Button soumettre;
        private Button parcourir;
        private Label titre;
        private TextBox textBox1;
        private PictureBox pictureBox1;
        private TextBox textBox2;
        private Button parcourirCSV;
        private Button effacerCSV;
        private Label label1;
        private Label label2;
    }
}
