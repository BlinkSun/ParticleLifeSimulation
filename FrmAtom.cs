using ParticleLifeSimulation.Core;

namespace ParticleLifeSimulation
{
    public partial class FrmAtom : Form
    {
        public Atom Atom { get; private set; }
        public FrmAtom(Atom atom)
        {
            InitializeComponent();

            //Clone Atom
            Atom = atom.ToClone();
            foreach (Particle particle in atom.Particles)
                Atom.Particles.Add(particle.ToClone());
            foreach (Force force in atom.Forces)
                Atom.AddForce(force.Target == atom ? new(Atom, force.Radiation, force.Attraction) : force.ToClone());

            //Initialize Components
            TxtName.Text = Atom.Name;
            TxtName.TextChanged += new((s, ev) =>
            {
                Atom.Name = TxtName.Text;
                int index = LstForces.Items.IndexOf(Atom.GetForceWith(Atom)!);
                LstForces.Items[index] = LstForces.Items[index];
            });
            void PicColorUpdate(object? sender, EventArgs e)
            {
                Bitmap bitmap = new(PicColor.ClientSize.Height, PicColor.ClientSize.Height);
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.Transparent);
                    float x = bitmap.Width * 0.25f;
                    float y = bitmap.Height * 0.25f;
                    float width = bitmap.Width / 2.0f;
                    float height = bitmap.Height / 2.0f;
                    ((FrmSimulation)Owner).ParticleDraw(graphics, 255, Atom.Color, x, y, width, height);
                }
                PicColor.Image = bitmap;
            }
            Atom.AtomColorChanged += new(PicColorUpdate);
            void ColorPicker(object? sender, EventArgs e)
            {
                ColorDialog colorDialog = new()
                {
                    SolidColorOnly = true,
                    Color = Atom.Color
                };
                if (colorDialog.ShowDialog() == DialogResult.OK)
                {
                    Atom.Color = colorDialog.Color;
                }
            }
            PicColor.Click += new(ColorPicker);
            BtnPicker.Click += new(ColorPicker);
            NumRadius.Value = (decimal)Atom.Radius;
            NumRadius.ValueChanged += new((s, ev) => Atom.Radius = (double)NumRadius.Value);
            NumParticles.Value = Atom.Particles.Count;
            NumParticles.ValueChanged += new((s, ev) => Atom.UpdateParticles((int)NumParticles.Value));
            LstForces.DisplayMember = "Name";
            LstForces.Items.AddRange(Atom.Forces.ToArray());
            LstForces.SelectedIndexChanged += new((s, ev) =>
            {
                if (LstForces.SelectedIndex == -1)
                {
                    GrpForce.Enabled = false;
                    LblTargetName.Text = string.Empty;
                    NumRadiation.Value = 0;
                    NumAttraction.Value = 0;
                }
                else
                {
                    GrpForce.Enabled = true;
                    BtnSave.Enabled = false;

                    Force forceSelected = (Force)LstForces.SelectedItem;
                    LblTargetName.Text = forceSelected.Target.Name;
                    NumRadiation.Value = (decimal)forceSelected.Radiation;
                    NumAttraction.Value = (decimal)forceSelected.Attraction;
                }
            });
            NumRadiation.ValueChanged += new((s, ev) =>
            {
                if (LstForces.SelectedItem is not null && (
                (decimal)((Force)LstForces.SelectedItem).Radiation != NumRadiation.Value ||
                (decimal)((Force)LstForces.SelectedItem).Attraction != NumAttraction.Value))
                    BtnSave.Enabled = true;
                else
                    BtnSave.Enabled = false;
            });
            NumAttraction.ValueChanged += new((s, ev) =>
            {
                if (LstForces.SelectedItem is not null && (
                (decimal)((Force)LstForces.SelectedItem).Radiation != NumRadiation.Value ||
                (decimal)((Force)LstForces.SelectedItem).Attraction != NumAttraction.Value))
                    BtnSave.Enabled = true;
                else
                    BtnSave.Enabled = false;
            });
            LstForces.SelectedIndex = -1;
            GrpForce.Enabled = false;
            LblTargetName.Text = string.Empty;
            NumRadiation.Value = 0;
            NumAttraction.Value = 0;
            BtnSave.Click += new((s, ev) =>
            {
                ((Force)LstForces.SelectedItem).Attraction = (double)NumAttraction.Value;
                BtnSave.Enabled = false;
            });
            BtnRandomRadiation.Click += new((s, ev) => NumRadiation.Value = (decimal)Random.Shared.NextDoubleInclusive(0.0, 1.0));
            BtnRandomAttraction.Click += new((s, ev) => NumAttraction.Value = (decimal)Random.Shared.NextDoubleInclusive(-1.0, 1.0));
            Load += new(PicColorUpdate);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}