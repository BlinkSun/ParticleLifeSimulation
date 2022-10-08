namespace ParticleLifeSimulation
{
    partial class FrmCanvasSettings
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
            this.BtnOk = new System.Windows.Forms.Button();
            this.BtnCancel = new System.Windows.Forms.Button();
            this.GrpBackground = new System.Windows.Forms.GroupBox();
            this.BtnPicker = new System.Windows.Forms.Button();
            this.LblColor = new System.Windows.Forms.Label();
            this.LblImge = new System.Windows.Forms.Label();
            this.BtnBrowser = new System.Windows.Forms.Button();
            this.TxtFilename = new System.Windows.Forms.TextBox();
            this.PicColor = new System.Windows.Forms.PictureBox();
            this.GrpAnimation = new System.Windows.Forms.GroupBox();
            this.LblIntervalPerFrame = new System.Windows.Forms.Label();
            this.LblInterval = new System.Windows.Forms.Label();
            this.NumInterval = new System.Windows.Forms.NumericUpDown();
            this.ChkAnimated = new System.Windows.Forms.CheckBox();
            this.GrpBackground.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).BeginInit();
            this.GrpAnimation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(423, 179);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(47, 30);
            this.BtnOk.TabIndex = 0;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(337, 179);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 30);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GrpBackground
            // 
            this.GrpBackground.Controls.Add(this.BtnPicker);
            this.GrpBackground.Controls.Add(this.LblColor);
            this.GrpBackground.Controls.Add(this.LblImge);
            this.GrpBackground.Controls.Add(this.BtnBrowser);
            this.GrpBackground.Controls.Add(this.TxtFilename);
            this.GrpBackground.Controls.Add(this.PicColor);
            this.GrpBackground.Location = new System.Drawing.Point(12, 12);
            this.GrpBackground.Name = "GrpBackground";
            this.GrpBackground.Size = new System.Drawing.Size(458, 94);
            this.GrpBackground.TabIndex = 1;
            this.GrpBackground.TabStop = false;
            this.GrpBackground.Text = "Background";
            // 
            // BtnPicker
            // 
            this.BtnPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnPicker.Location = new System.Drawing.Point(164, 24);
            this.BtnPicker.Name = "BtnPicker";
            this.BtnPicker.Size = new System.Drawing.Size(59, 23);
            this.BtnPicker.TabIndex = 6;
            this.BtnPicker.Text = "Picker";
            this.BtnPicker.UseVisualStyleBackColor = true;
            this.BtnPicker.Click += new System.EventHandler(this.PicColor_Click);
            // 
            // LblColor
            // 
            this.LblColor.AutoSize = true;
            this.LblColor.Location = new System.Drawing.Point(19, 28);
            this.LblColor.Name = "LblColor";
            this.LblColor.Size = new System.Drawing.Size(42, 15);
            this.LblColor.TabIndex = 5;
            this.LblColor.Text = "Color :";
            // 
            // LblImge
            // 
            this.LblImge.AutoSize = true;
            this.LblImge.Location = new System.Drawing.Point(19, 61);
            this.LblImge.Name = "LblImge";
            this.LblImge.Size = new System.Drawing.Size(46, 15);
            this.LblImge.TabIndex = 4;
            this.LblImge.Text = "Image :";
            // 
            // BtnBrowser
            // 
            this.BtnBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnBrowser.Location = new System.Drawing.Point(387, 58);
            this.BtnBrowser.Name = "BtnBrowser";
            this.BtnBrowser.Size = new System.Drawing.Size(59, 23);
            this.BtnBrowser.TabIndex = 3;
            this.BtnBrowser.Text = "Browser";
            this.BtnBrowser.UseVisualStyleBackColor = true;
            this.BtnBrowser.Click += new System.EventHandler(this.BtnOpenImage_Click);
            // 
            // TxtFilename
            // 
            this.TxtFilename.Location = new System.Drawing.Point(92, 58);
            this.TxtFilename.Name = "TxtFilename";
            this.TxtFilename.Size = new System.Drawing.Size(289, 23);
            this.TxtFilename.TabIndex = 2;
            this.TxtFilename.DoubleClick += new System.EventHandler(this.BtnOpenImage_Click);
            // 
            // PicColor
            // 
            this.PicColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PicColor.Location = new System.Drawing.Point(92, 22);
            this.PicColor.Name = "PicColor";
            this.PicColor.Size = new System.Drawing.Size(66, 27);
            this.PicColor.TabIndex = 1;
            this.PicColor.TabStop = false;
            this.PicColor.Click += new System.EventHandler(this.PicColor_Click);
            // 
            // GrpAnimation
            // 
            this.GrpAnimation.Controls.Add(this.LblIntervalPerFrame);
            this.GrpAnimation.Controls.Add(this.LblInterval);
            this.GrpAnimation.Controls.Add(this.NumInterval);
            this.GrpAnimation.Controls.Add(this.ChkAnimated);
            this.GrpAnimation.Location = new System.Drawing.Point(12, 112);
            this.GrpAnimation.Name = "GrpAnimation";
            this.GrpAnimation.Size = new System.Drawing.Size(458, 56);
            this.GrpAnimation.TabIndex = 3;
            this.GrpAnimation.TabStop = false;
            this.GrpAnimation.Text = "Animation";
            // 
            // LblIntervalPerFrame
            // 
            this.LblIntervalPerFrame.AutoSize = true;
            this.LblIntervalPerFrame.Location = new System.Drawing.Point(331, 23);
            this.LblIntervalPerFrame.Name = "LblIntervalPerFrame";
            this.LblIntervalPerFrame.Size = new System.Drawing.Size(115, 15);
            this.LblIntervalPerFrame.TabIndex = 3;
            this.LblIntervalPerFrame.Text = "milliseconds / frame";
            // 
            // LblInterval
            // 
            this.LblInterval.AutoSize = true;
            this.LblInterval.Location = new System.Drawing.Point(212, 23);
            this.LblInterval.Name = "LblInterval";
            this.LblInterval.Size = new System.Drawing.Size(52, 15);
            this.LblInterval.TabIndex = 2;
            this.LblInterval.Text = "Interval :";
            // 
            // NumInterval
            // 
            this.NumInterval.Location = new System.Drawing.Point(270, 19);
            this.NumInterval.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumInterval.Name = "NumInterval";
            this.NumInterval.Size = new System.Drawing.Size(58, 23);
            this.NumInterval.TabIndex = 1;
            this.NumInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ChkAnimated
            // 
            this.ChkAnimated.AutoSize = true;
            this.ChkAnimated.Location = new System.Drawing.Point(19, 22);
            this.ChkAnimated.Name = "ChkAnimated";
            this.ChkAnimated.Size = new System.Drawing.Size(78, 19);
            this.ChkAnimated.TabIndex = 0;
            this.ChkAnimated.Text = "Animated";
            this.ChkAnimated.UseVisualStyleBackColor = true;
            // 
            // FrmCanvasSettings
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(482, 221);
            this.Controls.Add(this.GrpAnimation);
            this.Controls.Add(this.GrpBackground);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmCanvasSettings";
            this.Text = "Canvas Settings";
            this.GrpBackground.ResumeLayout(false);
            this.GrpBackground.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).EndInit();
            this.GrpAnimation.ResumeLayout(false);
            this.GrpAnimation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button BtnOk;
        private Button BtnCancel;
        private GroupBox GrpBackground;
        private Button BtnBrowser;
        private TextBox TxtFilename;
        private PictureBox PicColor;
        private GroupBox GrpAnimation;
        private CheckBox ChkAnimated;
        private Label LblInterval;
        private NumericUpDown NumInterval;
        private Label LblColor;
        private Label LblImge;
        private Label LblIntervalPerFrame;
        private Button BtnPicker;
    }
}