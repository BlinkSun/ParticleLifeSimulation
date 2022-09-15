using System.ComponentModel;

namespace ParticleLifeSimulation
{
    partial class Accordion
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
            this.ChkTitleBar = new System.Windows.Forms.CheckBox();
            this.PanelControles = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ChkTitleBar
            // 
            this.ChkTitleBar.Appearance = System.Windows.Forms.Appearance.Button;
            this.ChkTitleBar.AutoSize = true;
            this.ChkTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ChkTitleBar.Location = new System.Drawing.Point(0, 0);
            this.ChkTitleBar.Name = "ChkTitleBar";
            this.ChkTitleBar.Size = new System.Drawing.Size(207, 25);
            this.ChkTitleBar.TabIndex = 0;
            this.ChkTitleBar.Text = "AccTitle";
            this.ChkTitleBar.UseVisualStyleBackColor = true;
            this.ChkTitleBar.CheckedChanged += new System.EventHandler(this.ChkTitleBar_CheckedChanged);
            // 
            // PanelControles
            // 
            this.PanelControles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelControles.Location = new System.Drawing.Point(0, 25);
            this.PanelControles.Name = "PanelControles";
            this.PanelControles.Size = new System.Drawing.Size(207, 291);
            this.PanelControles.TabIndex = 1;
            // 
            // Accordion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelControles);
            this.Controls.Add(this.ChkTitleBar);
            this.Name = "Accordion";
            this.Size = new System.Drawing.Size(207, 316);
            this.Load += new System.EventHandler(this.Accordion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox ChkTitleBar;
        private Panel PanelControles;
    }
}
