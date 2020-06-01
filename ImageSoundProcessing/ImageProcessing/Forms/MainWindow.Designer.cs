namespace ImageSoundProcessing
{
    partial class MainWindow
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this._originalImage = new System.Windows.Forms.PictureBox();
            this._loadImage = new System.Windows.Forms.Button();
            this._effectButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._originalImage)).BeginInit();
            this.SuspendLayout();
            // 
            // _originalImage
            // 
            this._originalImage.Location = new System.Drawing.Point(0, 68);
            this._originalImage.Name = "_originalImage";
            this._originalImage.Size = new System.Drawing.Size(84, 72);
            this._originalImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this._originalImage.TabIndex = 0;
            this._originalImage.TabStop = false;
            this._originalImage.Click += new System.EventHandler(this.OriginalImage_Click);
            // 
            // loadImage
            // 
            this._loadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._loadImage.Location = new System.Drawing.Point(12, 12);
            this._loadImage.Name = "loadImage";
            this._loadImage.Size = new System.Drawing.Size(157, 40);
            this._loadImage.TabIndex = 1;
            this._loadImage.Text = "Load image";
            this._loadImage.UseVisualStyleBackColor = true;
            this._loadImage.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // effectButton
            // 
            this._effectButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this._effectButton.Location = new System.Drawing.Point(175, 12);
            this._effectButton.Name = "effectButton";
            this._effectButton.Size = new System.Drawing.Size(157, 40);
            this._effectButton.TabIndex = 2;
            this._effectButton.Text = "Choose effect";
            this._effectButton.UseVisualStyleBackColor = true;
            this._effectButton.Click += new System.EventHandler(this.EffectButton_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1262, 977);
            this.Controls.Add(this._effectButton);
            this.Controls.Add(this._loadImage);
            this.Controls.Add(this._originalImage);
            this.Name = "MainWindow";
            this.Text = "Przetwarzanie obrazu";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this._originalImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox _originalImage;
        private System.Windows.Forms.Button _loadImage;
        private System.Windows.Forms.Button _effectButton;
    }
}

