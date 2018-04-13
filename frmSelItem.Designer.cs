namespace CardGenIDE
{
    partial class frmSelItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelItem));
            this.btnOK = new System.Windows.Forms.Button();
            this.rbText = new System.Windows.Forms.RadioButton();
            this.rbImage = new System.Windows.Forms.RadioButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.rbLabel = new System.Windows.Forms.RadioButton();
            this.rbShape = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(109, 143);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "Ok";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // rbText
            // 
            this.rbText.AutoSize = true;
            this.rbText.Location = new System.Drawing.Point(12, 39);
            this.rbText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbText.Name = "rbText";
            this.rbText.Size = new System.Drawing.Size(56, 21);
            this.rbText.TabIndex = 1;
            this.rbText.TabStop = true;
            this.rbText.Text = "Text";
            this.rbText.UseVisualStyleBackColor = true;
            // 
            // rbImage
            // 
            this.rbImage.AutoSize = true;
            this.rbImage.Location = new System.Drawing.Point(12, 66);
            this.rbImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbImage.Name = "rbImage";
            this.rbImage.Size = new System.Drawing.Size(67, 21);
            this.rbImage.TabIndex = 2;
            this.rbImage.TabStop = true;
            this.rbImage.Text = "Image";
            this.rbImage.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(191, 143);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // rbLabel
            // 
            this.rbLabel.AutoSize = true;
            this.rbLabel.Location = new System.Drawing.Point(12, 12);
            this.rbLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbLabel.Name = "rbLabel";
            this.rbLabel.Size = new System.Drawing.Size(64, 21);
            this.rbLabel.TabIndex = 4;
            this.rbLabel.TabStop = true;
            this.rbLabel.Text = "Label";
            this.rbLabel.UseVisualStyleBackColor = true;
            // 
            // rbShape
            // 
            this.rbShape.AutoSize = true;
            this.rbShape.Location = new System.Drawing.Point(12, 92);
            this.rbShape.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbShape.Name = "rbShape";
            this.rbShape.Size = new System.Drawing.Size(70, 21);
            this.rbShape.TabIndex = 5;
            this.rbShape.TabStop = true;
            this.rbShape.Text = "Shape";
            this.rbShape.UseVisualStyleBackColor = true;
            // 
            // frmSelItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(277, 177);
            this.Controls.Add(this.rbShape);
            this.Controls.Add(this.rbLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.rbImage);
            this.Controls.Add(this.rbText);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSelItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New object";
            this.Load += new System.EventHandler(this.frmSelItem_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.RadioButton rbText;
        private System.Windows.Forms.RadioButton rbImage;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RadioButton rbLabel;
        private System.Windows.Forms.RadioButton rbShape;
    }
}