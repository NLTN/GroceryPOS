namespace GroceryPOS
{
    partial class MainForm
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
            this.btnProductSample = new System.Windows.Forms.Button();
            this.btnCreateASale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnProductSample
            // 
            this.btnProductSample.Location = new System.Drawing.Point(260, 131);
            this.btnProductSample.Name = "btnProductSample";
            this.btnProductSample.Size = new System.Drawing.Size(137, 23);
            this.btnProductSample.TabIndex = 7;
            this.btnProductSample.Text = "Product sample";
            this.btnProductSample.UseVisualStyleBackColor = true;
            this.btnProductSample.Click += new System.EventHandler(this.btnProductSample_Click);
            // 
            // btnCreateASale
            // 
            this.btnCreateASale.Location = new System.Drawing.Point(260, 160);
            this.btnCreateASale.Name = "btnCreateASale";
            this.btnCreateASale.Size = new System.Drawing.Size(137, 23);
            this.btnCreateASale.TabIndex = 8;
            this.btnCreateASale.Text = "Create a Sale sample";
            this.btnCreateASale.UseVisualStyleBackColor = true;
            this.btnCreateASale.Click += new System.EventHandler(this.btnCreateASale_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(704, 490);
            this.Controls.Add(this.btnCreateASale);
            this.Controls.Add(this.btnProductSample);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnProductSample;
        private System.Windows.Forms.Button btnCreateASale;
    }
}

