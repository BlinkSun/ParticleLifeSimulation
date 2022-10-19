using ParticleLifeSimulation.Core;
using ParticleLifeSimulation.Properties;
using ParticleLifeSimulation.UserControls;
using static ParticleLifeSimulation.Core.Extensions;

namespace ParticleLifeSimulation;

public partial class FrmSimulation : Form, IFormLoop
{
    #region Properties
    private Bitmap BackgroundImageBuffer;
    private Settings Settings;
    private Universe Universe;

    private long ElapsedTime;
    private int FrameCounter;
    private int FramesPerSecond;
    private int LastRightDockerWidth;
    #endregion

    #region Constructor
    public FrmSimulation()
    {
        InitializeComponent();

        //How to brute force a protected property ;) (Avoid flicking, BackColor = Transparent)
        System.Reflection.PropertyInfo setDoubleBuffered = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        setDoubleBuffered.SetValue(RightDocker, true, null);
        setDoubleBuffered.SetValue(PanelSettings, true, null);

        BackgroundImageBuffer = new(ClientSize.Width, ClientSize.Height);

        Universe = new(ClientSize.Width, ClientSize.Height);

        Settings = new();

        InitializeSettingsUI();
        InitializeParticlesUI();
        InitializeUniverseUI();
    }
    #endregion

    #region SettingsUI
    private void InitializeSettingsUI()
    {
        void resizePanelSettingsControls()
        {
            foreach (Control control in PanelSettings.Controls)
            {
                control.Width = PanelSettings.ClientSize.Width - (PanelSettings.Margin.Left + PanelSettings.Margin.Right);
                if (control.Tag is string tag) switch (tag)
                    {
                        case "FIRST":
                            PanelSettings.Controls.SetChildIndex(control, 0);
                            break;
                        case "LAST":
                            PanelSettings.Controls.SetChildIndex(control, PanelSettings.Controls.Count - 1);
                            break;
                    }
            }
        }
        PanelSettings.DragOver += new((s, dev) =>
        {
            if (dev.Data!.GetDataPresent(typeof(Accordion)))
            {
                Point mousePosition = PanelSettings.PointToClient(new Point(dev.X, dev.Y));
                Accordion accSource = (Accordion)dev.Data!.GetData(typeof(Accordion));
                if (PanelSettings.GetChildAtPoint(mousePosition) is Accordion accTarget && accTarget != accSource && accTarget.IsDraggable)
                    dev.Effect = DragDropEffects.Move;
                else
                    dev.Effect = DragDropEffects.None;
            }
        });
        PanelSettings.DragDrop += new((s, dev) =>
        {
            Accordion accSource = (Accordion)dev.Data!.GetData(typeof(Accordion));
            Point mousePosition = PanelSettings.PointToClient(new Point(dev.X, dev.Y));
            Accordion? accTarget = PanelSettings.GetChildAtPoint(mousePosition) as Accordion;
            int accIndexTarget = PanelSettings.Controls.IndexOf(accTarget);
            if (accIndexTarget != -1) PanelSettings.Controls.SetChildIndex(accSource, accIndexTarget);
        });
        TogglePanel.CheckedChanged += new((s, ev) =>
        {
            if (TogglePanel.Checked)
            {
                LastRightDockerWidth = RightDocker.Width;
                RightDocker.Width = 0;
                TogglePanel.Left = RightDocker.Left - TogglePanel.Width;
                TogglePanel.Text = "<";
            }
            else
            {
                RightDocker.Width = LastRightDockerWidth;
                TogglePanel.Left = RightDocker.Left - TogglePanel.Width;
                TogglePanel.Text = ">";
            }
        });
        PanelSettings.ControlAdded += new((s, cev) => resizePanelSettingsControls());
        PanelSettings.ClientSizeChanged += new((s, ev) => resizePanelSettingsControls());

        ContextMenuStrip = new();
        ToolStripMenuItem toolStripSettings = new() { Text = "Settings" };
        toolStripSettings.Click += (s, ev) =>
        {
            FrmSettings frmSettings = new(Settings) { Owner = this, StartPosition = FormStartPosition.CenterParent };
            DialogResult dialogResult = frmSettings.ShowDialog(this);
            switch (dialogResult)
            {
                case DialogResult.OK:
                    Settings.Save();
                    break;
                case DialogResult.Yes:
                    Settings.Reset();
                    break;
                default:
                    Settings.Reload();
                    break;
            }
        };
        ContextMenuStrip.Items.Add(toolStripSettings);
    }
    #endregion

    #region ParticlesUI
    private void InitializeParticlesUI()
    {
        // Particles AccordionUI
        Accordion accordionParticlesUI = new() { Title = "Particles" };

        // Particles Contraste
        CheckBox chkContraste = new() { Text = "Contraste", Checked = Settings.ParticlesContraste };
        chkContraste.CheckedChanged += new((s, ev) =>
        {
            Settings.ParticlesContraste = chkContraste.Checked;
            Settings.Save();
            PanelSettings.Invalidate(true);
        });

        // Particles Borderless
        CheckBox chkBorderless = new() { Text = "Borderless", Checked = Settings.ParticlesBorderless };
        chkBorderless.CheckedChanged += new((s, ev) =>
        {
            Settings.ParticlesBorderless = chkBorderless.Checked;
            Settings.Save();
            PanelSettings.Invalidate(true);
        });

        accordionParticlesUI.Controls.AddRange(new Control[] { chkContraste, chkBorderless});
        PanelSettings.Controls.Add(accordionParticlesUI);
    }
    #endregion

    #region UniverseUI
    private void InitializeUniverseUI()
    {
        ClientSizeChanged += new((s, ev) =>
        {
            if (WindowState == FormWindowState.Minimized) return;
            BackgroundImageBuffer = new(BackgroundImageBuffer, ClientSize);
            Universe.Size = ClientSize;
        });
        Universe.UniverseAtomAdded += new((s, uev) =>
        {
            Atom atom = uev.Atom!;
            InitializeAtomUI(atom);
            foreach (Atom target in Universe.Atoms)
            {
                atom.AddForce(new(target, Random.Shared.NextDoubleInclusive(), Random.Shared.NextDoubleInclusive(-1.0, 1.0)));
                if (atom != target)
                    if (!target.HasForceWith(atom))
                        target.AddForce(new(atom, Random.Shared.NextDoubleInclusive(), Random.Shared.NextDoubleInclusive(-1.0, 1.0)));
            }
        });

        // Universe Settings Accordion
        Accordion accordionUniverseUI = new() { Title = "Universe Settings" };

        // Reset All Particles Button
        Button btnResetAllParticles = new()
        {
            FlatStyle = FlatStyle.Flat,
            Text = "Reset All Particles Position",
            UseVisualStyleBackColor = true
        };
        btnResetAllParticles.Click += new((s, ev) => Universe.ResetParticles());

        // Wrap CheckBox
        CheckBox chkWrap = new()
        {
            Text = "Wrap",
            Checked = Universe.Wrap,
            UseVisualStyleBackColor = true
        };
        chkWrap.CheckedChanged += new((s, ev) => Universe.Wrap = chkWrap.Checked);

        // Friction Slider
        Slider sldFriction = new() { MinValue = 0.0, MaxValue = 1.0, Value = Universe.Friction, ValueFormat = "F2", Text = "Friction" };
        sldFriction.OnValueChanged += new((s, ev) => Universe.Friction = sldFriction.Value);

        // Add Atom Button
        Label lblAddAtom = new()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Tag = "LAST",
            Text = "+ Add new Atom",
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = SystemColors.Control
        };
        lblAddAtom.Click += new((s, ev) =>
        {
            Color randomKnownColor = GetRandomKnownColor(Settings.ParticlesSystemColorsIncluded, Settings.ParticlesCompoundColorNamesIncluded);
            while (Universe.Atoms.Any(atom => atom.Color == randomKnownColor))
                randomKnownColor = GetRandomKnownColor(Settings.ParticlesSystemColorsIncluded, Settings.ParticlesCompoundColorNamesIncluded);
            Universe.AddAtom(new(randomKnownColor.Name, randomKnownColor, Universe.Width, Universe.Height, 2.5, 150));
        });

        accordionUniverseUI.Controls.AddRange(new Control[] { btnResetAllParticles, chkWrap, sldFriction });
        PanelSettings.Controls.AddRange(new Control[] { accordionUniverseUI, lblAddAtom });
    }
    #endregion

    #region AtomUI
    private void InitializeAtomUI(Atom atom)
    {
        // Atom Settings AccordionUI
        Accordion accordionAtomUI = new() { Title = atom.Name, IsDraggable = true, Atom = atom, ParticleDraw = ParticleDraw, ContextMenuStrip = new() };

        // Atom AccordionUI ContextMenuStrip
        ToolStripMenuItem editAtomMenuItem = new() { Text = "Edit Atom" };
        editAtomMenuItem.Click += new((s, ev) =>
        {
            FrmAtom frmAtom = new(atom) { Owner = this, StartPosition = FormStartPosition.CenterParent }; ;
            DialogResult dialogResult = frmAtom.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Atom atomUpdated = frmAtom.Atom;
                atom.Name = atomUpdated.Name;
                atom.Color = atomUpdated.Color;
                atom.Radius = atomUpdated.Radius;
                atom.UpdateParticles(atomUpdated.Particles.Count);

                foreach (Force force in atomUpdated.Forces)
                    if (atom.HasForceWith(force.Target))
                        atom.GetForceWith(force.Target)!.Attraction = force.Attraction;
                    else
                    {
                        if (atom.Name == force.Target.Name)
                            atom.Forces.SingleOrDefault(f => f.Name == force.Name)!.Attraction = force.Attraction;
                    }
                frmAtom.Atom.Dispose();
                frmAtom.Dispose();
                accordionAtomUI.Invalidate(true);
            }
        });
        ToolStripMenuItem removeAtomMenuItem = new() { Text = "Remove Atom" };
        removeAtomMenuItem.Click += new((s, ev) => Universe.RemoveAtom(atom));
        accordionAtomUI.ContextMenuStrip.Items.AddRange(new[] { editAtomMenuItem, removeAtomMenuItem });

        atom.AtomNameChanged += new((s, ev) => accordionAtomUI.Title = atom.Name);

        Universe.UniverseAtomRemoved += new((s, ev) =>
        {
            Atom atomRemoved = ev.Atom!;
            if (atomRemoved == atom)
                accordionAtomUI.Dispose();
            else
                atom.RemoveForceWith(atomRemoved);
        });

        Slider sldNumber = new() { MinValue = 0.0, MaxValue = 500.0, Value = atom.Particles.Count, Text = "Number" };
        sldNumber.OnValueChanged += new((s, ev) => atom.UpdateParticles(Convert.ToInt32(sldNumber.Value)));
        atom.AtomParticlesChanged += new((s, ev) => sldNumber.Value = atom.Particles.Count);

        Button btnResetParticles = new() { Text = "Reset Particles Position", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter };
        btnResetParticles.Click += new((s, e) => atom.ResetParticles());

        Slider sldRadius = new() { MinValue = 1.0, MaxValue = 10.0, Value = atom.Radius, ValueFormat = "F1", Text = "Radius" };
        sldRadius.OnValueChanged += new((s, ev) => atom.Radius = sldRadius.Value);
        atom.AtomRadiusChanged += new((s, ev) => sldRadius.Value = atom.Radius);

        Accordion accRadiationsUI = new() { Title = "Radiations..." };
        accRadiationsUI.ClientSizeChanged += new((s, ev) => accordionAtomUI.ResizeAccordionControls());

        Accordion accAttractionsUI = new() { Title = "Attractions..." };
        accAttractionsUI.ClientSizeChanged += new((s, ev) => accordionAtomUI.ResizeAccordionControls());

        atom.AtomForceAdded += new((s, ev) =>
        {
            Force forceAdded = ev.Force!;

            Slider sldRadiation = new() { Text = forceAdded.Name, MinValue = 0.0, MaxValue = 1.0, Value = forceAdded.Radiation, ValueFormat = "F2" };
            sldRadiation.OnValueChanged += new((s, ev) => forceAdded.Radiation = sldRadiation.Value);
            accRadiationsUI.Controls.Add(sldRadiation);

            Slider sldAttraction = new() { Text = forceAdded.Name, MinValue = -1.0, MaxValue = 1.0, Value = forceAdded.Attraction, ValueFormat = "F2" };
            sldAttraction.OnValueChanged += new((s, ev) => forceAdded.Attraction = sldAttraction.Value);
            accAttractionsUI.Controls.Add(sldAttraction);

            forceAdded.ForceNameChanged += new((s, ev) =>
            {
                sldRadiation.Text = forceAdded.Name;
                sldAttraction.Text = forceAdded.Name;
            });
            forceAdded.ForceRadiationChanged += new((s, ev) =>
            {
                sldAttraction.Value = forceAdded.Radiation;
            });
            forceAdded.ForceAttractionChanged += new((s, ev) =>
            {
                sldAttraction.Value = forceAdded.Attraction;
            });

            atom.AtomForceRemoved += new((s, ev) =>
            {
                if (ev.Force! == forceAdded)
                {
                    sldRadiation.Dispose();
                    sldAttraction.Dispose();
                }
            });
            atom.AtomForcesCleared += new((s, ev) =>
            {
                sldRadiation.Dispose();
                sldAttraction.Dispose();
            });
        });

        Button btnRemoveAtom = new() { Text = "Remove Atom", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Red, FlatStyle = FlatStyle.Flat, Tag = "LAST" };
        btnRemoveAtom.Click += new((s, e) => Universe.RemoveAtom(atom));

        accordionAtomUI.Controls.AddRange(new Control[]{ sldNumber, btnResetParticles, sldRadius, accRadiationsUI, accAttractionsUI, btnRemoveAtom });

        PanelSettings.Controls.Add(accordionAtomUI);
    }
    #endregion

    public void UniverseDraw(Graphics graphics, int alpha)
    {
        foreach (Atom atom in Universe.Atoms)
        {
            foreach (Particle particle in atom.Particles)
            {
                float x = (float)particle.X - (float)atom.Radius;
                float y = (float)particle.Y - (float)atom.Radius;

                float width = (float)atom.Diameter;
                float height = (float)atom.Diameter;

                ParticleDraw(graphics, alpha, atom.Color, x, y, width, height);
            }
        }
    }
    public void ParticleDraw(Graphics graphics, int alpha, Color color, float x, float y, float width, float height)
    {
        Color fill = Settings.ParticlesContraste ? ControlPaint.Dark(color) : ControlPaint.Light(color);
        Color draw = Settings.ParticlesContraste ? ControlPaint.Light(color) : ControlPaint.Dark(color);

        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

        graphics.FillEllipse(new SolidBrush(Color.FromArgb(alpha, fill)), x, y, width, height);
        if (!Settings.ParticlesBorderless) graphics.DrawEllipse(new Pen(Color.FromArgb(alpha, draw), 1f), x, y, width, height);
    }
    public void UpdateEnvironment(long deltaTime)
    {
        ElapsedTime += deltaTime;
        if (ElapsedTime >= 1000)
        {
            FramesPerSecond = FrameCounter;
            ElapsedTime = 0;
            FrameCounter = 0;
        }
        FrameCounter++;
    }
    public void RenderEnvironment(long deltaTime)
    {
        if (Settings.SimulationAnimated)
        {
            using Graphics graphics = Graphics.FromImage(BackgroundImageBuffer);

            graphics.Clear(Settings.SimulationBackColor);

            for (int i = 1; i <= Settings.SimulationStepsPerFrame; i++)
            {
                int alpha = i * 255 / Settings.SimulationStepsPerFrame;
                Universe.Step();
                UniverseDraw(graphics, alpha);
            }

            BackgroundImage = BackgroundImageBuffer;
            Invalidate();
        }

        ToolStripStatusLabelTotalParticlesValue.Text = Universe.ParticlesCount().ToString();
        ToolStripStatusLabelFpsValue.Text = FramesPerSecond.ToString();
    }
}