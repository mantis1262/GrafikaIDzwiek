namespace Sound
{
    partial class CharWindow
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.Histogram = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.autocorrelationButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chunkSizeBox = new System.Windows.Forms.TextBox();
            this.applyChunkButton = new System.Windows.Forms.Button();
            this.cepstrumButton = new System.Windows.Forms.Button();
            this.pathLabel = new System.Windows.Forms.Label();
            this.actionStateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Histogram)).BeginInit();
            this.SuspendLayout();
            // 
            // Histogram
            // 
            chartArea5.AxisX.IsLabelAutoFit = false;
            chartArea5.AxisX.ScaleBreakStyle.Enabled = true;
            chartArea5.CursorX.AutoScroll = false;
            chartArea5.CursorX.IsUserEnabled = true;
            chartArea5.Name = "ChartArea1";
            this.Histogram.ChartAreas.Add(chartArea5);
            this.Histogram.Location = new System.Drawing.Point(16, 63);
            this.Histogram.Margin = new System.Windows.Forms.Padding(4);
            this.Histogram.Name = "Histogram";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series5.Name = "Value";
            this.Histogram.Series.Add(series5);
            this.Histogram.Size = new System.Drawing.Size(1547, 697);
            this.Histogram.TabIndex = 0;
            this.Histogram.Text = "Histogram";
            // 
            // loadFileButton
            // 
            this.loadFileButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.loadFileButton.Location = new System.Drawing.Point(32, 12);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(144, 40);
            this.loadFileButton.TabIndex = 2;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // autocorrelationButton
            // 
            this.autocorrelationButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.autocorrelationButton.Location = new System.Drawing.Point(980, 12);
            this.autocorrelationButton.Name = "autocorrelationButton";
            this.autocorrelationButton.Size = new System.Drawing.Size(199, 40);
            this.autocorrelationButton.TabIndex = 3;
            this.autocorrelationButton.Text = "Autocorrelation";
            this.autocorrelationButton.UseVisualStyleBackColor = true;
            this.autocorrelationButton.Click += new System.EventHandler(this.autocorrelationButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(446, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 5;
            this.label1.Text = "Chunk size";
            // 
            // chunkSizeBox
            // 
            this.chunkSizeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chunkSizeBox.Location = new System.Drawing.Point(562, 20);
            this.chunkSizeBox.Name = "chunkSizeBox";
            this.chunkSizeBox.Size = new System.Drawing.Size(121, 30);
            this.chunkSizeBox.TabIndex = 6;
            this.chunkSizeBox.Text = "44100";
            // 
            // applyChunkButton
            // 
            this.applyChunkButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.applyChunkButton.Location = new System.Drawing.Point(689, 17);
            this.applyChunkButton.Name = "applyChunkButton";
            this.applyChunkButton.Size = new System.Drawing.Size(75, 35);
            this.applyChunkButton.TabIndex = 8;
            this.applyChunkButton.Text = "Apply";
            this.applyChunkButton.UseVisualStyleBackColor = true;
            this.applyChunkButton.Click += new System.EventHandler(this.applyChunkButton_Click);
            // 
            // cepstrumButton
            // 
            this.cepstrumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.cepstrumButton.Location = new System.Drawing.Point(1219, 12);
            this.cepstrumButton.Name = "cepstrumButton";
            this.cepstrumButton.Size = new System.Drawing.Size(202, 40);
            this.cepstrumButton.TabIndex = 9;
            this.cepstrumButton.Text = "Cepstrum";
            this.cepstrumButton.UseVisualStyleBackColor = true;
            this.cepstrumButton.Click += new System.EventHandler(this.cepstrumButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.AutoSize = true;
            this.pathLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.pathLabel.Location = new System.Drawing.Point(191, 20);
            this.pathLabel.MaximumSize = new System.Drawing.Size(250, 0);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(53, 25);
            this.pathLabel.TabIndex = 12;
            this.pathLabel.Text = "FILE";
            // 
            // actionStateLabel
            // 
            this.actionStateLabel.AutoSize = true;
            this.actionStateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.actionStateLabel.Location = new System.Drawing.Point(792, 20);
            this.actionStateLabel.Name = "actionStateLabel";
            this.actionStateLabel.Size = new System.Drawing.Size(79, 25);
            this.actionStateLabel.TabIndex = 13;
            this.actionStateLabel.Text = "STATE";
            this.actionStateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CharWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1579, 922);
            this.Controls.Add(this.actionStateLabel);
            this.Controls.Add(this.pathLabel);
            this.Controls.Add(this.cepstrumButton);
            this.Controls.Add(this.applyChunkButton);
            this.Controls.Add(this.chunkSizeBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.autocorrelationButton);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.Histogram);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CharWindow";
            this.Text = "CharWindow";
            ((System.ComponentModel.ISupportInitialize)(this.Histogram)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataVisualization.Charting.Chart Histogram;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Button autocorrelationButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox chunkSizeBox;
        private System.Windows.Forms.Button applyChunkButton;
        private System.Windows.Forms.Button cepstrumButton;
        private System.Windows.Forms.Label pathLabel;
        private System.Windows.Forms.Label actionStateLabel;
    }
}