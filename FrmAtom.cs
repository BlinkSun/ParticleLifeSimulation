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
                Atom.AddParticle(particle.ToClone());
            foreach (Force force in atom.Forces)
                Atom.AddForce(force.AtomTarget == atom ? new(Atom, force.Value) : force.ToClone());

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
                using (Graphics gfx = Graphics.FromImage(bitmap))
                {
                    gfx.Clear(Color.Transparent);
                    float x = bitmap.Width * 0.25f;
                    float y = bitmap.Height * 0.25f;
                    float width = bitmap.Width / 2.0f;
                    float height = bitmap.Height / 2.0f;
                    ((FrmUniverse)Owner).Particle_Draw(gfx, 255, Atom.Color, x, y, width, height);
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
                    LblAtomName.Text = String.Empty;
                    NumValue.Value = 0;
                }
                else
                {
                    GrpForce.Enabled = true;
                    BtnSave.Enabled = false;

                    Force forceSelected = (Force)LstForces.SelectedItem;
                    LblAtomName.Text = forceSelected.AtomTarget.Name;
                    NumValue.Value = (decimal)forceSelected.Value;
                }
            });
            NumValue.ValueChanged += new((s, ev) =>
            {
                if (LstForces.SelectedItem is not null && (decimal)((Force)LstForces.SelectedItem).Value != NumValue.Value)
                    BtnSave.Enabled = true;
                else
                    BtnSave.Enabled = false;
            });
            LstForces.SelectedIndex = -1;
            GrpForce.Enabled = false;
            LblAtomName.Text = String.Empty;
            NumValue.Value = 0;
            BtnSave.Click += new((s, ev) =>
            {
                ((Force)LstForces.SelectedItem).Value = (double)NumValue.Value;
                BtnSave.Enabled = false;
            });
            BtnRandom.Click += new((s, ev) => NumValue.Value = (decimal)((Random.Shared.NextDoubleInclusive() * 2.0) - 1.0));
            Load += new(PicColorUpdate);
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}