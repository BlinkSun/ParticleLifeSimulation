namespace ParticleLifeSimulation
{
    partial class Slider
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.LblSlider = new System.Windows.Forms.Label();
            this.LblCurrentValue = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LblSlider
            // 
            this.LblSlider.BackColor = System.Drawing.Color.Transparent;
            this.LblSlider.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblSlider.Location = new System.Drawing.Point(0, 0);
            this.LblSlider.Name = "LblSlider";
            this.LblSlider.Size = new System.Drawing.Size(231, 16);
            this.LblSlider.TabIndex = 0;
            this.LblSlider.Text = "LblSlider";
            this.LblSlider.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseMove);
            this.LblSlider.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseUp);
            // 
            // LblCurrentValue
            // 
            this.LblCurrentValue.AutoSize = true;
            this.LblCurrentValue.BackColor = System.Drawing.Color.Transparent;
            this.LblCurrentValue.Dock = System.Windows.Forms.DockStyle.Right;
            this.LblCurrentValue.Location = new System.Drawing.Point(231, 0);
            this.LblCurrentValue.Name = "LblCurrentValue";
            this.LblCurrentValue.Size = new System.Drawing.Size(38, 15);
            this.LblCurrentValue.TabIndex = 1;
            this.LblCurrentValue.Text = "LblCurrentValue";
            this.LblCurrentValue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseMove);
            this.LblCurrentValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Slider_MouseUp);
            // 
            // Slider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.LblSlider);
            this.Controls.Add(this.LblCurrentValue);
            this.Name = "Slider";
            this.Size = new System.Drawing.Size(269, 16);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label LblSlider;
        private Label LblCurrentValue;
    }
}
