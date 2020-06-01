namespace ImageSoundProcessing.Forms
{
    partial class EffectWindow
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Brightness"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))));
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "Contrast"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))));
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Negative");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("GrayMode");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("AritmeticMiddleFilter");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("MedianFilter");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Histogram");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("ModifyHistogram");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("SouthFilter");
            System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem("SouthWestFilter");
            System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem("WestFilter");
            System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem("NorthWestFilter");
            System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem("UolisaFilter");
            System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem("SouthStatic");
            System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem("FFT");
            this.effectsList = new System.Windows.Forms.ListView();
            this.effectHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chooseEffectButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // effectsList
            // 
            this.effectsList.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.effectsList.AutoArrange = false;
            this.effectsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.effectHeader});
            this.effectsList.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.effectsList.FullRowSelect = true;
            this.effectsList.GridLines = true;
            this.effectsList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.effectsList.HideSelection = false;
            listViewItem2.IndentCount = 1;
            listViewItem3.IndentCount = 2;
            this.effectsList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9,
            listViewItem10,
            listViewItem11,
            listViewItem12,
            listViewItem13,
            listViewItem14,
            listViewItem15});
            this.effectsList.Location = new System.Drawing.Point(12, 12);
            this.effectsList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.effectsList.MultiSelect = false;
            this.effectsList.Name = "effectsList";
            this.effectsList.Size = new System.Drawing.Size(247, 386);
            this.effectsList.TabIndex = 0;
            this.effectsList.UseCompatibleStateImageBehavior = false;
            this.effectsList.View = System.Windows.Forms.View.Details;
            this.effectsList.SelectedIndexChanged += new System.EventHandler(this.effectsList_SelectedIndexChanged);
            // 
            // effectHeader
            // 
            this.effectHeader.Width = 200;
            // 
            // chooseEffectButton
            // 
            this.chooseEffectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chooseEffectButton.Location = new System.Drawing.Point(88, 404);
            this.chooseEffectButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chooseEffectButton.Name = "chooseEffectButton";
            this.chooseEffectButton.Size = new System.Drawing.Size(108, 43);
            this.chooseEffectButton.TabIndex = 1;
            this.chooseEffectButton.Text = "Apply";
            this.chooseEffectButton.UseVisualStyleBackColor = true;
            this.chooseEffectButton.Click += new System.EventHandler(this.ChooseEffectButton_Click);
            // 
            // EffectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(272, 460);
            this.Controls.Add(this.chooseEffectButton);
            this.Controls.Add(this.effectsList);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "EffectWindow";
            this.Text = "Effect";
            this.Load += new System.EventHandler(this.Effect_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView effectsList;
        private System.Windows.Forms.ColumnHeader effectHeader;
        private System.Windows.Forms.Button chooseEffectButton;
    }
}