namespace ParticleLifeSimulation
{
    partial class FrmSettings
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
            this.GrpSimulation = new System.Windows.Forms.GroupBox();
            this.LblStepsByFrame = new System.Windows.Forms.Label();
            this.BtnPicker = new System.Windows.Forms.Button();
            this.LblSteps = new System.Windows.Forms.Label();
            this.LblBackColor = new System.Windows.Forms.Label();
            this.NumStepsByFrame = new System.Windows.Forms.NumericUpDown();
            this.ChkAnimated = new System.Windows.Forms.CheckBox();
            this.PicColor = new System.Windows.Forms.PictureBox();
            this.GrpParticles = new System.Windows.Forms.GroupBox();
            this.ChkBorderless = new System.Windows.Forms.CheckBox();
            this.ChkContraste = new System.Windows.Forms.CheckBox();
            this.BtnResetDefault = new System.Windows.Forms.Button();
            this.ChkCompoundColorNamesIncluded = new System.Windows.Forms.CheckBox();
            this.ChkSystemColorsIncluded = new System.Windows.Forms.CheckBox();
            this.GrpSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumStepsByFrame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).BeginInit();
            this.GrpParticles.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(406, 229);
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
            this.BtnCancel.Location = new System.Drawing.Point(320, 229);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 30);
            this.BtnCancel.TabIndex = 0;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GrpSimulation
            // 
            this.GrpSimulation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpSimulation.Controls.Add(this.LblStepsByFrame);
            this.GrpSimulation.Controls.Add(this.BtnPicker);
            this.GrpSimulation.Controls.Add(this.LblSteps);
            this.GrpSimulation.Controls.Add(this.LblBackColor);
            this.GrpSimulation.Controls.Add(this.NumStepsByFrame);
            this.GrpSimulation.Controls.Add(this.ChkAnimated);
            this.GrpSimulation.Controls.Add(this.PicColor);
            this.GrpSimulation.Location = new System.Drawing.Point(12, 12);
            this.GrpSimulation.Name = "GrpSimulation";
            this.GrpSimulation.Size = new System.Drawing.Size(441, 99);
            this.GrpSimulation.TabIndex = 1;
            this.GrpSimulation.TabStop = false;
            this.GrpSimulation.Text = "Simulation";
            // 
            // LblStepsByFrame
            // 
            this.LblStepsByFrame.AutoSize = true;
            this.LblStepsByFrame.Location = new System.Drawing.Point(381, 28);
            this.LblStepsByFrame.Name = "LblStepsByFrame";
            this.LblStepsByFrame.Size = new System.Drawing.Size(49, 15);
            this.LblStepsByFrame.TabIndex = 3;
            this.LblStepsByFrame.Text = " / frame";
            // 
            // BtnPicker
            // 
            this.BtnPicker.Location = new System.Drawing.Point(164, 24);
            this.BtnPicker.Name = "BtnPicker";
            this.BtnPicker.Size = new System.Drawing.Size(59, 23);
            this.BtnPicker.TabIndex = 6;
            this.BtnPicker.Text = "Picker";
            this.BtnPicker.UseVisualStyleBackColor = true;
            this.BtnPicker.Click += new System.EventHandler(this.PicColor_Click);
            // 
            // LblSteps
            // 
            this.LblSteps.AutoSize = true;
            this.LblSteps.Location = new System.Drawing.Point(273, 28);
            this.LblSteps.Name = "LblSteps";
            this.LblSteps.Size = new System.Drawing.Size(41, 15);
            this.LblSteps.TabIndex = 2;
            this.LblSteps.Text = "Steps :";
            // 
            // LblBackColor
            // 
            this.LblBackColor.AutoSize = true;
            this.LblBackColor.Location = new System.Drawing.Point(19, 28);
            this.LblBackColor.Name = "LblBackColor";
            this.LblBackColor.Size = new System.Drawing.Size(67, 15);
            this.LblBackColor.TabIndex = 5;
            this.LblBackColor.Text = "BackColor :";
            // 
            // NumStepsByFrame
            // 
            this.NumStepsByFrame.Location = new System.Drawing.Point(320, 24);
            this.NumStepsByFrame.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.NumStepsByFrame.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumStepsByFrame.Name = "NumStepsByFrame";
            this.NumStepsByFrame.Size = new System.Drawing.Size(58, 23);
            this.NumStepsByFrame.TabIndex = 1;
            this.NumStepsByFrame.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ChkAnimated
            // 
            this.ChkAnimated.AutoSize = true;
            this.ChkAnimated.Location = new System.Drawing.Point(19, 64);
            this.ChkAnimated.Name = "ChkAnimated";
            this.ChkAnimated.Size = new System.Drawing.Size(78, 19);
            this.ChkAnimated.TabIndex = 0;
            this.ChkAnimated.Text = "Animated";
            this.ChkAnimated.UseVisualStyleBackColor = true;
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
            // GrpParticles
            // 
            this.GrpParticles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GrpParticles.Controls.Add(this.ChkCompoundColorNamesIncluded);
            this.GrpParticles.Controls.Add(this.ChkSystemColorsIncluded);
            this.GrpParticles.Controls.Add(this.ChkBorderless);
            this.GrpParticles.Controls.Add(this.ChkContraste);
            this.GrpParticles.Location = new System.Drawing.Point(12, 117);
            this.GrpParticles.Name = "GrpParticles";
            this.GrpParticles.Size = new System.Drawing.Size(441, 92);
            this.GrpParticles.TabIndex = 2;
            this.GrpParticles.TabStop = false;
            this.GrpParticles.Text = "Particles";
            // 
            // ChkBorderless
            // 
            this.ChkBorderless.AutoSize = true;
            this.ChkBorderless.Location = new System.Drawing.Point(19, 57);
            this.ChkBorderless.Name = "ChkBorderless";
            this.ChkBorderless.Size = new System.Drawing.Size(80, 19);
            this.ChkBorderless.TabIndex = 2;
            this.ChkBorderless.Text = "Borderless";
            this.ChkBorderless.UseVisualStyleBackColor = true;
            // 
            // ChkContraste
            // 
            this.ChkContraste.AutoSize = true;
            this.ChkContraste.Location = new System.Drawing.Point(19, 22);
            this.ChkContraste.Name = "ChkContraste";
            this.ChkContraste.Size = new System.Drawing.Size(77, 19);
            this.ChkContraste.TabIndex = 1;
            this.ChkContraste.Text = "Contraste";
            this.ChkContraste.UseVisualStyleBackColor = true;
            // 
            // BtnResetDefault
            // 
            this.BtnResetDefault.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BtnResetDefault.Location = new System.Drawing.Point(12, 229);
            this.BtnResetDefault.Name = "BtnResetDefault";
            this.BtnResetDefault.Size = new System.Drawing.Size(86, 30);
            this.BtnResetDefault.TabIndex = 3;
            this.BtnResetDefault.Text = "Reset Default";
            this.BtnResetDefault.UseVisualStyleBackColor = true;
            this.BtnResetDefault.Click += new System.EventHandler(this.BtnResetDefault_Click);
            // 
            // ChkCompoundColorNamesIncluded
            // 
            this.ChkCompoundColorNamesIncluded.AutoSize = true;
            this.ChkCompoundColorNamesIncluded.Location = new System.Drawing.Point(174, 57);
            this.ChkCompoundColorNamesIncluded.Name = "ChkCompoundColorNamesIncluded";
            this.ChkCompoundColorNamesIncluded.Size = new System.Drawing.Size(256, 19);
            this.ChkCompoundColorNamesIncluded.TabIndex = 4;
            this.ChkCompoundColorNamesIncluded.Text = "Random Compound Color Names Included";
            this.ChkCompoundColorNamesIncluded.UseVisualStyleBackColor = true;
            // 
            // ChkSystemColorsIncluded
            // 
            this.ChkSystemColorsIncluded.AutoSize = true;
            this.ChkSystemColorsIncluded.Location = new System.Drawing.Point(174, 22);
            this.ChkSystemColorsIncluded.Name = "ChkSystemColorsIncluded";
            this.ChkSystemColorsIncluded.Size = new System.Drawing.Size(198, 19);
            this.ChkSystemColorsIncluded.TabIndex = 3;
            this.ChkSystemColorsIncluded.Text = "Random System Colors Included";
            this.ChkSystemColorsIncluded.UseVisualStyleBackColor = true;
            // 
            // FrmSettings
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(465, 271);
            this.Controls.Add(this.BtnResetDefault);
            this.Controls.Add(this.GrpParticles);
            this.Controls.Add(this.GrpSimulation);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmSettings";
            this.Text = "Settings";
            this.GrpSimulation.ResumeLayout(false);
            this.GrpSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumStepsByFrame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).EndInit();
            this.GrpParticles.ResumeLayout(false);
            this.GrpParticles.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button BtnOk;
        private Button BtnCancel;
        private GroupBox GrpSimulation;
        private PictureBox PicColor;
        private CheckBox ChkAnimated;
        private Label LblSteps;
        private NumericUpDown NumStepsByFrame;
        private Label LblBackColor;
        private Label LblStepsByFrame;
        private Button BtnPicker;
        private GroupBox GrpParticles;
        private CheckBox ChkBorderless;
        private CheckBox ChkContraste;
        private Button BtnResetDefault;
        private CheckBox ChkCompoundColorNamesIncluded;
        private CheckBox ChkSystemColorsIncluded;
    }
}