namespace ParticleLifeSimulation
{
    public partial class FrmSettings : Form
    {
        public FrmSettings(Settings settings)
        {
            InitializeComponent();
            PicColor.DataBindings.Add("BackColor", settings, "SimulationBackColor", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkAnimated.DataBindings.Add("Checked", settings, "SimulationAnimated", true, DataSourceUpdateMode.OnPropertyChanged);
            NumStepsByFrame.DataBindings.Add("Value", settings, "SimulationStepsPerFrame", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkContraste.DataBindings.Add("Checked", settings, "ParticlesContraste", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkBorderless.DataBindings.Add("Checked", settings, "ParticlesBorderless", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkSystemColorsIncluded.DataBindings.Add("Checked", settings, "ParticlesSystemColorsIncluded", true, DataSourceUpdateMode.OnPropertyChanged);
            ChkCompoundColorNamesIncluded.DataBindings.Add("Checked", settings, "ParticlesCompoundColorNamesIncluded", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void PicColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new()
            {
                SolidColorOnly = true,
                Color = PicColor.BackColor
            };
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                PicColor.BackColor = colorDialog.Color;
            }
        }

        private void BtnResetDefault_Click(object sender, EventArgs e)
        {
            DialogResult = MessageBox.Show(this, "Are you sure to reset settings to default ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            Close();
        }
    }
}