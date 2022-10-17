using ParticleLifeSimulation.UserControls;

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
            this.PanelSettings = new System.Windows.Forms.FlowLayoutPanel();
            this.RightDocker = new System.Windows.Forms.Panel();
            this.TogglePanel = new System.Windows.Forms.CheckBox();
            this.StatusToolStripStats = new System.Windows.Forms.StatusStrip();
            this.ToolStripTotalParticles = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatsTotalParticles = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatsSpace = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.ToolStripStatsFPS = new System.Windows.Forms.ToolStripStatusLabel();
            this.RightDocker.SuspendLayout();
            this.StatusToolStripStats.SuspendLayout();
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
            this.PanelSettings.TabIndex = 1;
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
            this.RightDocker.TabIndex = 2;
            // 
            // TogglePanel
            // 
            this.TogglePanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TogglePanel.Appearance = System.Windows.Forms.Appearance.Button;
            this.TogglePanel.AutoSize = true;
            this.TogglePanel.Location = new System.Drawing.Point(575, 3);
            this.TogglePanel.Name = "TogglePanel";
            this.TogglePanel.Size = new System.Drawing.Size(25, 25);
            this.TogglePanel.TabIndex = 3;
            this.TogglePanel.Text = ">";
            this.TogglePanel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.TogglePanel.UseVisualStyleBackColor = true;
            // 
            // StatusToolStripStats
            // 
            this.StatusToolStripStats.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripTotalParticles,
            this.ToolStripStatsTotalParticles,
            this.ToolStripStatsSpace,
            this.ToolStripFPS,
            this.ToolStripStatsFPS});
            this.StatusToolStripStats.Location = new System.Drawing.Point(0, 476);
            this.StatusToolStripStats.Name = "StatusToolStripStats";
            this.StatusToolStripStats.Size = new System.Drawing.Size(800, 24);
            this.StatusToolStripStats.TabIndex = 4;
            this.StatusToolStripStats.Text = "statusStrip1";
            // 
            // ToolStripTotalParticles
            // 
            this.ToolStripTotalParticles.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripTotalParticles.Name = "ToolStripTotalParticles";
            this.ToolStripTotalParticles.Size = new System.Drawing.Size(85, 19);
            this.ToolStripTotalParticles.Text = "Total Particles :";
            // 
            // ToolStripStatsTotalParticles
            // 
            this.ToolStripStatsTotalParticles.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripStatsTotalParticles.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.ToolStripStatsTotalParticles.Name = "ToolStripStatsTotalParticles";
            this.ToolStripStatsTotalParticles.Size = new System.Drawing.Size(17, 19);
            this.ToolStripStatsTotalParticles.Text = "0";
            // 
            // ToolStripStatsSpace
            // 
            this.ToolStripStatsSpace.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripStatsSpace.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.ToolStripStatsSpace.Name = "ToolStripStatsSpace";
            this.ToolStripStatsSpace.Size = new System.Drawing.Size(603, 19);
            this.ToolStripStatsSpace.Spring = true;
            // 
            // ToolStripFPS
            // 
            this.ToolStripFPS.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripFPS.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.ToolStripFPS.Name = "ToolStripFPS";
            this.ToolStripFPS.Size = new System.Drawing.Size(36, 19);
            this.ToolStripFPS.Text = "FPS :";
            // 
            // ToolStripStatsFPS
            // 
            this.ToolStripStatsFPS.BackColor = System.Drawing.SystemColors.Control;
            this.ToolStripStatsFPS.Name = "ToolStripStatsFPS";
            this.ToolStripStatsFPS.Size = new System.Drawing.Size(13, 19);
            this.ToolStripStatsFPS.Text = "0";
            // 
            // FrmUniverse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.TogglePanel);
            this.Controls.Add(this.RightDocker);
            this.Controls.Add(this.StatusToolStripStats);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmUniverse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Universe";
            this.RightDocker.ResumeLayout(false);
            this.RightDocker.PerformLayout();
            this.StatusToolStripStats.ResumeLayout(false);
            this.StatusToolStripStats.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private FlowLayoutPanel PanelSettings;
    private Panel RightDocker;
    private CheckBox TogglePanel;
    private StatusStrip StatusToolStripStats;
    private ToolStripStatusLabel ToolStripTotalParticles;
    private ToolStripStatusLabel ToolStripStatsTotalParticles;
    private ToolStripStatusLabel ToolStripStatsSpace;
    private ToolStripStatusLabel ToolStripFPS;
    private ToolStripStatusLabel ToolStripStatsFPS;
}