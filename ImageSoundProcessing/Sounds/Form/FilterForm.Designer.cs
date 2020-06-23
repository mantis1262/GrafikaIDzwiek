namespace Sounds
{
    partial class FilterForm
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
            this.windowType = new System.Windows.Forms.ListBox();
            this.WindowSizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.WindowSize = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.causal = new System.Windows.Forms.Button();
            this.notCausal = new System.Windows.Forms.Button();
            this.R = new System.Windows.Forms.TextBox();
            this.L = new System.Windows.Forms.TextBox();
            this.Fc = new System.Windows.Forms.TextBox();
            this.TimeFilter = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // windowType
            // 
            this.windowType.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.windowType.FormattingEnabled = true;
            this.windowType.ItemHeight = 20;
            this.windowType.Items.AddRange(new object[] {
            "Rectangular",
            "Von Hanna",
            "Hamming"});
            this.windowType.Location = new System.Drawing.Point(24, 12);
            this.windowType.Name = "windowType";
            this.windowType.Size = new System.Drawing.Size(132, 64);
            this.windowType.TabIndex = 2;
            // 
            // WindowSizeLabel
            // 
            this.WindowSizeLabel.AutoSize = true;
            this.WindowSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.WindowSizeLabel.Location = new System.Drawing.Point(24, 86);
            this.WindowSizeLabel.Name = "WindowSizeLabel";
            this.WindowSizeLabel.Size = new System.Drawing.Size(97, 20);
            this.WindowSizeLabel.TabIndex = 3;
            this.WindowSizeLabel.Text = "Window size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(24, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Hop size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(24, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(76, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Filter size";
            // 
            // WindowSize
            // 
            this.WindowSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.WindowSize.Location = new System.Drawing.Point(173, 86);
            this.WindowSize.Name = "WindowSize";
            this.WindowSize.Size = new System.Drawing.Size(100, 26);
            this.WindowSize.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(24, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Cut frequency";
            // 
            // causal
            // 
            this.causal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.causal.Location = new System.Drawing.Point(544, 129);
            this.causal.Name = "causal";
            this.causal.Size = new System.Drawing.Size(162, 82);
            this.causal.TabIndex = 8;
            this.causal.Text = "causal ";
            this.causal.UseVisualStyleBackColor = true;
            this.causal.Click += new System.EventHandler(this.causal_Click);
            // 
            // notCausal
            // 
            this.notCausal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.notCausal.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.notCausal.Location = new System.Drawing.Point(544, 13);
            this.notCausal.Name = "notCausal";
            this.notCausal.Size = new System.Drawing.Size(162, 66);
            this.notCausal.TabIndex = 9;
            this.notCausal.Text = "not causal";
            this.notCausal.UseVisualStyleBackColor = true;
            this.notCausal.Click += new System.EventHandler(this.notCausal_Click);
            // 
            // R
            // 
            this.R.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.R.Location = new System.Drawing.Point(173, 120);
            this.R.Name = "R";
            this.R.Size = new System.Drawing.Size(100, 26);
            this.R.TabIndex = 10;
            // 
            // L
            // 
            this.L.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.L.Location = new System.Drawing.Point(173, 152);
            this.L.Name = "L";
            this.L.Size = new System.Drawing.Size(100, 26);
            this.L.TabIndex = 11;
            // 
            // Fc
            // 
            this.Fc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.Fc.Location = new System.Drawing.Point(173, 185);
            this.Fc.Name = "Fc";
            this.Fc.Size = new System.Drawing.Size(100, 26);
            this.Fc.TabIndex = 12;
            // 
            // TimeFilter
            // 
            this.TimeFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TimeFilter.Location = new System.Drawing.Point(34, 304);
            this.TimeFilter.Name = "TimeFilter";
            this.TimeFilter.Size = new System.Drawing.Size(239, 86);
            this.TimeFilter.TabIndex = 13;
            this.TimeFilter.Text = "TimeFiltration";
            this.TimeFilter.UseVisualStyleBackColor = true;
            this.TimeFilter.Click += new System.EventHandler(this.TimeFilter_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(369, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Frequency domain";
            // 
            // FilterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TimeFilter);
            this.Controls.Add(this.Fc);
            this.Controls.Add(this.L);
            this.Controls.Add(this.R);
            this.Controls.Add(this.notCausal);
            this.Controls.Add(this.causal);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WindowSize);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.WindowSizeLabel);
            this.Controls.Add(this.windowType);
            this.Name = "FilterForm";
            this.Text = "Zad4";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox windowType;
        private System.Windows.Forms.Label WindowSizeLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox WindowSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button causal;
        private System.Windows.Forms.Button notCausal;
        private System.Windows.Forms.TextBox R;
        private System.Windows.Forms.TextBox L;
        private System.Windows.Forms.TextBox Fc;
        private System.Windows.Forms.Button TimeFilter;
        private System.Windows.Forms.Label label4;
    }
}