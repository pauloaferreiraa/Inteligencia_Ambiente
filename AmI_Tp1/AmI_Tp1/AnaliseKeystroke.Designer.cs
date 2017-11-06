namespace AmI_Tp1
{
    partial class AnaliseKeystroke
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
            this.showTop10 = new System.Windows.Forms.RichTextBox();
            this.BackSpaceKey = new System.Windows.Forms.RichTextBox();
            this.labelBackSpace = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // showTop10
            // 
            this.showTop10.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showTop10.Location = new System.Drawing.Point(12, 69);
            this.showTop10.Name = "showTop10";
            this.showTop10.ReadOnly = true;
            this.showTop10.Size = new System.Drawing.Size(234, 203);
            this.showTop10.TabIndex = 1;
            this.showTop10.Text = "";
            // 
            // BackSpaceKey
            // 
            this.BackSpaceKey.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackSpaceKey.Location = new System.Drawing.Point(309, 69);
            this.BackSpaceKey.Name = "BackSpaceKey";
            this.BackSpaceKey.ReadOnly = true;
            this.BackSpaceKey.Size = new System.Drawing.Size(242, 35);
            this.BackSpaceKey.TabIndex = 2;
            this.BackSpaceKey.Text = "";
            // 
            // labelBackSpace
            // 
            this.labelBackSpace.AutoSize = true;
            this.labelBackSpace.Location = new System.Drawing.Point(306, 42);
            this.labelBackSpace.Name = "labelBackSpace";
            this.labelBackSpace.Size = new System.Drawing.Size(181, 13);
            this.labelBackSpace.TabIndex = 3;
            this.labelBackSpace.Text = "Uso do evento Keytroke BackSpace";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 30);
            this.label1.TabIndex = 4;
            this.label1.Text = "Top 10\r\n     Caracter       Valor\r\n";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 356);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelBackSpace);
            this.Controls.Add(this.BackSpaceKey);
            this.Controls.Add(this.showTop10);
            this.Name = "Form2";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox showTop10;
        private System.Windows.Forms.RichTextBox BackSpaceKey;
        private System.Windows.Forms.Label labelBackSpace;
        private System.Windows.Forms.Label label1;
    }
}