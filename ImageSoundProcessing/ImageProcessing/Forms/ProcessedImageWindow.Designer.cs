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
            this.powerSpectrumButton = new System.Windows.Forms.Button();
            this.phaseSpectrumButton = new System.Windows.Forms.Button();
            this.lowPassFilterButton = new System.Windows.Forms.Button();
            this.fourierFilterLabel = new System.Windows.Forms.Label();
            this.rangeTextBox = new System.Windows.Forms.TextBox();
            this.highPassFilterButton = new System.Windows.Forms.Button();
            this.fourierFilterLabel2 = new System.Windows.Forms.Label();
            this.rangeTextBox2 = new System.Windows.Forms.TextBox();
            this.bandPassFilterButton = new System.Windows.Forms.Button();
            this.bandCutFilterButton = new System.Windows.Forms.Button();
            this.highPassEdgeDetectionFilterButton = new System.Windows.Forms.Button();
            this.phaseSpectrumFilterButton = new System.Windows.Forms.Button();
            this.kComponentLabel = new System.Windows.Forms.Label();
            this.lComponentLabel = new System.Windows.Forms.Label();
            this.kComponentTextBox = new System.Windows.Forms.TextBox();
            this.lComponentTextBox = new System.Windows.Forms.TextBox();
            this.regionSplittingAndMergingSegmentation = new System.Windows.Forms.Button();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.minimumPixelsForRegionLabel = new System.Windows.Forms.Label();
            this.thresholdTextBox = new System.Windows.Forms.TextBox();
            this.minPixelsTextBox = new System.Windows.Forms.TextBox();
            this.MaskUse = new System.Windows.Forms.Button();
            this.sectorWidthLabel = new System.Windows.Forms.Label();
            this.sectorWidthTextBox = new System.Windows.Forms.TextBox();
            this.rotationLabel = new System.Windows.Forms.Label();
            this.rotationTextBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.processedImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.factorSlider2)).BeginInit();
            this.SuspendLayout();
            // 
            // processedImage
            // 
            this.processedImage.Location = new System.Drawing.Point(12, 203);
            this.processedImage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(156, 44);
            this.saveButton.TabIndex = 1;
            this.saveButton.Text = "Save image";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // factorSlider
            // 
            this.factorSlider.Location = new System.Drawing.Point(180, 22);
            this.factorSlider.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.factorSlider.Maximum = 255;
            this.factorSlider.Minimum = -255;
            this.factorSlider.Name = "factorSlider";
            this.factorSlider.Size = new System.Drawing.Size(1067, 56);
            this.factorSlider.TabIndex = 2;
            this.factorSlider.Visible = false;
            this.factorSlider.Scroll += new System.EventHandler(this.FactorSlider_Scroll);
            // 
            // factorLabel
            // 
            this.factorLabel.AutoSize = true;
            this.factorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.factorLabel.Location = new System.Drawing.Point(704, 53);
            this.factorLabel.Name = "factorLabel";
            this.factorLabel.Size = new System.Drawing.Size(23, 25);
            this.factorLabel.TabIndex = 3;
            this.factorLabel.Text = "0";
            this.factorLabel.Visible = false;
            // 
            // maskButton
            // 
            this.maskButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.maskButton.Location = new System.Drawing.Point(173, 14);
            this.maskButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            // SliderName1
            // 
            this.SliderName1.AutoSize = true;
            this.SliderName1.Enabled = false;
            this.SliderName1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SliderName1.Location = new System.Drawing.Point(693, 4);
            this.SliderName1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SliderName1.Name = "SliderName1";
            this.SliderName1.Size = new System.Drawing.Size(64, 25);
            this.SliderName1.TabIndex = 6;
            this.SliderName1.Text = "Name";
            this.SliderName1.Visible = false;
            // 
            // SliderName2
            // 
            this.SliderName2.AutoSize = true;
            this.SliderName2.Enabled = false;
            this.SliderName2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SliderName2.Location = new System.Drawing.Point(693, 95);
            this.SliderName2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SliderName2.Name = "SliderName2";
            this.SliderName2.Size = new System.Drawing.Size(64, 25);
            this.SliderName2.TabIndex = 7;
            this.SliderName2.Text = "Name";
            this.SliderName2.Visible = false;
            // 
            // factorSlider2
            // 
            this.factorSlider2.Location = new System.Drawing.Point(180, 124);
            this.factorSlider2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.factorSlider2.Maximum = 255;
            this.factorSlider2.Minimum = -255;
            this.factorSlider2.Name = "factorSlider2";
            this.factorSlider2.Size = new System.Drawing.Size(1067, 56);
            this.factorSlider2.TabIndex = 8;
            this.factorSlider2.Value = 255;
            this.factorSlider2.Visible = false;
            this.factorSlider2.Scroll += new System.EventHandler(this.FactorSlider_Scroll);
            // 
            // factorLabel2
            // 
            this.factorLabel2.AutoSize = true;
            this.factorLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.factorLabel2.Location = new System.Drawing.Point(709, 162);
            this.factorLabel2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.factorLabel2.Name = "factorLabel2";
            this.factorLabel2.Size = new System.Drawing.Size(23, 25);
            this.factorLabel2.TabIndex = 9;
            this.factorLabel2.Text = "0";
            this.factorLabel2.Visible = false;
            // 
            // histogram
            // 
            this.histogram.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.histogram.Location = new System.Drawing.Point(12, 95);
            this.histogram.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.histogram.Name = "histogram";
            this.histogram.Size = new System.Drawing.Size(156, 68);
            this.histogram.TabIndex = 10;
            this.histogram.Text = "Show histogram";
            this.histogram.UseVisualStyleBackColor = true;
            this.histogram.Visible = false;
            this.histogram.Click += new System.EventHandler(this.histogram_Click);
            // 
            // powerSpectrumButton
            // 
            this.powerSpectrumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.powerSpectrumButton.Location = new System.Drawing.Point(339, 10);
            this.powerSpectrumButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.powerSpectrumButton.Name = "powerSpectrumButton";
            this.powerSpectrumButton.Size = new System.Drawing.Size(171, 44);
            this.powerSpectrumButton.TabIndex = 11;
            this.powerSpectrumButton.Text = "Magnitude";
            this.powerSpectrumButton.UseVisualStyleBackColor = true;
            this.powerSpectrumButton.Visible = false;
            this.powerSpectrumButton.Click += new System.EventHandler(this.PowerSpectrumButton_Click);
            // 
            // phaseSpectrumButton
            // 
            this.phaseSpectrumButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.phaseSpectrumButton.Location = new System.Drawing.Point(515, 10);
            this.phaseSpectrumButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.phaseSpectrumButton.Name = "phaseSpectrumButton";
            this.phaseSpectrumButton.Size = new System.Drawing.Size(169, 44);
            this.phaseSpectrumButton.TabIndex = 12;
            this.phaseSpectrumButton.Text = "Phase";
            this.phaseSpectrumButton.UseVisualStyleBackColor = true;
            this.phaseSpectrumButton.Visible = false;
            this.phaseSpectrumButton.Click += new System.EventHandler(this.PhaseSpectrumButton_Click);
            // 
            // lowPassFilterButton
            // 
            this.lowPassFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lowPassFilterButton.Location = new System.Drawing.Point(1036, 203);
            this.lowPassFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lowPassFilterButton.Name = "lowPassFilterButton";
            this.lowPassFilterButton.Size = new System.Drawing.Size(171, 42);
            this.lowPassFilterButton.TabIndex = 13;
            this.lowPassFilterButton.Text = "Low pass filter";
            this.lowPassFilterButton.UseVisualStyleBackColor = true;
            this.lowPassFilterButton.Visible = false;
            this.lowPassFilterButton.Click += new System.EventHandler(this.LowPassFilterButton_Click);
            // 
            // fourierFilterLabel
            // 
            this.fourierFilterLabel.AutoSize = true;
            this.fourierFilterLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fourierFilterLabel.Location = new System.Drawing.Point(797, 266);
            this.fourierFilterLabel.Name = "fourierFilterLabel";
            this.fourierFilterLabel.Size = new System.Drawing.Size(119, 25);
            this.fourierFilterLabel.TabIndex = 14;
            this.fourierFilterLabel.Text = "Range (min)";
            this.fourierFilterLabel.Visible = false;
            this.fourierFilterLabel.Click += new System.EventHandler(this.FourierFilterLabel_Click);
            // 
            // rangeTextBox
            // 
            this.rangeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rangeTextBox.Location = new System.Drawing.Point(941, 263);
            this.rangeTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rangeTextBox.Name = "rangeTextBox";
            this.rangeTextBox.Size = new System.Drawing.Size(64, 30);
            this.rangeTextBox.TabIndex = 15;
            this.rangeTextBox.Visible = false;
            this.rangeTextBox.TextChanged += new System.EventHandler(this.RangeTextBox_TextChanged);
            // 
            // highPassFilterButton
            // 
            this.highPassFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.highPassFilterButton.Location = new System.Drawing.Point(1036, 251);
            this.highPassFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.highPassFilterButton.Name = "highPassFilterButton";
            this.highPassFilterButton.Size = new System.Drawing.Size(171, 42);
            this.highPassFilterButton.TabIndex = 16;
            this.highPassFilterButton.Text = "High pass filter";
            this.highPassFilterButton.UseVisualStyleBackColor = true;
            this.highPassFilterButton.Visible = false;
            this.highPassFilterButton.Click += new System.EventHandler(this.HighPassFilterButton_Click);
            // 
            // fourierFilterLabel2
            // 
            this.fourierFilterLabel2.AutoSize = true;
            this.fourierFilterLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.fourierFilterLabel2.Location = new System.Drawing.Point(797, 304);
            this.fourierFilterLabel2.Name = "fourierFilterLabel2";
            this.fourierFilterLabel2.Size = new System.Drawing.Size(125, 25);
            this.fourierFilterLabel2.TabIndex = 17;
            this.fourierFilterLabel2.Text = "Range (max)";
            this.fourierFilterLabel2.Visible = false;
            this.fourierFilterLabel2.Click += new System.EventHandler(this.FourierFilterLabel2_Click);
            // 
            // rangeTextBox2
            // 
            this.rangeTextBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rangeTextBox2.Location = new System.Drawing.Point(941, 306);
            this.rangeTextBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rangeTextBox2.Name = "rangeTextBox2";
            this.rangeTextBox2.Size = new System.Drawing.Size(64, 30);
            this.rangeTextBox2.TabIndex = 18;
            this.rangeTextBox2.Visible = false;
            this.rangeTextBox2.TextChanged += new System.EventHandler(this.RangeTextBox2_TextChanged);
            // 
            // bandPassFilterButton
            // 
            this.bandPassFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bandPassFilterButton.Location = new System.Drawing.Point(1036, 300);
            this.bandPassFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bandPassFilterButton.Name = "bandPassFilterButton";
            this.bandPassFilterButton.Size = new System.Drawing.Size(171, 42);
            this.bandPassFilterButton.TabIndex = 19;
            this.bandPassFilterButton.Text = "Band pass filter";
            this.bandPassFilterButton.UseVisualStyleBackColor = true;
            this.bandPassFilterButton.Visible = false;
            this.bandPassFilterButton.Click += new System.EventHandler(this.BandPassFilterButton_Click);
            // 
            // bandCutFilterButton
            // 
            this.bandCutFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.bandCutFilterButton.Location = new System.Drawing.Point(1036, 350);
            this.bandCutFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bandCutFilterButton.Name = "bandCutFilterButton";
            this.bandCutFilterButton.Size = new System.Drawing.Size(171, 42);
            this.bandCutFilterButton.TabIndex = 20;
            this.bandCutFilterButton.Text = "Band cut filter";
            this.bandCutFilterButton.UseVisualStyleBackColor = true;
            this.bandCutFilterButton.Visible = false;
            this.bandCutFilterButton.Click += new System.EventHandler(this.BandCutFilterButton_Click);
            // 
            // highPassEdgeDetectionFilterButton
            // 
            this.highPassEdgeDetectionFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.highPassEdgeDetectionFilterButton.Location = new System.Drawing.Point(1036, 398);
            this.highPassEdgeDetectionFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.highPassEdgeDetectionFilterButton.Name = "highPassEdgeDetectionFilterButton";
            this.highPassEdgeDetectionFilterButton.Size = new System.Drawing.Size(171, 42);
            this.highPassEdgeDetectionFilterButton.TabIndex = 21;
            this.highPassEdgeDetectionFilterButton.Text = "Edge detection";
            this.highPassEdgeDetectionFilterButton.UseVisualStyleBackColor = true;
            this.highPassEdgeDetectionFilterButton.Visible = false;
            this.highPassEdgeDetectionFilterButton.Click += new System.EventHandler(this.HighPassEdgeDetectionFilterButton_Click);
            // 
            // phaseSpectrumFilterButton
            // 
            this.phaseSpectrumFilterButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.phaseSpectrumFilterButton.Location = new System.Drawing.Point(1036, 447);
            this.phaseSpectrumFilterButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.phaseSpectrumFilterButton.Name = "phaseSpectrumFilterButton";
            this.phaseSpectrumFilterButton.Size = new System.Drawing.Size(171, 64);
            this.phaseSpectrumFilterButton.TabIndex = 22;
            this.phaseSpectrumFilterButton.Text = "Phase spectrum filter";
            this.phaseSpectrumFilterButton.UseVisualStyleBackColor = true;
            this.phaseSpectrumFilterButton.Visible = false;
            this.phaseSpectrumFilterButton.Click += new System.EventHandler(this.PhaseSpectrumFilterButton_Click);
            // 
            // kComponentLabel
            // 
            this.kComponentLabel.AutoSize = true;
            this.kComponentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kComponentLabel.Location = new System.Drawing.Point(895, 447);
            this.kComponentLabel.Name = "kComponentLabel";
            this.kComponentLabel.Size = new System.Drawing.Size(22, 25);
            this.kComponentLabel.TabIndex = 23;
            this.kComponentLabel.Text = "k";
            this.kComponentLabel.Visible = false;
            this.kComponentLabel.Click += new System.EventHandler(this.KComponentLabel_Click);
            // 
            // lComponentLabel
            // 
            this.lComponentLabel.AutoSize = true;
            this.lComponentLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lComponentLabel.Location = new System.Drawing.Point(895, 486);
            this.lComponentLabel.Name = "lComponentLabel";
            this.lComponentLabel.Size = new System.Drawing.Size(16, 25);
            this.lComponentLabel.TabIndex = 24;
            this.lComponentLabel.Text = "l";
            this.lComponentLabel.Visible = false;
            this.lComponentLabel.Click += new System.EventHandler(this.LComponentLabel_Click);
            // 
            // kComponentTextBox
            // 
            this.kComponentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.kComponentTextBox.Location = new System.Drawing.Point(941, 447);
            this.kComponentTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.kComponentTextBox.Name = "kComponentTextBox";
            this.kComponentTextBox.Size = new System.Drawing.Size(64, 30);
            this.kComponentTextBox.TabIndex = 25;
            this.kComponentTextBox.Visible = false;
            this.kComponentTextBox.TextChanged += new System.EventHandler(this.KComponentTextBox_TextChanged);
            // 
            // lComponentTextBox
            // 
            this.lComponentTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lComponentTextBox.Location = new System.Drawing.Point(941, 484);
            this.lComponentTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lComponentTextBox.Name = "lComponentTextBox";
            this.lComponentTextBox.Size = new System.Drawing.Size(64, 30);
            this.lComponentTextBox.TabIndex = 26;
            this.lComponentTextBox.Visible = false;
            this.lComponentTextBox.TextChanged += new System.EventHandler(this.LComponentTextBox_TextChanged);
            // 
            // regionSplittingAndMergingSegmentation
            // 
            this.regionSplittingAndMergingSegmentation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.regionSplittingAndMergingSegmentation.Location = new System.Drawing.Point(1036, 518);
            this.regionSplittingAndMergingSegmentation.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.regionSplittingAndMergingSegmentation.Name = "regionSplittingAndMergingSegmentation";
            this.regionSplittingAndMergingSegmentation.Size = new System.Drawing.Size(171, 64);
            this.regionSplittingAndMergingSegmentation.TabIndex = 27;
            this.regionSplittingAndMergingSegmentation.Text = "Region splitting and merging";
            this.regionSplittingAndMergingSegmentation.UseVisualStyleBackColor = true;
            this.regionSplittingAndMergingSegmentation.Visible = false;
            this.regionSplittingAndMergingSegmentation.Click += new System.EventHandler(this.RegionSplittingAndMergingSegmentation_Click);
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thresholdLabel.Location = new System.Drawing.Point(823, 532);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(100, 25);
            this.thresholdLabel.TabIndex = 28;
            this.thresholdLabel.Text = "Threshold";
            this.thresholdLabel.Visible = false;
            this.thresholdLabel.Click += new System.EventHandler(this.ThresholdLabel_Click);
            // 
            // minimumPixelsForRegionLabel
            // 
            this.minimumPixelsForRegionLabel.AutoSize = true;
            this.minimumPixelsForRegionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minimumPixelsForRegionLabel.Location = new System.Drawing.Point(819, 572);
            this.minimumPixelsForRegionLabel.Name = "minimumPixelsForRegionLabel";
            this.minimumPixelsForRegionLabel.Size = new System.Drawing.Size(104, 25);
            this.minimumPixelsForRegionLabel.TabIndex = 29;
            this.minimumPixelsForRegionLabel.Text = "Min. pixels";
            this.minimumPixelsForRegionLabel.Visible = false;
            this.minimumPixelsForRegionLabel.Click += new System.EventHandler(this.MinimumPixelsForRegionLabel_Click);
            // 
            // thresholdTextBox
            // 
            this.thresholdTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.thresholdTextBox.Location = new System.Drawing.Point(941, 530);
            this.thresholdTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.thresholdTextBox.Name = "thresholdTextBox";
            this.thresholdTextBox.Size = new System.Drawing.Size(64, 30);
            this.thresholdTextBox.TabIndex = 30;
            this.thresholdTextBox.Visible = false;
            this.thresholdTextBox.TextChanged += new System.EventHandler(this.ThresholdTextBox_TextChanged);
            // 
            // minPixelsTextBox
            // 
            this.minPixelsTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minPixelsTextBox.Location = new System.Drawing.Point(941, 572);
            this.minPixelsTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.minPixelsTextBox.Name = "minPixelsTextBox";
            this.minPixelsTextBox.Size = new System.Drawing.Size(64, 30);
            this.minPixelsTextBox.TabIndex = 31;
            this.minPixelsTextBox.Visible = false;
            this.minPixelsTextBox.TextChanged += new System.EventHandler(this.MinPixelsTextBox_TextChanged);
            // 
            // MaskUse
            // 
            this.MaskUse.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.MaskUse.Location = new System.Drawing.Point(1036, 590);
            this.MaskUse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaskUse.Name = "MaskUse";
            this.MaskUse.Size = new System.Drawing.Size(171, 66);
            this.MaskUse.TabIndex = 32;
            this.MaskUse.Text = "Segment mask use";
            this.MaskUse.UseVisualStyleBackColor = true;
            this.MaskUse.Visible = false;
            this.MaskUse.Click += new System.EventHandler(this.MaskUse_Click);
            // 
            // sectorWidthLabel
            // 
            this.sectorWidthLabel.AutoSize = true;
            this.sectorWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sectorWidthLabel.Location = new System.Drawing.Point(794, 350);
            this.sectorWidthLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.sectorWidthLabel.Name = "sectorWidthLabel";
            this.sectorWidthLabel.Size = new System.Drawing.Size(119, 25);
            this.sectorWidthLabel.TabIndex = 32;
            this.sectorWidthLabel.Text = "Sector width";
            this.sectorWidthLabel.Visible = false;
            // 
            // sectorWidthTextBox
            // 
            this.sectorWidthTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sectorWidthTextBox.Location = new System.Drawing.Point(941, 350);
            this.sectorWidthTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sectorWidthTextBox.Name = "sectorWidthTextBox";
            this.sectorWidthTextBox.Size = new System.Drawing.Size(64, 30);
            this.sectorWidthTextBox.TabIndex = 33;
            this.sectorWidthTextBox.Visible = false;
            // 
            // rotationLabel
            // 
            this.rotationLabel.AutoSize = true;
            this.rotationLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rotationLabel.Location = new System.Drawing.Point(787, 401);
            this.rotationLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.rotationLabel.Name = "rotationLabel";
            this.rotationLabel.Size = new System.Drawing.Size(136, 25);
            this.rotationLabel.TabIndex = 34;
            this.rotationLabel.Text = "Rotation angle";
            this.rotationLabel.Visible = false;
            // 
            // rotationTextBox
            // 
            this.rotationTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.rotationTextBox.Location = new System.Drawing.Point(941, 398);
            this.rotationTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.rotationTextBox.Name = "rotationTextBox";
            this.rotationTextBox.Size = new System.Drawing.Size(64, 30);
            this.rotationTextBox.TabIndex = 35;
            this.rotationTextBox.Visible = false;
            // 
            // ProcessedImageWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1683, 1050);
            this.Controls.Add(this.rotationTextBox);
            this.Controls.Add(this.rotationLabel);
            this.Controls.Add(this.sectorWidthTextBox);
            this.Controls.Add(this.sectorWidthLabel);
            this.Controls.Add(this.MaskUse);
            this.Controls.Add(this.minPixelsTextBox);
            this.Controls.Add(this.thresholdTextBox);
            this.Controls.Add(this.minimumPixelsForRegionLabel);
            this.Controls.Add(this.thresholdLabel);
            this.Controls.Add(this.regionSplittingAndMergingSegmentation);
            this.Controls.Add(this.lComponentTextBox);
            this.Controls.Add(this.kComponentTextBox);
            this.Controls.Add(this.lComponentLabel);
            this.Controls.Add(this.kComponentLabel);
            this.Controls.Add(this.phaseSpectrumFilterButton);
            this.Controls.Add(this.highPassEdgeDetectionFilterButton);
            this.Controls.Add(this.bandCutFilterButton);
            this.Controls.Add(this.bandPassFilterButton);
            this.Controls.Add(this.rangeTextBox2);
            this.Controls.Add(this.fourierFilterLabel2);
            this.Controls.Add(this.highPassFilterButton);
            this.Controls.Add(this.rangeTextBox);
            this.Controls.Add(this.fourierFilterLabel);
            this.Controls.Add(this.lowPassFilterButton);
            this.Controls.Add(this.phaseSpectrumButton);
            this.Controls.Add(this.powerSpectrumButton);
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
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
        private System.Windows.Forms.Button powerSpectrumButton;
        private System.Windows.Forms.Button phaseSpectrumButton;
        private System.Windows.Forms.Button lowPassFilterButton;
        private System.Windows.Forms.Label fourierFilterLabel;
        private System.Windows.Forms.TextBox rangeTextBox;
        private System.Windows.Forms.Button highPassFilterButton;
        private System.Windows.Forms.Label fourierFilterLabel2;
        private System.Windows.Forms.TextBox rangeTextBox2;
        private System.Windows.Forms.Button bandPassFilterButton;
        private System.Windows.Forms.Button bandCutFilterButton;
        private System.Windows.Forms.Button highPassEdgeDetectionFilterButton;
        private System.Windows.Forms.Button phaseSpectrumFilterButton;
        private System.Windows.Forms.Label kComponentLabel;
        private System.Windows.Forms.Label lComponentLabel;
        private System.Windows.Forms.TextBox kComponentTextBox;
        private System.Windows.Forms.TextBox lComponentTextBox;
        private System.Windows.Forms.Button regionSplittingAndMergingSegmentation;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.Label minimumPixelsForRegionLabel;
        private System.Windows.Forms.TextBox thresholdTextBox;
        private System.Windows.Forms.TextBox minPixelsTextBox;
        private System.Windows.Forms.Button MaskUse;
        private System.Windows.Forms.Label sectorWidthLabel;
        private System.Windows.Forms.TextBox sectorWidthTextBox;
        private System.Windows.Forms.Label rotationLabel;
        private System.Windows.Forms.TextBox rotationTextBox;
    }
}