namespace ImageSoundProcessing
{
    partial class ProcessedImageWindow
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
            this.processedImage = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.factorSlider = new System.Windows.Forms.TrackBar();
            this.factorLabel = new System.Windows.Forms.Label();
            this.maskButton = new System.Windows.Forms.Button();
            this.maskLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.processedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // processedImage
            // 
            this.processedImage.Location = new System.Drawing.Point(12, 91);
            this.processedImage.Name = "processedImage";
            this.processedImage.Size = new System.Drawing.Size(69, 72);
            this.processedImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.processedImage.TabIndex = 0;
            this.processedImage.TabStop = false;
            this.processedImage.Click += new System.EventHandler(this.ProcessedImage_Click);
            // 
            // saveButton
            // 
            this.saveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.saveButton.Location = new System.Drawing.Point(12, 12);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(156, 44);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // factorSlider
            // 
            this.factorSlider.Location = new System.Drawing.Point(184, 12);
            this.factorSlider.Maximum = 255;
            this.factorSlider.Minimum = -255;
            this.factorSlider.Name = "factorSlider";
            this.factorSlider.Size = new System.Drawing.Size(1066, 56);
            this.factorSlider.TabIndex = 2;
            this.factorSlider.Visible = false;
            this.factorSlider.Scroll += new System.EventHandler(this.FactorSlider_Scroll);
            // 
            // factorLabel
            // 
            this.factorLabel.AutoSize = true;
            this.factorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.factorLabel.Location = new System.Drawing.Point(708, 43);
            this.factorLabel.Name = "factorLabel";
            this.factorLabel.Size = new System.Drawing.Size(23, 25);
            this.factorLabel.TabIndex = 3;
            this.factorLabel.Text = "0";
            this.factorLabel.Visible = false;
            // 
            // maskButton
            // 
            this.maskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maskButton.Location = new System.Drawing.Point(193, 12);
            this.maskButton.Name = "maskButton";
            this.maskButton.Size = new System.Drawing.Size(160, 44);
            this.maskButton.TabIndex = 4;
            this.maskButton.Text = "Mask:              ";
            this.maskButton.UseVisualStyleBackColor = true;
            this.maskButton.Visible = false;
            this.maskButton.Click += new System.EventHandler(this.MaskButton_Click);
            // 
            // maskLabel
            // 
            this.maskLabel.AutoSize = true;
            this.maskLabel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.maskLabel.Enabled = false;
            this.maskLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maskLabel.Location = new System.Drawing.Point(269, 22);
            this.maskLabel.Name = "maskLabel";
            this.maskLabel.Size = new System.Drawing.Size(23, 25);
            this.maskLabel.TabIndex = 5;
            this.maskLabel.Text = "0";
            this.maskLabel.Visible = false;
            this.maskLabel.Click += new System.EventHandler(this.MaskLabel_Click);
            // 
            // ProcessedImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Controls.Add(this.maskLabel);
            this.Controls.Add(this.maskButton);
            this.Controls.Add(this.factorLabel);
            this.Controls.Add(this.factorSlider);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.processedImage);
            this.Name = "ProcessedImageWindow";
            this.Text = "Processed image";
            this.Load += new System.EventHandler(this.ProcessedImageWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.processedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox processedImage;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TrackBar factorSlider;
        private System.Windows.Forms.Label factorLabel;
        private System.Windows.Forms.Button maskButton;
        private System.Windows.Forms.Label maskLabel;
    }
}