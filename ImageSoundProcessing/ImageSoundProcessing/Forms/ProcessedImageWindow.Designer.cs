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
            this.components = new System.ComponentModel.Container();
            this.processedImage = new System.Windows.Forms.PictureBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.factorSlider = new System.Windows.Forms.TrackBar();
            this.factorLabel = new System.Windows.Forms.Label();
            this.maskButton = new System.Windows.Forms.Button();
            this.maskLabel = new System.Windows.Forms.Label();
            this.SliderName1 = new System.Windows.Forms.Label();
            this.SliderName2 = new System.Windows.Forms.Label();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.factorSlider2 = new System.Windows.Forms.TrackBar();
            this.factorLabel2 = new System.Windows.Forms.Label();
            this.histogram = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.processedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider2)).BeginInit();
            this.SuspendLayout();
            // 
            // processedImage
            // 
            this.processedImage.Location = new System.Drawing.Point(9, 176);
            this.processedImage.Margin = new System.Windows.Forms.Padding(2);
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
            this.saveButton.Location = new System.Drawing.Point(9, 10);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(117, 36);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Visible = false;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // factorSlider
            // 
            this.factorSlider.Location = new System.Drawing.Point(135, 18);
            this.factorSlider.Margin = new System.Windows.Forms.Padding(2);
            this.factorSlider.Maximum = 255;
            this.factorSlider.Minimum = -255;
            this.factorSlider.Name = "factorSlider";
            this.factorSlider.Size = new System.Drawing.Size(800, 45);
            this.factorSlider.TabIndex = 2;
            this.factorSlider.Visible = false;
            this.factorSlider.Scroll += new System.EventHandler(this.FactorSlider_Scroll);
            // 
            // factorLabel
            // 
            this.factorLabel.AutoSize = true;
            this.factorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.factorLabel.Location = new System.Drawing.Point(528, 43);
            this.factorLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.factorLabel.Name = "factorLabel";
            this.factorLabel.Size = new System.Drawing.Size(18, 20);
            this.factorLabel.TabIndex = 3;
            this.factorLabel.Text = "0";
            this.factorLabel.Visible = false;
            // 
            // maskButton
            // 
            this.maskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maskButton.Location = new System.Drawing.Point(130, 11);
            this.maskButton.Margin = new System.Windows.Forms.Padding(2);
            this.maskButton.Name = "maskButton";
            this.maskButton.Size = new System.Drawing.Size(120, 36);
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
            this.maskLabel.Location = new System.Drawing.Point(202, 18);
            this.maskLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.maskLabel.Name = "maskLabel";
            this.maskLabel.Size = new System.Drawing.Size(18, 20);
            this.maskLabel.TabIndex = 5;
            this.maskLabel.Text = "0";
            this.maskLabel.Visible = false;
            this.maskLabel.Click += new System.EventHandler(this.MaskLabel_Click);
            // 
            // SliderName1
            // 
            this.SliderName1.AutoSize = true;
            this.SliderName1.Enabled = false;
            this.SliderName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SliderName1.Location = new System.Drawing.Point(520, 3);
            this.SliderName1.Name = "SliderName1";
            this.SliderName1.Size = new System.Drawing.Size(51, 20);
            this.SliderName1.TabIndex = 6;
            this.SliderName1.Text = "Name";
            this.SliderName1.Visible = false;
            // 
            // SliderName2
            // 
            this.SliderName2.AutoSize = true;
            this.SliderName2.Enabled = false;
            this.SliderName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SliderName2.Location = new System.Drawing.Point(520, 77);
            this.SliderName2.Name = "SliderName2";
            this.SliderName2.Size = new System.Drawing.Size(51, 20);
            this.SliderName2.TabIndex = 7;
            this.SliderName2.Text = "Name";
            this.SliderName2.Visible = false;
            // 
            // factorSlider2
            // 
            this.factorSlider2.Location = new System.Drawing.Point(135, 101);
            this.factorSlider2.Maximum = 255;
            this.factorSlider2.Minimum = -255;
            this.factorSlider2.Name = "factorSlider2";
            this.factorSlider2.Size = new System.Drawing.Size(800, 45);
            this.factorSlider2.TabIndex = 8;
            this.factorSlider2.Value = 255;
            this.factorSlider2.Visible = false;
            this.factorSlider2.Scroll += new System.EventHandler(this.FactorSlider_Scroll);
            // 
            // factorLabel2
            // 
            this.factorLabel2.AutoSize = true;
            this.factorLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.factorLabel2.Location = new System.Drawing.Point(532, 132);
            this.factorLabel2.Name = "factorLabel2";
            this.factorLabel2.Size = new System.Drawing.Size(18, 20);
            this.factorLabel2.TabIndex = 9;
            this.factorLabel2.Text = "0";
            this.factorLabel2.Visible = false;
            // 
            // histogram
            // 
            this.histogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.histogram.Location = new System.Drawing.Point(9, 77);
            this.histogram.Name = "histogram";
            this.histogram.Size = new System.Drawing.Size(117, 55);
            this.histogram.TabIndex = 10;
            this.histogram.Text = "Show histogram";
            this.histogram.UseVisualStyleBackColor = true;
            this.histogram.Visible = false;
            this.histogram.Click += new System.EventHandler(this.histogram_Click);
            // 
            // ProcessedImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(946, 749);
            this.Controls.Add(this.histogram);
            this.Controls.Add(this.factorLabel2);
            this.Controls.Add(this.factorSlider2);
            this.Controls.Add(this.SliderName2);
            this.Controls.Add(this.SliderName1);
            this.Controls.Add(this.maskLabel);
            this.Controls.Add(this.maskButton);
            this.Controls.Add(this.factorLabel);
            this.Controls.Add(this.factorSlider);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.processedImage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ProcessedImageWindow";
            this.Text = "Processed image";
            this.Load += new System.EventHandler(this.ProcessedImageWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.processedImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider2)).EndInit();
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
        private System.Windows.Forms.Label SliderName1;
        private System.Windows.Forms.Label SliderName2;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.TrackBar factorSlider2;
        private System.Windows.Forms.Label factorLabel2;
        private System.Windows.Forms.Button histogram;
    }
}