namespace AmI_Tp1
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.EscolheLog = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.MostraResult = new System.Windows.Forms.Button();
            this.textBoxUt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.VerResult = new System.Windows.Forms.ComboBox();
            this.labelMostraRes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // EscolheLog
            // 
            this.EscolheLog.Location = new System.Drawing.Point(92, 81);
            this.EscolheLog.Name = "EscolheLog";
            this.EscolheLog.Size = new System.Drawing.Size(129, 23);
            this.EscolheLog.TabIndex = 0;
            this.EscolheLog.Text = "Escolher ficheiro";
            this.EscolheLog.UseVisualStyleBackColor = true;
            this.EscolheLog.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // MostraResult
            // 
            this.MostraResult.Enabled = false;
            this.MostraResult.Location = new System.Drawing.Point(268, 234);
            this.MostraResult.Name = "MostraResult";
            this.MostraResult.Size = new System.Drawing.Size(37, 23);
            this.MostraResult.TabIndex = 7;
            this.MostraResult.Text = "Ok";
            this.MostraResult.UseVisualStyleBackColor = true;
            this.MostraResult.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxUt
            // 
            this.textBoxUt.Location = new System.Drawing.Point(92, 40);
            this.textBoxUt.Name = "textBoxUt";
            this.textBoxUt.Size = new System.Drawing.Size(155, 20);
            this.textBoxUt.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Utilizador";
            // 
            // VerResult
            // 
            this.VerResult.FormattingEnabled = true;
            this.VerResult.Items.AddRange(new object[] {
            "Análise de eventos Keystroke",
            "Análise de eventos Digraph",
            "Análise de eventos de Palavras"});
            this.VerResult.Location = new System.Drawing.Point(92, 196);
            this.VerResult.Name = "VerResult";
            this.VerResult.Size = new System.Drawing.Size(155, 21);
            this.VerResult.TabIndex = 10;
            // 
            // labelMostraRes
            // 
            this.labelMostraRes.AutoSize = true;
            this.labelMostraRes.Location = new System.Drawing.Point(54, 162);
            this.labelMostraRes.Name = "labelMostraRes";
            this.labelMostraRes.Size = new System.Drawing.Size(193, 13);
            this.labelMostraRes.TabIndex = 11;
            this.labelMostraRes.Text = "Selecione o resultado que pretende ver\r\n";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 323);
            this.Controls.Add(this.labelMostraRes);
            this.Controls.Add(this.VerResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxUt);
            this.Controls.Add(this.MostraResult);
            this.Controls.Add(this.EscolheLog);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button EscolheLog;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button MostraResult;
        private System.Windows.Forms.TextBox textBoxUt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox VerResult;
        private System.Windows.Forms.Label labelMostraRes;
    }
}

