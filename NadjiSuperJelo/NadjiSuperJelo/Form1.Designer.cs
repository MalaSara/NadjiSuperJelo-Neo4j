namespace NadjiSuperJelo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpboxAutor = new System.Windows.Forms.GroupBox();
            this.btnDodajAutore = new System.Windows.Forms.Button();
            this.btnObrisiAutora = new System.Windows.Forms.Button();
            this.nudIdAutora = new System.Windows.Forms.NumericUpDown();
            this.btnIzmeniAutora = new System.Windows.Forms.Button();
            this.nudIdAutoraZaOcenjivanje = new System.Windows.Forms.NumericUpDown();
            this.numOcena = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGetAutori = new System.Windows.Forms.TextBox();
            this.btnPronadjiAutoraPoImenu = new System.Windows.Forms.Button();
            this.grpboxAutor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdAutora)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdAutoraZaOcenjivanje)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).BeginInit();
            this.SuspendLayout();
            // 
            // grpboxAutor
            // 
            this.grpboxAutor.Controls.Add(this.btnPronadjiAutoraPoImenu);
            this.grpboxAutor.Controls.Add(this.txtGetAutori);
            this.grpboxAutor.Controls.Add(this.label2);
            this.grpboxAutor.Controls.Add(this.label1);
            this.grpboxAutor.Controls.Add(this.numOcena);
            this.grpboxAutor.Controls.Add(this.nudIdAutoraZaOcenjivanje);
            this.grpboxAutor.Controls.Add(this.btnIzmeniAutora);
            this.grpboxAutor.Controls.Add(this.nudIdAutora);
            this.grpboxAutor.Controls.Add(this.btnObrisiAutora);
            this.grpboxAutor.Controls.Add(this.btnDodajAutore);
            this.grpboxAutor.Location = new System.Drawing.Point(12, 12);
            this.grpboxAutor.Name = "grpboxAutor";
            this.grpboxAutor.Size = new System.Drawing.Size(288, 243);
            this.grpboxAutor.TabIndex = 0;
            this.grpboxAutor.TabStop = false;
            this.grpboxAutor.Text = "Autor";
            // 
            // btnDodajAutore
            // 
            this.btnDodajAutore.Location = new System.Drawing.Point(6, 19);
            this.btnDodajAutore.Name = "btnDodajAutore";
            this.btnDodajAutore.Size = new System.Drawing.Size(131, 23);
            this.btnDodajAutore.TabIndex = 1;
            this.btnDodajAutore.Text = "Dodaj Autore";
            this.btnDodajAutore.UseVisualStyleBackColor = true;
            this.btnDodajAutore.Click += new System.EventHandler(this.btnDodajAutore_Click);
            // 
            // btnObrisiAutora
            // 
            this.btnObrisiAutora.Location = new System.Drawing.Point(143, 63);
            this.btnObrisiAutora.Name = "btnObrisiAutora";
            this.btnObrisiAutora.Size = new System.Drawing.Size(139, 23);
            this.btnObrisiAutora.TabIndex = 1;
            this.btnObrisiAutora.Text = "Obrisi Autora";
            this.btnObrisiAutora.UseVisualStyleBackColor = true;
            this.btnObrisiAutora.Click += new System.EventHandler(this.btnObrisiAutora_Click);
            // 
            // nudIdAutora
            // 
            this.nudIdAutora.Location = new System.Drawing.Point(6, 63);
            this.nudIdAutora.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudIdAutora.Name = "nudIdAutora";
            this.nudIdAutora.Size = new System.Drawing.Size(131, 20);
            this.nudIdAutora.TabIndex = 2;
            // 
            // btnIzmeniAutora
            // 
            this.btnIzmeniAutora.Location = new System.Drawing.Point(143, 113);
            this.btnIzmeniAutora.Name = "btnIzmeniAutora";
            this.btnIzmeniAutora.Size = new System.Drawing.Size(139, 23);
            this.btnIzmeniAutora.TabIndex = 3;
            this.btnIzmeniAutora.Text = "Izmeni/Oceni Autora";
            this.btnIzmeniAutora.UseVisualStyleBackColor = true;
            this.btnIzmeniAutora.Click += new System.EventHandler(this.btnIzmeniAutora_Click);
            // 
            // nudIdAutoraZaOcenjivanje
            // 
            this.nudIdAutoraZaOcenjivanje.Location = new System.Drawing.Point(80, 101);
            this.nudIdAutoraZaOcenjivanje.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.nudIdAutoraZaOcenjivanje.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudIdAutoraZaOcenjivanje.Name = "nudIdAutoraZaOcenjivanje";
            this.nudIdAutoraZaOcenjivanje.Size = new System.Drawing.Size(57, 20);
            this.nudIdAutoraZaOcenjivanje.TabIndex = 4;
            this.nudIdAutoraZaOcenjivanje.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // numOcena
            // 
            this.numOcena.Location = new System.Drawing.Point(80, 127);
            this.numOcena.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numOcena.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOcena.Name = "numOcena";
            this.numOcena.Size = new System.Drawing.Size(57, 20);
            this.numOcena.TabIndex = 5;
            this.numOcena.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Id za ocenu:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 129);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Ocena:";
            // 
            // txtGetAutori
            // 
            this.txtGetAutori.Location = new System.Drawing.Point(6, 168);
            this.txtGetAutori.Name = "txtGetAutori";
            this.txtGetAutori.Size = new System.Drawing.Size(131, 20);
            this.txtGetAutori.TabIndex = 9;
            // 
            // btnPronadjiAutoraPoImenu
            // 
            this.btnPronadjiAutoraPoImenu.Location = new System.Drawing.Point(143, 165);
            this.btnPronadjiAutoraPoImenu.Name = "btnPronadjiAutoraPoImenu";
            this.btnPronadjiAutoraPoImenu.Size = new System.Drawing.Size(139, 23);
            this.btnPronadjiAutoraPoImenu.TabIndex = 10;
            this.btnPronadjiAutoraPoImenu.Text = "Pronadji Autora po imenu";
            this.btnPronadjiAutoraPoImenu.UseVisualStyleBackColor = true;
            this.btnPronadjiAutoraPoImenu.Click += new System.EventHandler(this.btnPronadjiAutoraPoImenu_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.grpboxAutor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.grpboxAutor.ResumeLayout(false);
            this.grpboxAutor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdAutora)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudIdAutoraZaOcenjivanje)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOcena)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpboxAutor;
        private System.Windows.Forms.Button btnDodajAutore;
        private System.Windows.Forms.Button btnObrisiAutora;
        private System.Windows.Forms.NumericUpDown nudIdAutora;
        private System.Windows.Forms.Button btnIzmeniAutora;
        private System.Windows.Forms.NumericUpDown numOcena;
        private System.Windows.Forms.NumericUpDown nudIdAutoraZaOcenjivanje;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGetAutori;
        private System.Windows.Forms.Button btnPronadjiAutoraPoImenu;
    }
}

