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
            effacer = new Button();
            soumettre = new Button();
            parcourir = new Button();
            titre = new Label();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // effacer
            // 
            effacer.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            effacer.Location = new Point(567, 173);
            effacer.Name = "effacer";
            effacer.Size = new Size(94, 28);
            effacer.TabIndex = 0;
            effacer.Text = "Effacer";
            effacer.UseVisualStyleBackColor = true;
            // 
            // soumettre
            // 
            soumettre.BackColor = Color.FromArgb(0, 192, 0);
            soumettre.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            soumettre.Location = new Point(449, 253);
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
            parcourir.Location = new Point(280, 259);
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
            titre.Location = new Point(265, 83);
            titre.Name = "titre";
            titre.Size = new Size(289, 30);
            titre.TabIndex = 3;
            titre.Text = "Convertir Bottin en CSV Wix";
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.White;
            textBox1.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            textBox1.Location = new Point(280, 146);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(263, 83);
            textBox1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(titre);
            Controls.Add(parcourir);
            Controls.Add(soumettre);
            Controls.Add(effacer);
            Name = "Form1";
            Text = "Convertisseur CSV Wix";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button effacer;
        private Button soumettre;
        private Button parcourir;
        private Label titre;
        private TextBox textBox1;
    }
}
