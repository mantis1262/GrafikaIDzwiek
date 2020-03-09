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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Brightness"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))));
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Contrast"}, -1, System.Drawing.Color.Empty, System.Drawing.Color.Empty, new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238))));
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Negative");
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
            listViewItem5.IndentCount = 1;
            listViewItem6.IndentCount = 2;
            this.effectsList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5,
            listViewItem6});
            this.effectsList.Location = new System.Drawing.Point(12, 12);
            this.effectsList.MultiSelect = false;
            this.effectsList.Name = "effectsList";
            this.effectsList.Size = new System.Drawing.Size(206, 239);
            this.effectsList.TabIndex = 0;
            this.effectsList.UseCompatibleStateImageBehavior = false;
            this.effectsList.View = System.Windows.Forms.View.Details;
            // 
            // effectHeader
            // 
            this.effectHeader.Width = 200;
            // 
            // chooseEffectButton
            // 
            this.chooseEffectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.chooseEffectButton.Location = new System.Drawing.Point(61, 269);
            this.chooseEffectButton.Name = "chooseEffectButton";
            this.chooseEffectButton.Size = new System.Drawing.Size(108, 43);
            this.chooseEffectButton.TabIndex = 1;
            this.chooseEffectButton.Text = "Apply";
            this.chooseEffectButton.UseVisualStyleBackColor = true;
            this.chooseEffectButton.Click += new System.EventHandler(this.ChooseEffectButton_Click);
            // 
            // Effect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 324);
            this.Controls.Add(this.chooseEffectButton);
            this.Controls.Add(this.effectsList);
            this.Name = "Effect";
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