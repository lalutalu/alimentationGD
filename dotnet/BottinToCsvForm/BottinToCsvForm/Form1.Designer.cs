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
            components = new System.ComponentModel.Container();
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
            circulairePath = new TextBox();
            parcourirCirculaire = new Button();
            effacerCirculaire = new Button();
            label3 = new Label();
            toolTip1 = new ToolTip(components);
            button1 = new Button();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // effacer
            // 
            effacer.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            effacer.Location = new Point(416, 197);
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
            soumettre.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            soumettre.Location = new Point(517, 444);
            soumettre.Name = "soumettre";
            soumettre.Size = new Size(114, 51);
            soumettre.TabIndex = 1;
            soumettre.Text = "Soumettre";
            soumettre.UseVisualStyleBackColor = false;
            soumettre.Click += soumettre_Click;
            // 
            // parcourir
            // 
            parcourir.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            parcourir.Location = new Point(229, 197);
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
            titre.Font = new Font("Segoe UI Semibold", 16.2F, FontStyle.Bold | FontStyle.Italic);
            titre.Location = new Point(448, 32);
            titre.Name = "titre";
            titre.Size = new Size(289, 30);
            titre.TabIndex = 3;
            titre.Text = "Convertir Bottin en CSV Wix";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox1.Location = new Point(229, 126);
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
            textBox2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox2.Location = new Point(681, 126);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.Size = new Size(281, 60);
            textBox2.TabIndex = 6;
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // parcourirCSV
            // 
            parcourirCSV.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            parcourirCSV.Location = new Point(681, 192);
            parcourirCSV.Name = "parcourirCSV";
            parcourirCSV.Size = new Size(94, 28);
            parcourirCSV.TabIndex = 9;
            parcourirCSV.Text = "Parcourir";
            parcourirCSV.UseVisualStyleBackColor = true;
            parcourirCSV.Click += parcourirCSV_Click;
            // 
            // effacerCSV
            // 
            effacerCSV.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            effacerCSV.Location = new Point(868, 192);
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
            label1.Location = new Point(283, 100);
            label1.Name = "label1";
            label1.Size = new Size(173, 23);
            label1.TabIndex = 10;
            label1.Text = "Fichier Bottin en PDF";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label2.Location = new Point(656, 100);
            label2.Name = "label2";
            label2.Size = new Size(330, 23);
            label2.TabIndex = 11;
            label2.Text = "Fichiers supplémentaires en XLSX ou CSV";
            // 
            // circulairePath
            // 
            circulairePath.BackColor = Color.White;
            circulairePath.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            circulairePath.Location = new Point(229, 308);
            circulairePath.Multiline = true;
            circulairePath.Name = "circulairePath";
            circulairePath.ReadOnly = true;
            circulairePath.Size = new Size(281, 60);
            circulairePath.TabIndex = 12;
            // 
            // parcourirCirculaire
            // 
            parcourirCirculaire.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            parcourirCirculaire.Location = new Point(229, 374);
            parcourirCirculaire.Name = "parcourirCirculaire";
            parcourirCirculaire.Size = new Size(94, 28);
            parcourirCirculaire.TabIndex = 13;
            parcourirCirculaire.Text = "Parcourir";
            parcourirCirculaire.UseVisualStyleBackColor = true;
            parcourirCirculaire.Click += parcourirCirculaire_Click;
            // 
            // effacerCirculaire
            // 
            effacerCirculaire.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            effacerCirculaire.Location = new Point(395, 374);
            effacerCirculaire.Name = "effacerCirculaire";
            effacerCirculaire.Size = new Size(94, 28);
            effacerCirculaire.TabIndex = 14;
            effacerCirculaire.Text = "Effacer";
            effacerCirculaire.UseVisualStyleBackColor = true;
            effacerCirculaire.Click += effacerCirculaire_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.White;
            label3.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label3.Location = new Point(272, 282);
            label3.Name = "label3";
            label3.Size = new Size(202, 23);
            label3.TabIndex = 15;
            label3.Text = "Fichier Circulaire en PDF";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button1.Location = new Point(953, 260);
            button1.Name = "button1";
            button1.Size = new Size(94, 28);
            button1.TabIndex = 16;
            button1.Text = "Parcourir";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox3
            // 
            textBox3.BackColor = Color.White;
            textBox3.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox3.Location = new Point(656, 282);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.Size = new Size(281, 25);
            textBox3.TabIndex = 17;
            // 
            // textBox4
            // 
            textBox4.AcceptsReturn = true;
            textBox4.BackColor = Color.White;
            textBox4.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox4.Location = new Point(656, 342);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(281, 60);
            textBox4.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.White;
            label4.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label4.Location = new Point(778, 256);
            label4.Name = "label4";
            label4.Size = new Size(65, 23);
            label4.TabIndex = 19;
            label4.Text = "Viande";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button2.Location = new Point(953, 294);
            button2.Name = "button2";
            button2.Size = new Size(94, 28);
            button2.TabIndex = 20;
            button2.Text = "Effacer";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button3.Location = new Point(953, 358);
            button3.Name = "button3";
            button3.Size = new Size(94, 28);
            button3.TabIndex = 21;
            button3.Text = "Effacer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1123, 533);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Controls.Add(label3);
            Controls.Add(effacerCirculaire);
            Controls.Add(parcourirCirculaire);
            Controls.Add(circulairePath);
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
        private TextBox circulairePath;
        private Button parcourirCirculaire;
        private Button effacerCirculaire;
        private Label label3;
        private ToolTip toolTip1;
        private Button button1;
        private TextBox textBox3;
        private TextBox textBox4;
        private Label label4;
        private Button button2;
        private Button button3;
    }
}
