namespace ParticleLifeSimulation
{
    partial class FrmAtom
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
            this.GrpAtomSettings = new System.Windows.Forms.GroupBox();
            this.LblParticles = new System.Windows.Forms.Label();
            this.NumParticles = new System.Windows.Forms.NumericUpDown();
            this.LblRadius = new System.Windows.Forms.Label();
            this.BtnPicker = new System.Windows.Forms.Button();
            this.NumRadius = new System.Windows.Forms.NumericUpDown();
            this.LblColor = new System.Windows.Forms.Label();
            this.LblName = new System.Windows.Forms.Label();
            this.TxtName = new System.Windows.Forms.TextBox();
            this.PicColor = new System.Windows.Forms.PictureBox();
            this.GrpAtomForces = new System.Windows.Forms.GroupBox();
            this.GrpForce = new System.Windows.Forms.GroupBox();
            this.BtnRandomRadiation = new System.Windows.Forms.Button();
            this.NumRadiation = new System.Windows.Forms.NumericUpDown();
            this.LblRadiation = new System.Windows.Forms.Label();
            this.LblTargetName = new System.Windows.Forms.Label();
            this.BtnRandomAttraction = new System.Windows.Forms.Button();
            this.LblAttraction = new System.Windows.Forms.Label();
            this.NumAttraction = new System.Windows.Forms.NumericUpDown();
            this.BtnSave = new System.Windows.Forms.Button();
            this.LblTarget = new System.Windows.Forms.Label();
            this.LstForces = new System.Windows.Forms.ListBox();
            this.GrpAtomSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumParticles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumRadius)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).BeginInit();
            this.GrpAtomForces.SuspendLayout();
            this.GrpForce.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumRadiation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAttraction)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnOk
            // 
            this.BtnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnOk.Location = new System.Drawing.Point(423, 330);
            this.BtnOk.Name = "BtnOk";
            this.BtnOk.Size = new System.Drawing.Size(47, 30);
            this.BtnOk.TabIndex = 14;
            this.BtnOk.Text = "OK";
            this.BtnOk.UseVisualStyleBackColor = true;
            this.BtnOk.Click += new System.EventHandler(this.BtnOk_Click);
            // 
            // BtnCancel
            // 
            this.BtnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnCancel.Location = new System.Drawing.Point(337, 330);
            this.BtnCancel.Name = "BtnCancel";
            this.BtnCancel.Size = new System.Drawing.Size(80, 30);
            this.BtnCancel.TabIndex = 13;
            this.BtnCancel.Text = "CANCEL";
            this.BtnCancel.UseVisualStyleBackColor = true;
            // 
            // GrpAtomSettings
            // 
            this.GrpAtomSettings.Controls.Add(this.LblParticles);
            this.GrpAtomSettings.Controls.Add(this.NumParticles);
            this.GrpAtomSettings.Controls.Add(this.LblRadius);
            this.GrpAtomSettings.Controls.Add(this.BtnPicker);
            this.GrpAtomSettings.Controls.Add(this.NumRadius);
            this.GrpAtomSettings.Controls.Add(this.LblColor);
            this.GrpAtomSettings.Controls.Add(this.LblName);
            this.GrpAtomSettings.Controls.Add(this.TxtName);
            this.GrpAtomSettings.Controls.Add(this.PicColor);
            this.GrpAtomSettings.Location = new System.Drawing.Point(12, 12);
            this.GrpAtomSettings.Name = "GrpAtomSettings";
            this.GrpAtomSettings.Size = new System.Drawing.Size(458, 109);
            this.GrpAtomSettings.TabIndex = 1;
            this.GrpAtomSettings.TabStop = false;
            this.GrpAtomSettings.Text = "Atom Settings";
            // 
            // LblParticles
            // 
            this.LblParticles.AutoSize = true;
            this.LblParticles.Location = new System.Drawing.Point(248, 69);
            this.LblParticles.Name = "LblParticles";
            this.LblParticles.Size = new System.Drawing.Size(57, 15);
            this.LblParticles.TabIndex = 8;
            this.LblParticles.Text = "Particles :";
            // 
            // NumParticles
            // 
            this.NumParticles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumParticles.Location = new System.Drawing.Point(310, 67);
            this.NumParticles.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.NumParticles.Name = "NumParticles";
            this.NumParticles.Size = new System.Drawing.Size(131, 23);
            this.NumParticles.TabIndex = 3;
            // 
            // LblRadius
            // 
            this.LblRadius.AutoSize = true;
            this.LblRadius.Location = new System.Drawing.Point(19, 69);
            this.LblRadius.Name = "LblRadius";
            this.LblRadius.Size = new System.Drawing.Size(48, 15);
            this.LblRadius.TabIndex = 2;
            this.LblRadius.Text = "Radius :";
            // 
            // BtnPicker
            // 
            this.BtnPicker.Location = new System.Drawing.Point(343, 24);
            this.BtnPicker.Name = "BtnPicker";
            this.BtnPicker.Size = new System.Drawing.Size(98, 23);
            this.BtnPicker.TabIndex = 1;
            this.BtnPicker.Text = "Color Picker";
            this.BtnPicker.UseVisualStyleBackColor = true;
            // 
            // NumRadius
            // 
            this.NumRadius.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumRadius.DecimalPlaces = 1;
            this.NumRadius.Location = new System.Drawing.Point(81, 67);
            this.NumRadius.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.NumRadius.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumRadius.Name = "NumRadius";
            this.NumRadius.Size = new System.Drawing.Size(131, 23);
            this.NumRadius.TabIndex = 2;
            this.NumRadius.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // LblColor
            // 
            this.LblColor.AutoSize = true;
            this.LblColor.Location = new System.Drawing.Point(248, 28);
            this.LblColor.Name = "LblColor";
            this.LblColor.Size = new System.Drawing.Size(42, 15);
            this.LblColor.TabIndex = 5;
            this.LblColor.Text = "Color :";
            // 
            // LblName
            // 
            this.LblName.AutoSize = true;
            this.LblName.Location = new System.Drawing.Point(19, 28);
            this.LblName.Name = "LblName";
            this.LblName.Size = new System.Drawing.Size(45, 15);
            this.LblName.TabIndex = 4;
            this.LblName.Text = "Name :";
            // 
            // TxtName
            // 
            this.TxtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtName.Location = new System.Drawing.Point(81, 25);
            this.TxtName.Name = "TxtName";
            this.TxtName.Size = new System.Drawing.Size(131, 23);
            this.TxtName.TabIndex = 0;
            // 
            // PicColor
            // 
            this.PicColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PicColor.Location = new System.Drawing.Point(310, 22);
            this.PicColor.Name = "PicColor";
            this.PicColor.Size = new System.Drawing.Size(27, 27);
            this.PicColor.TabIndex = 1;
            this.PicColor.TabStop = false;
            // 
            // GrpAtomForces
            // 
            this.GrpAtomForces.Controls.Add(this.GrpForce);
            this.GrpAtomForces.Controls.Add(this.LstForces);
            this.GrpAtomForces.Location = new System.Drawing.Point(12, 127);
            this.GrpAtomForces.Name = "GrpAtomForces";
            this.GrpAtomForces.Size = new System.Drawing.Size(458, 192);
            this.GrpAtomForces.TabIndex = 2;
            this.GrpAtomForces.TabStop = false;
            this.GrpAtomForces.Text = "Atom Forces";
            // 
            // GrpForce
            // 
            this.GrpForce.Controls.Add(this.BtnRandomRadiation);
            this.GrpForce.Controls.Add(this.NumRadiation);
            this.GrpForce.Controls.Add(this.LblRadiation);
            this.GrpForce.Controls.Add(this.LblTargetName);
            this.GrpForce.Controls.Add(this.BtnRandomAttraction);
            this.GrpForce.Controls.Add(this.LblAttraction);
            this.GrpForce.Controls.Add(this.NumAttraction);
            this.GrpForce.Controls.Add(this.BtnSave);
            this.GrpForce.Controls.Add(this.LblTarget);
            this.GrpForce.Location = new System.Drawing.Point(167, 22);
            this.GrpForce.Name = "GrpForce";
            this.GrpForce.Size = new System.Drawing.Size(274, 155);
            this.GrpForce.TabIndex = 1;
            this.GrpForce.TabStop = false;
            this.GrpForce.Text = "Force";
            // 
            // BtnRandomRadiation
            // 
            this.BtnRandomRadiation.Location = new System.Drawing.Point(195, 50);
            this.BtnRandomRadiation.Name = "BtnRandomRadiation";
            this.BtnRandomRadiation.Size = new System.Drawing.Size(61, 23);
            this.BtnRandomRadiation.TabIndex = 16;
            this.BtnRandomRadiation.Text = "Random";
            this.BtnRandomRadiation.UseVisualStyleBackColor = true;
            // 
            // NumRadiation
            // 
            this.NumRadiation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumRadiation.DecimalPlaces = 10;
            this.NumRadiation.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumRadiation.Location = new System.Drawing.Point(88, 50);
            this.NumRadiation.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumRadiation.Name = "NumRadiation";
            this.NumRadiation.Size = new System.Drawing.Size(101, 23);
            this.NumRadiation.TabIndex = 15;
            // 
            // LblRadiation
            // 
            this.LblRadiation.AutoSize = true;
            this.LblRadiation.Location = new System.Drawing.Point(13, 52);
            this.LblRadiation.Name = "LblRadiation";
            this.LblRadiation.Size = new System.Drawing.Size(63, 15);
            this.LblRadiation.TabIndex = 14;
            this.LblRadiation.Text = "Radiation :";
            // 
            // LblTargetName
            // 
            this.LblTargetName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LblTargetName.Location = new System.Drawing.Point(88, 18);
            this.LblTargetName.Name = "LblTargetName";
            this.LblTargetName.Size = new System.Drawing.Size(168, 23);
            this.LblTargetName.TabIndex = 13;
            this.LblTargetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnRandomAttraction
            // 
            this.BtnRandomAttraction.Location = new System.Drawing.Point(195, 81);
            this.BtnRandomAttraction.Name = "BtnRandomAttraction";
            this.BtnRandomAttraction.Size = new System.Drawing.Size(61, 23);
            this.BtnRandomAttraction.TabIndex = 12;
            this.BtnRandomAttraction.Text = "Random";
            this.BtnRandomAttraction.UseVisualStyleBackColor = true;
            // 
            // LblAttraction
            // 
            this.LblAttraction.AutoSize = true;
            this.LblAttraction.Location = new System.Drawing.Point(13, 83);
            this.LblAttraction.Name = "LblAttraction";
            this.LblAttraction.Size = new System.Drawing.Size(66, 15);
            this.LblAttraction.TabIndex = 4;
            this.LblAttraction.Text = "Attraction :";
            // 
            // NumAttraction
            // 
            this.NumAttraction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NumAttraction.DecimalPlaces = 10;
            this.NumAttraction.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.NumAttraction.Location = new System.Drawing.Point(88, 81);
            this.NumAttraction.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumAttraction.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            -2147483648});
            this.NumAttraction.Name = "NumAttraction";
            this.NumAttraction.Size = new System.Drawing.Size(101, 23);
            this.NumAttraction.TabIndex = 7;
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(195, 117);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(61, 24);
            this.BtnSave.TabIndex = 11;
            this.BtnSave.Text = "Save";
            this.BtnSave.UseVisualStyleBackColor = true;
            // 
            // LblTarget
            // 
            this.LblTarget.AutoSize = true;
            this.LblTarget.Location = new System.Drawing.Point(13, 22);
            this.LblTarget.Name = "LblTarget";
            this.LblTarget.Size = new System.Drawing.Size(45, 15);
            this.LblTarget.TabIndex = 4;
            this.LblTarget.Text = "Target :";
            // 
            // LstForces
            // 
            this.LstForces.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LstForces.FormattingEnabled = true;
            this.LstForces.ItemHeight = 15;
            this.LstForces.Location = new System.Drawing.Point(19, 25);
            this.LstForces.Name = "LstForces";
            this.LstForces.ScrollAlwaysVisible = true;
            this.LstForces.Size = new System.Drawing.Size(142, 152);
            this.LstForces.TabIndex = 5;
            // 
            // FrmAtom
            // 
            this.AcceptButton = this.BtnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.BtnCancel;
            this.ClientSize = new System.Drawing.Size(482, 372);
            this.Controls.Add(this.GrpAtomForces);
            this.Controls.Add(this.GrpAtomSettings);
            this.Controls.Add(this.BtnCancel);
            this.Controls.Add(this.BtnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmAtom";
            this.Text = "Atom";
            this.GrpAtomSettings.ResumeLayout(false);
            this.GrpAtomSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumParticles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumRadius)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PicColor)).EndInit();
            this.GrpAtomForces.ResumeLayout(false);
            this.GrpForce.ResumeLayout(false);
            this.GrpForce.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NumRadiation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NumAttraction)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Button BtnOk;
        private Button BtnCancel;
        private GroupBox GrpAtomSettings;
        private TextBox TxtName;
        private Label LblRadius;
        private NumericUpDown NumRadius;
        private Label LblColor;
        private Button BtnPicker;
        private Label LblName;
        private PictureBox PicColor;
        private Label LblParticles;
        private NumericUpDown NumParticles;
        private GroupBox GrpAtomForces;
        private ListBox LstForces;
        private GroupBox GrpForce;
        private Label LblAttraction;
        private NumericUpDown NumAttraction;
        private Label LblTarget;
        private Button BtnSave;
        private Button BtnRandomAttraction;
        private Label LblTargetName;
        private Button BtnRandomRadiation;
        private NumericUpDown NumRadiation;
        private Label LblRadiation;
    }
}