namespace ParticleLifeSimulation;

partial class FrmMain
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
            this.Canvas = new ParticleLifeSimulation.Canvas();
            this.PanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // Canvas
            // 
            this.Canvas.AutoSize = true;
            this.Canvas.BackColor = System.Drawing.Color.Black;
            this.Canvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(4);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(500, 500);
            this.Canvas.TabIndex = 0;
            // 
            // PanelSettings
            // 
            this.PanelSettings.AutoScroll = true;
            this.PanelSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.PanelSettings.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PanelSettings.Location = new System.Drawing.Point(499, 0);
            this.PanelSettings.Margin = new System.Windows.Forms.Padding(0);
            this.PanelSettings.Name = "PanelSettings";
            this.PanelSettings.Size = new System.Drawing.Size(200, 500);
            this.PanelSettings.TabIndex = 1;
            this.PanelSettings.WrapContents = false;
            this.PanelSettings.SizeChanged += new System.EventHandler(this.PanelSettings_SizeChanged);
            this.PanelSettings.ControlAdded += new System.Windows.Forms.ControlEventHandler(this.PanelSettings_ControlAdded);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(699, 500);
            this.Controls.Add(this.PanelSettings);
            this.Controls.Add(this.Canvas);
            this.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private Canvas Canvas;
    private FlowLayoutPanel PanelSettings;
}