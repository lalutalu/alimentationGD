namespace CIrculaireToPdf
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
            parcourir = new Button();
            soumettre = new Button();
            filePath = new TextBox();
            titre = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // parcourir
            // 
            parcourir.Location = new Point(239, 260);
            parcourir.Name = "parcourir";
            parcourir.Size = new Size(111, 29);
            parcourir.TabIndex = 0;
            parcourir.Text = "PARCOURIR";
            parcourir.UseVisualStyleBackColor = true;
            parcourir.Click += parcourir_Click;
            // 
            // soumettre
            // 
            soumettre.Location = new Point(464, 260);
            soumettre.Name = "soumettre";
            soumettre.Size = new Size(110, 29);
            soumettre.TabIndex = 1;
            soumettre.Text = "SOUMETTRE";
            soumettre.UseVisualStyleBackColor = true;
            soumettre.Click += soumettre_Click;
            // 
            // filePath
            // 
            filePath.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            filePath.Location = new Point(215, 206);
            filePath.Name = "filePath";
            filePath.Size = new Size(386, 30);
            filePath.TabIndex = 2;
            // 
            // titre
            // 
            titre.AutoSize = true;
            titre.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            titre.Location = new Point(262, 146);
            titre.Name = "titre";
            titre.Size = new Size(306, 31);
            titre.TabIndex = 3;
            titre.Text = "Convertir Circulaire en PDF";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(398, 80);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pictureBox1);
            Controls.Add(titre);
            Controls.Add(filePath);
            Controls.Add(soumettre);
            Controls.Add(parcourir);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Convertir Circulaire en PDF";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button parcourir;
        private Button soumettre;
        private TextBox filePath;
        private Label titre;
        private PictureBox pictureBox1;
    }
}
