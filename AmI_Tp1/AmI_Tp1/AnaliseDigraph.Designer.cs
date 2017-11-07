namespace AmI_Tp1
{
    partial class AnaliseDigraph
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
            this.digraphTB = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.writingTimeMediaTB = new System.Windows.Forms.RichTextBox();
            this.writingTimeDPTB = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // digraphTB
            // 
            this.digraphTB.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.digraphTB.Location = new System.Drawing.Point(24, 90);
            this.digraphTB.Name = "digraphTB";
            this.digraphTB.ReadOnly = true;
            this.digraphTB.Size = new System.Drawing.Size(328, 290);
            this.digraphTB.TabIndex = 2;
            this.digraphTB.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Análise de Grupos";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(382, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tempo de Escrita";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Média";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(382, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Desvio Padrão";
            // 
            // writingTimeMediaTB
            // 
            this.writingTimeMediaTB.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writingTimeMediaTB.Location = new System.Drawing.Point(385, 90);
            this.writingTimeMediaTB.Name = "writingTimeMediaTB";
            this.writingTimeMediaTB.ReadOnly = true;
            this.writingTimeMediaTB.Size = new System.Drawing.Size(242, 35);
            this.writingTimeMediaTB.TabIndex = 7;
            this.writingTimeMediaTB.Text = "";
            // 
            // writingTimeDPTB
            // 
            this.writingTimeDPTB.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.writingTimeDPTB.Location = new System.Drawing.Point(385, 167);
            this.writingTimeDPTB.Name = "writingTimeDPTB";
            this.writingTimeDPTB.ReadOnly = true;
            this.writingTimeDPTB.Size = new System.Drawing.Size(242, 35);
            this.writingTimeDPTB.TabIndex = 8;
            this.writingTimeDPTB.Text = "";
            // 
            // AnaliseDigraph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 442);
            this.Controls.Add(this.writingTimeDPTB);
            this.Controls.Add(this.writingTimeMediaTB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.digraphTB);
            this.Name = "AnaliseDigraph";
            this.Text = "AnaliseDigraph";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox digraphTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox writingTimeMediaTB;
        private System.Windows.Forms.RichTextBox writingTimeDPTB;
    }
}