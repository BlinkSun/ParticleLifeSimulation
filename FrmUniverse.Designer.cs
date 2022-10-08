namespace ParticleLifeSimulation;

partial class FrmUniverse
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.Canvas = new CustomControls.Canvas();
            this.PanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.SplitCanvasSettings = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.SplitCanvasSettings)).BeginInit();
            this.SplitCanvasSettings.Panel1.SuspendLayout();
            this.SplitCanvasSettings.Panel2.SuspendLayout();
            this.SplitCanvasSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(648, 501);
            this.Canvas.TabIndex = 0;
            // 
            // PanelSettings
            // 
            this.PanelSettings.AllowDrop = true;
            this.PanelSettings.AutoSize = true;
            this.PanelSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSettings.Location = new System.Drawing.Point(0, 0);
            this.PanelSettings.Name = "PanelSettings";
            this.PanelSettings.Size = new System.Drawing.Size(152, 0);
            // 
            // SplitCanvasSettings
            // 
            this.SplitCanvasSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitCanvasSettings.Location = new System.Drawing.Point(0, 0);
            this.SplitCanvasSettings.Name = "SplitCanvasSettings";
            // 
            // SplitCanvasSettings.Panel1
            // 
            this.SplitCanvasSettings.Panel1.Controls.Add(this.Canvas);
            // 
            // SplitCanvasSettings.Panel2
            // 
            this.SplitCanvasSettings.Panel2.AutoScroll = true;
            this.SplitCanvasSettings.Panel2.Controls.Add(this.PanelSettings);
            this.SplitCanvasSettings.Size = new System.Drawing.Size(804, 501);
            this.SplitCanvasSettings.SplitterDistance = 648;
            this.SplitCanvasSettings.TabIndex = 0;
            // 
            // FrmUniverse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 501);
            this.Controls.Add(this.SplitCanvasSettings);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmUniverse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Universe";
            this.SplitCanvasSettings.Panel1.ResumeLayout(false);
            this.SplitCanvasSettings.Panel2.ResumeLayout(false);
            this.SplitCanvasSettings.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitCanvasSettings)).EndInit();
            this.SplitCanvasSettings.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private CustomControls.Canvas Canvas;
    private FlowLayoutPanel PanelSettings;
    private SplitContainer SplitCanvasSettings;
}