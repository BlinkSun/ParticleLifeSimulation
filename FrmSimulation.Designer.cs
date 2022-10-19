using ParticleLifeSimulation.UserControls;

namespace ParticleLifeSimulation;

partial class FrmSimulation
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
            this.PanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.RightDocker = new System.Windows.Forms.Panel();
            this.TogglePanel = new System.Windows.Forms.CheckBox();
            this.StatusStripStatus = new System.Windows.Forms.StatusStrip();
            this.ToolStripStatusLabelTotalParticles = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabelTotalParticlesValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabelSpring = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabelFps = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatusLabelFpsValue = new System.Windows.Forms.ToolStripStatusLabel();
            this.RightDocker.SuspendLayout();
            this.StatusStripStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // PanelSettings
            // 
            this.PanelSettings.AllowDrop = true;
            this.PanelSettings.AutoSize = true;
            this.PanelSettings.BackColor = System.Drawing.Color.Transparent;
            this.PanelSettings.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelSettings.Location = new System.Drawing.Point(0, 0);
            this.PanelSettings.Name = "PanelSettings";
            this.PanelSettings.Size = new System.Drawing.Size(200, 0);
            this.PanelSettings.TabIndex = 0;
            // 
            // RightDocker
            // 
            this.RightDocker.AutoScroll = true;
            this.RightDocker.BackColor = System.Drawing.Color.Transparent;
            this.RightDocker.Controls.Add(this.PanelSettings);
            this.RightDocker.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightDocker.Location = new System.Drawing.Point(600, 0);
            this.RightDocker.Name = "RightDocker";
            this.RightDocker.Size = new System.Drawing.Size(200, 476);
            this.RightDocker.TabIndex = 1;
            // 
            // TogglePanel
            // 
            this.TogglePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TogglePanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.TogglePanel.AutoSize = true;
            this.TogglePanel.Location = new System.Drawing.Point(575, 3);
            this.TogglePanel.Name = "TogglePanel";
            this.TogglePanel.Size = new System.Drawing.Size(25, 25);
            this.TogglePanel.TabIndex = 0;
            this.TogglePanel.Text = ">";
            this.TogglePanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TogglePanel.UseVisualStyleBackColor = true;
            // 
            // StatusStripStatus
            // 
            this.StatusStripStatus.BackColor = System.Drawing.SystemColors.Control;
            this.StatusStripStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripStatusLabelTotalParticles,
            this.ToolStripStatusLabelTotalParticlesValue,
            this.ToolStripStatusLabelSpring,
            this.ToolStripStatusLabelFps,
            this.ToolStripStatusLabelFpsValue});
            this.StatusStripStatus.Location = new System.Drawing.Point(0, 476);
            this.StatusStripStatus.Name = "StatusStripStatus";
            this.StatusStripStatus.Size = new System.Drawing.Size(800, 24);
            this.StatusStripStatus.TabIndex = 2;
            // 
            // ToolStripStatusLabelTotalParticles
            // 
            this.ToolStripStatusLabelTotalParticles.Name = "ToolStripStatusLabelTotalParticles";
            this.ToolStripStatusLabelTotalParticles.Size = new System.Drawing.Size(85, 19);
            this.ToolStripStatusLabelTotalParticles.Text = "Total Particles :";
            // 
            // ToolStripStatusLabelTotalParticlesValue
            // 
            this.ToolStripStatusLabelTotalParticlesValue.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ToolStripStatusLabelTotalParticlesValue.Name = "ToolStripStatusLabelTotalParticlesValue";
            this.ToolStripStatusLabelTotalParticlesValue.Size = new System.Drawing.Size(17, 19);
            this.ToolStripStatusLabelTotalParticlesValue.Text = "0";
            // 
            // ToolStripStatusLabelSpring
            // 
            this.ToolStripStatusLabelSpring.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.ToolStripStatusLabelSpring.Name = "ToolStripStatusLabelSpring";
            this.ToolStripStatusLabelSpring.Size = new System.Drawing.Size(634, 19);
            this.ToolStripStatusLabelSpring.Spring = true;
            // 
            // ToolStripStatusLabelFps
            // 
            this.ToolStripStatusLabelFps.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.ToolStripStatusLabelFps.Name = "ToolStripStatusLabelFps";
            this.ToolStripStatusLabelFps.Size = new System.Drawing.Size(36, 19);
            this.ToolStripStatusLabelFps.Text = "FPS :";
            // 
            // ToolStripStatusLabelFpsValue
            // 
            this.ToolStripStatusLabelFpsValue.Name = "ToolStripStatusLabelFpsValue";
            this.ToolStripStatusLabelFpsValue.Size = new System.Drawing.Size(13, 19);
            this.ToolStripStatusLabelFpsValue.Text = "0";
            // 
            // FrmSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.TogglePanel);
            this.Controls.Add(this.RightDocker);
            this.Controls.Add(this.StatusStripStatus);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmSimulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulation";
            this.RightDocker.ResumeLayout(false);
            this.RightDocker.PerformLayout();
            this.StatusStripStatus.ResumeLayout(false);
            this.StatusStripStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private FlowLayoutPanel PanelSettings;
    private Panel RightDocker;
    private CheckBox TogglePanel;
    private StatusStrip StatusStripStatus;
    private ToolStripStatusLabel ToolStripStatusLabelTotalParticles;
    private ToolStripStatusLabel ToolStripStatusLabelTotalParticlesValue;
    private ToolStripStatusLabel ToolStripStatusLabelSpring;
    private ToolStripStatusLabel ToolStripStatusLabelFps;
    private ToolStripStatusLabel ToolStripStatusLabelFpsValue;
}