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
            label5 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            tabPage4 = new TabPage();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            button4 = new Button();
            sugelerText = new TextBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // effacer
            // 
            effacer.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            effacer.Location = new Point(643, 200);
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
            soumettre.Location = new Point(520, 541);
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
            parcourir.Location = new Point(456, 200);
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
            titre.Location = new Point(476, 38);
            titre.Name = "titre";
            titre.Size = new Size(289, 30);
            titre.TabIndex = 3;
            titre.Text = "Convertir Bottin en CSV Wix";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            textBox1.Location = new Point(456, 113);
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
            textBox2.Location = new Point(437, 135);
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
            parcourirCSV.Location = new Point(437, 220);
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
            effacerCSV.Location = new Point(624, 220);
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
            label1.Location = new Point(501, 68);
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
            label2.Location = new Point(423, 90);
            label2.Name = "label2";
            label2.Size = new Size(330, 23);
            label2.TabIndex = 11;
            label2.Text = "Fichiers supplémentaires en XLSX ou CSV";
            // 
            // circulairePath
            // 
            circulairePath.BackColor = Color.White;
            circulairePath.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            circulairePath.Location = new Point(463, 114);
            circulairePath.Multiline = true;
            circulairePath.Name = "circulairePath";
            circulairePath.ReadOnly = true;
            circulairePath.Size = new Size(281, 60);
            circulairePath.TabIndex = 12;
            // 
            // parcourirCirculaire
            // 
            parcourirCirculaire.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            parcourirCirculaire.Location = new Point(463, 214);
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
            effacerCirculaire.Location = new Point(629, 214);
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
            label3.Location = new Point(501, 74);
            label3.Name = "label3";
            label3.Size = new Size(202, 23);
            label3.TabIndex = 15;
            label3.Text = "Fichier Circulaire en PDF";
            // 
            // button1
            // 
            button1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button1.Location = new Point(740, 84);
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
            textBox3.Location = new Point(443, 106);
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
            textBox4.Location = new Point(77, 239);
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
            label4.Location = new Point(550, 26);
            label4.Name = "label4";
            label4.Size = new Size(65, 23);
            label4.TabIndex = 19;
            label4.Text = "Viande";
            // 
            // button2
            // 
            button2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button2.Location = new Point(740, 118);
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
            button3.Location = new Point(374, 255);
            button3.Name = "button3";
            button3.Size = new Size(94, 28);
            button3.TabIndex = 21;
            button3.Text = "Effacer";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label5.Location = new Point(162, 302);
            label5.Name = "label5";
            label5.Size = new Size(118, 23);
            label5.TabIndex = 22;
            label5.Text = "Separateur \";\"";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new Point(-1, 106);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1199, 404);
            tabControl1.TabIndex = 23;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.WhiteSmoke;
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(textBox1);
            tabPage1.Controls.Add(parcourir);
            tabPage1.Controls.Add(effacer);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1191, 376);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "BOTTIN";
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.WhiteSmoke;
            tabPage2.Controls.Add(circulairePath);
            tabPage2.Controls.Add(parcourirCirculaire);
            tabPage2.Controls.Add(effacerCirculaire);
            tabPage2.Controls.Add(label3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1191, 376);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "CIRCULAIRE";
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.WhiteSmoke;
            tabPage3.Controls.Add(textBox2);
            tabPage3.Controls.Add(effacerCSV);
            tabPage3.Controls.Add(parcourirCSV);
            tabPage3.Controls.Add(label2);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1191, 376);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "EXTRAS";
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.WhiteSmoke;
            tabPage4.Controls.Add(label9);
            tabPage4.Controls.Add(label8);
            tabPage4.Controls.Add(label7);
            tabPage4.Controls.Add(button4);
            tabPage4.Controls.Add(sugelerText);
            tabPage4.Controls.Add(label6);
            tabPage4.Controls.Add(textBox3);
            tabPage4.Controls.Add(label5);
            tabPage4.Controls.Add(button1);
            tabPage4.Controls.Add(button3);
            tabPage4.Controls.Add(textBox4);
            tabPage4.Controls.Add(button2);
            tabPage4.Controls.Add(label4);
            tabPage4.Location = new Point(4, 24);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1191, 376);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "VIANDE";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.White;
            label9.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label9.Location = new Point(99, 200);
            label9.Name = "label9";
            label9.Size = new Size(239, 23);
            label9.TabIndex = 28;
            label9.Text = "Metro Viande et produit frais ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.White;
            label8.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label8.Location = new Point(815, 200);
            label8.Name = "label8";
            label8.Size = new Size(117, 23);
            label8.TabIndex = 27;
            label8.Text = "Metro Surgele";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.White;
            label7.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label7.Location = new Point(814, 302);
            label7.Name = "label7";
            label7.Size = new Size(118, 23);
            label7.TabIndex = 26;
            label7.Text = "Separateur \";\"";
            // 
            // button4
            // 
            button4.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold);
            button4.Location = new Point(1026, 255);
            button4.Name = "button4";
            button4.Size = new Size(94, 28);
            button4.TabIndex = 25;
            button4.Text = "Effacer";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // sugelerText
            // 
            sugelerText.AcceptsReturn = true;
            sugelerText.BackColor = Color.White;
            sugelerText.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold);
            sugelerText.Location = new Point(729, 239);
            sugelerText.Multiline = true;
            sugelerText.Name = "sugelerText";
            sugelerText.Size = new Size(281, 60);
            sugelerText.TabIndex = 24;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.White;
            label6.Font = new Font("Segoe UI Semibold", 12.2F, FontStyle.Bold | FontStyle.Italic);
            label6.Location = new Point(550, 69);
            label6.Name = "label6";
            label6.Size = new Size(70, 23);
            label6.TabIndex = 23;
            label6.Text = "Chemin";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1194, 634);
            Controls.Add(tabControl1);
            Controls.Add(pictureBox1);
            Controls.Add(titre);
            Controls.Add(soumettre);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Convertisseur CSV Wix";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            tabPage4.PerformLayout();
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
        private Label label5;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label label6;
        private Label label7;
        private Button button4;
        private TextBox sugelerText;
        private Label label9;
        private Label label8;
    }
}
