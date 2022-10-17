using ParticleLifeSimulation.AviFile;
using ParticleLifeSimulation.Core;
using ParticleLifeSimulation.Properties;
using ParticleLifeSimulation.UserControls;
using static ParticleLifeSimulation.Core.Extensions;

namespace ParticleLifeSimulation;

public partial class FrmUniverse : Form, IFormLoop
{
    #region Properties
    private readonly Settings Settings;
    private readonly Universe Universe;

    private long elapsedTime;
    private int frames;
    private int framesPerSecond;

    private Bitmap Buffer { get; set; }
    private AviWriter? AviRecorder { get; set; }
    private int LastRightDockerWidth { get; set; }

    public bool Border { get; set; }
    public bool Contraste { get; set; }
    public int StepsPerFrame { get; set; }
    public bool Animated { get; set; }
    public Bitmap? BackgroundBitmap { get; set; }
    #endregion

    #region Constructor
    public FrmUniverse()
    {
        InitializeComponent();

        //How to brute force a protected property ;) (Avoid flicking, BackColor = Transparent)
        System.Reflection.PropertyInfo setDoubleBuffered = typeof(Control).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!;
        setDoubleBuffered.SetValue(RightDocker, true, null);
        setDoubleBuffered.SetValue(PanelSettings, true, null);
        setDoubleBuffered.SetValue(TogglePanel, true, null);

        BackgroundBitmap = null;
        Settings = new();
        Buffer = new Bitmap(ClientSize.Width, ClientSize.Height);
        Universe = new(ClientSize.Width, ClientSize.Height);

        InitializeSettings();
        InitializeCanvas();
        InitializeUniverse();
    }
    #endregion

    #region Accordion Control
    private class AccordionProperties
    {
        private string title = "accordion";
        public event EventHandler<EventArgs>? AccordionTitleChanged;
        public bool IsDraggable { get; set; }
        public Atom? Atom { get; set; }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                AccordionTitleChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public void Dispose()
        {
            AccordionTitleChanged = null;
            Atom = null;
        }
    }
    private FlowLayoutPanel CreateAccordion(AccordionProperties accProperties)
    {
        FlowLayoutPanel FlpAccordion = new()
        {
            WrapContents = false,
            BorderStyle = BorderStyle.FixedSingle,
            FlowDirection = FlowDirection.TopDown,
            Tag = accProperties,
            BackColor = SystemColors.Control
        };
        CheckBox ChkTitleBar = new()
        {
            Name = "BtnTitleBar",
            Appearance = Appearance.Button,
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            UseVisualStyleBackColor = true,
            Text = accProperties.Title,
            Tag = "TITLEBAR",
            Margin = new(0),
        };
        ChkTitleBar.FlatAppearance.BorderSize = 0;

        void ResizeAccordionControls()
        {
            Size iconSize = new(ChkTitleBar.ClientSize.Height / 4 * 3, ChkTitleBar.ClientSize.Height / 4 * 3);
            IEnumerable<Control> resetChildIndexes = Enumerable.Empty<Control>();

            //width
            foreach (Control control in FlpAccordion.Controls)
                control.Width = FlpAccordion.ClientSize.Width - control.Margin.Right - control.Margin.Left;

            //height
            if (ChkTitleBar.Checked)
            {
                Bitmap bitmap = new(Resources.arrow_bottom, iconSize);
                ChkTitleBar.Image = bitmap;
                int height = FlpAccordion.Margin.Top;
                foreach (Control control in FlpAccordion.Controls)
                {
                    height += control.Height + control.Margin.Top + control.Margin.Bottom;
                    if (control.Tag is string) resetChildIndexes = resetChildIndexes.Append(control);
                }
                FlpAccordion.Height = height;
            }
            else
            {
                Bitmap bitmap = new(Resources.arrow_right, iconSize);
                ChkTitleBar.Image = bitmap;
                FlpAccordion.Height = ChkTitleBar.ClientSize.Height + 2;
            }

            //position
            foreach (Control control in resetChildIndexes)
            {
                switch ((string)control.Tag)
                {
                    case "TITLEBAR":
                        FlpAccordion.Controls.SetChildIndex(control, 0);
                        break;
                    case "LAST":
                        FlpAccordion.Controls.SetChildIndex(control, FlpAccordion.Controls.Count - 1);
                        break;
                }
            }
        };

        FlpAccordion.Disposed += new((s, ev) => accProperties.Dispose());
        if (accProperties.IsDraggable) ChkTitleBar.MouseDown += new((s, ev) =>
        {
            int height = ChkTitleBar.ClientSize.Height;
            int width = height;
            RectangleF rectangleF = new(ChkTitleBar.ClientSize.Width - width - ((ChkTitleBar.ClientSize.Height - height) / 2f), (ChkTitleBar.ClientSize.Height - height) / 2f, width, height);
            if (rectangleF.Contains(ev.Location)) FlpAccordion.DoDragDrop(FlpAccordion, DragDropEffects.Move);
        });
        if (accProperties.Atom is not null) ChkTitleBar.Paint += new((s, ev) =>
        {
            Bitmap bitmap = new(ChkTitleBar.ClientSize.Height, ChkTitleBar.ClientSize.Height);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                gfx.Clear(Color.Transparent);
                float x = bitmap.Width * 0.25f;
                float y = bitmap.Height * 0.25f;
                float width = bitmap.Width / 2.0f;
                float height = bitmap.Height / 2.0f;
                Particle_Draw(gfx, 255, accProperties.Atom.Color, x, y, width, height);
            }
            Graphics g = ev.Graphics;
            g.DrawImage(bitmap, ChkTitleBar.ClientSize.Width - ChkTitleBar.ClientSize.Height, 0, ChkTitleBar.ClientSize.Height, ChkTitleBar.ClientSize.Height);
        });
        accProperties.AccordionTitleChanged += new((s, ev) => ChkTitleBar.Text = accProperties.Title);

        ChkTitleBar.CheckedChanged += new((s, ev) => ResizeAccordionControls());
        FlpAccordion.ControlAdded += new((s, ev) => ResizeAccordionControls());
        FlpAccordion.ControlRemoved += new((s, ev) => ResizeAccordionControls());
        FlpAccordion.ClientSizeChanged += new((s, ev) => ResizeAccordionControls());

        FlpAccordion.Controls.Add(ChkTitleBar);

        return FlpAccordion;
    }
    #endregion

    #region Settings
    private void InitializeSettings()
    {
        void ResizePanelSettingsControls()
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
        PanelSettings.DragOver += new((s, ev) =>
        {
            if (ev.Data!.GetDataPresent(typeof(FlowLayoutPanel)))
            {
                Point mousePosition = PanelSettings.PointToClient(new Point(ev.X, ev.Y));
                FlowLayoutPanel accSource = (FlowLayoutPanel)ev.Data!.GetData(typeof(FlowLayoutPanel));
                if (PanelSettings.GetChildAtPoint(mousePosition) is FlowLayoutPanel accTarget && accTarget != accSource && ((AccordionProperties)accTarget.Tag).IsDraggable)
                    ev.Effect = DragDropEffects.Move;
                else
                    ev.Effect = DragDropEffects.None;
            }
        });
        PanelSettings.DragDrop += new((s, ev) =>
        {
            FlowLayoutPanel accSource = (FlowLayoutPanel)ev.Data!.GetData(typeof(FlowLayoutPanel));
            Point mousePosition = PanelSettings.PointToClient(new Point(ev.X, ev.Y));
            FlowLayoutPanel? accTarget = PanelSettings.GetChildAtPoint(mousePosition) as FlowLayoutPanel;
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
        PanelSettings.ControlAdded += new((s, ev) => ResizePanelSettingsControls());
        PanelSettings.ClientSizeChanged += new((s, ev) => ResizePanelSettingsControls());
    }
    #endregion

    #region Canvas
    private void InitializeCanvas()
    {
        ClientSizeChanged += new((s, ev) =>
        {
            Buffer = new Bitmap(Buffer, ClientSize.Width, ClientSize.Height);
            Universe.Size = ClientSize;
        });

        // Form Interval/Animated/BackColor/BackgroundBitmap Set/Bind
        DataBindings.Add(nameof(BackColor), Settings, "CanvasBackColor", true, DataSourceUpdateMode.OnPropertyChanged);
        DataBindings.Add(nameof(StepsPerFrame), Settings, "CanvasStepsPerFrame", true, DataSourceUpdateMode.OnPropertyChanged);
        DataBindings.Add(nameof(Animated), Settings, "CanvasAnimated", true, DataSourceUpdateMode.OnPropertyChanged);
        DataBindings.Add(nameof(Contraste), Settings, "ParticlesContraste", true, DataSourceUpdateMode.OnPropertyChanged);
        DataBindings.Add(nameof(Border), Settings, "ParticlesBorder", true, DataSourceUpdateMode.OnPropertyChanged);
        Binding bndCanvasBackgroundBitmap = new(nameof(BackgroundBitmap), Settings, "CanvasBackgroundBitmap", true, DataSourceUpdateMode.OnPropertyChanged);
        bndCanvasBackgroundBitmap.Format += new((s, ev) => ev.Value = !string.IsNullOrWhiteSpace(ev.Value as string) && File.Exists((string)ev.Value) ? Bitmap.FromFile((string)ev.Value) : null);
        bndCanvasBackgroundBitmap.Parse += new((s, ev) => ev.Value = ev.Value is not null ? ((Bitmap)ev.Value).ToString() : null);
        DataBindings.Add(bndCanvasBackgroundBitmap);

        // Form ContextMenuStrip/ToolStripMenuItem Set/Bind/Events
        ContextMenuStrip = new();
        ToolStripMenuItem toolStripCanvasSettings = new() { Text = "Canvas Settings" };
        toolStripCanvasSettings.Click += (s, ev) =>
        {
            FrmCanvasSettings frmCanvasSettings = new(Settings) { Owner = this, StartPosition = FormStartPosition.CenterParent };
            DialogResult dialogResult = frmCanvasSettings.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
                Settings.Save();
            else
                Settings.Reload();
        };
        ToolStripSeparator toolStripSeparator1 = new();
        ToolStripMenuItem toolStripToggleRecord = new() { Text = "Start Recording" };
        toolStripToggleRecord.Click += new((s, ev) =>
        {
            if (AviRecorder is null)
            {
                SaveFileDialog saveAVI = new()
                {
                    Filter = "AVI Files (*.avi)|*.avi"
                };
                if (saveAVI.ShowDialog() == DialogResult.OK)
                {
                    AviRecorder = new AviWriter();
                    Buffer = AviRecorder.Open(saveAVI.FileName, (uint)(23), Buffer.Width, Buffer.Height);
                    toolStripToggleRecord.Text = "Stop Recording";
                }
            }
            else
            {
                AviRecorder.Close();
                AviRecorder = null;
                toolStripToggleRecord.Text = "Start Recording";
            }
        });

        // Canvas Accordion Set
        FlowLayoutPanel accCanvasSettings = CreateAccordion(new() { Title = "Canvas Settings" });

        // Canvas Contraste Set/Bind/Events
        CheckBox chkContraste = new() { Text = "Contraste" };
        chkContraste.DataBindings.Add("Checked", this, "Contraste", true, DataSourceUpdateMode.OnPropertyChanged);
        ToolStripMenuItem toolStripChkContraste = new() { Text = "Contraste", CheckOnClick = true, Checked = Contraste };
        toolStripChkContraste.CheckedChanged += new((s, ev) => { chkContraste.Checked = toolStripChkContraste.Checked; Settings.Save(); PanelSettings.Invalidate(true); });
        chkContraste.CheckedChanged += new((s, ev) => { toolStripChkContraste.Checked = chkContraste.Checked; Settings.Save(); PanelSettings.Invalidate(true); });
        accCanvasSettings.Controls.Add(chkContraste);

        // Canvas Border Set/Bind/Events
        CheckBox chkBorder = new() { Text = "Border" };
        chkBorder.DataBindings.Add("Checked", this, "Border", true, DataSourceUpdateMode.OnPropertyChanged);
        ToolStripMenuItem toolStripChkBorder = new() { Text = "Border", CheckOnClick = true, Checked = Border };
        toolStripChkBorder.CheckedChanged += new((s, ev) => { chkBorder.Checked = toolStripChkBorder.Checked; Settings.Save(); PanelSettings.Invalidate(true); });
        chkBorder.CheckedChanged += new((s, ev) => { toolStripChkBorder.Checked = chkBorder.Checked; Settings.Save(); PanelSettings.Invalidate(true); });
        accCanvasSettings.Controls.Add(chkBorder);

        PanelSettings.Controls.Add(accCanvasSettings);

        ContextMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            toolStripCanvasSettings,
            toolStripChkContraste,
            toolStripChkBorder,
            toolStripSeparator1,
            toolStripToggleRecord
        });
    }
    #endregion

    #region Universe
    private void InitializeUniverse()
    {
        Universe.UniverseAtomAdded += new(Universe_AtomAdded);

        // Universe Settings Accordion
        FlowLayoutPanel accUniverseSettings = CreateAccordion(new() { Title = "Universe Settings" });

        // Reset All Particles Button
        Button BtnResetAllParticles = new()
        {
            FlatStyle = FlatStyle.Flat,
            Text = "Reset All Particles Position",
            UseVisualStyleBackColor = true
        };
        BtnResetAllParticles.Click += new((s, e) => Universe.ResetAllParticles());
        accUniverseSettings.Controls.Add(BtnResetAllParticles);

        //ToolStripSeparator toolStripSeparator = new();
        //Canvas.ContextMenuStrip.Items.Add(toolStripSeparator);

        //ToolStripMenuItem toolStripUniverseSettings = new() { Text = "Universe Settings" };
        //toolStripUniverseSettings.Click += (s, ev) =>
        //{
        //    FrmCanvasSettings frmCanvasSettings = new(settings) { Owner = this, StartPosition = FormStartPosition.CenterParent };
        //    DialogResult dialogResult = frmCanvasSettings.ShowDialog(this);
        //    if (dialogResult == DialogResult.OK)
        //        settings.Save();
        //    else
        //        settings.Reload();
        //};
        //Canvas.ContextMenuStrip.Items.Add(toolStripUniverseSettings);

        //ToolStripMenuItem toolStripResetAllParticles = new() { Text = "Reset All Particles" };
        //toolStripResetAllParticles.Click += new((s, e) => universe.ResetAllParticles());
        //Canvas.ContextMenuStrip.Items.Add(toolStripResetAllParticles);

        // Wrap CheckBox
        CheckBox ChkWrap = new()
        {
            Text = "Wrap",
            Checked = Universe.Wrap,
            UseVisualStyleBackColor = true
        };
        ChkWrap.CheckedChanged += new((s, e) => Universe.Wrap = ChkWrap.Checked);
        accUniverseSettings.Controls.Add(ChkWrap);

        //ToolStripMenuItem toolStripChkWrap = new() { Text = "Wrap", CheckOnClick = true, Checked = universe.Wrap };
        //toolStripChkWrap.CheckedChanged += new((s, ev) => ChkWrap.Checked = toolStripChkWrap.Checked);
        //ChkWrap.CheckedChanged += new((s, ev) => toolStripChkWrap.Checked = ChkWrap.Checked);
        //Canvas.ContextMenuStrip.Items.Add(toolStripChkWrap);

        PanelSettings.Controls.Add(accUniverseSettings);

        // Add Atom Button
        void AddAtom()
        {
            //FrmAtom frmAtom = new(universe) { Owner = this, StartPosition = FormStartPosition.CenterParent };
            //DialogResult dialogResult = frmAtom.ShowDialog(this);
            //if (dialogResult == DialogResult.OK) universe.AddAtom(frmAtom.Atom);
            Color randomKnownColor = GetRandomKnownColor();
            while (Universe.Atoms.Any(atom => atom.Color == randomKnownColor))
                randomKnownColor = GetRandomKnownColor();
            Universe.AddAtom(new(randomKnownColor.Name, randomKnownColor, Universe.Width, Universe.Height, 2.5, 150));
        }
        Label lblAddAtom = new()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Tag = "LAST",
            Text = "+ Add new Atom",
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = SystemColors.Control
        };
        lblAddAtom.Click += new((s, ev) => AddAtom());

        PanelSettings.Controls.Add(lblAddAtom);

        //ToolStripMenuItem toolStripAtoms = new() { Text = "Atoms", Name = "Atoms" };
        //Canvas.ContextMenuStrip.Items.Add(toolStripAtoms);

        //ToolStripSeparator toolStripSeparator2 = new() { Visible = universe.Atoms.Count > 0, Name = "Separator" };
        //toolStripAtoms.DropDownItems.Add(toolStripSeparator2);

        //ToolStripMenuItem toolStripAddAtom = new() { Text = "+ Add Atom ..." };
        //toolStripAddAtom.Click += new((s, ev) => AddAtom());
        //toolStripAtoms.DropDownItems.Add(toolStripAddAtom);
    }
    private void Universe_Draw(Graphics graphics, int alpha)
    {
        foreach (Atom atom in Universe.Atoms)
        {
            foreach (Particle particle in atom.Particles)
            {
                float x = (float)particle.X - (float)atom.Radius;
                float y = (float)particle.Y - (float)atom.Radius;

                float width = (float)atom.Diameter;
                float height = (float)atom.Diameter;

                Particle_Draw(graphics, alpha, atom.Color, x, y, width, height);
            }
        }
    }
    public void Particle_Draw(Graphics graphics, int alpha, Color color, float x, float y, float width, float height)
    {
        Color fill, draw;
        if (Contraste)
        {
            fill = ControlPaint.Dark(color);
            draw = ControlPaint.Light(color);
        }
        else
        {
            fill = ControlPaint.Light(color);
            draw = ControlPaint.Dark(color);
        }
        //*****************//
        // Fancy CPU Eater //
        //*****************//

        //if (universe.Wrap)
        //{
        //    if (x < atom.Diameter)
        //    {
        //        graphics.FillEllipse(new SolidBrush(fill), x + (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
        //        graphics.DrawEllipse(new Pen(draw, 0.1f), x + (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
        //    }
        //    if (y < atom.Diameter)
        //    {
        //        graphics.FillEllipse(new SolidBrush(fill), x, y + (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
        //        graphics.DrawEllipse(new Pen(draw, 0.1f), x, y + (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
        //    }
        //    if (x > universe.Width - atom.Diameter)
        //    {
        //        graphics.FillEllipse(new SolidBrush(fill), x - (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
        //        graphics.DrawEllipse(new Pen(draw, 0.1f), x - (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
        //    }
        //    if (y < universe.Height - atom.Diameter)
        //    {
        //        graphics.FillEllipse(new SolidBrush(fill), x, y - (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
        //        graphics.DrawEllipse(new Pen(draw, 0.1f), x, y - (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
        //    }
        //}
        graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
        graphics.FillEllipse(new SolidBrush(Color.FromArgb(alpha, fill)), x, y, width, height);
        if (Border) graphics.DrawEllipse(new Pen(Color.FromArgb(alpha, draw), 1f), x, y, width, height);
    }
    private void Universe_AtomAdded(object? sender, UniverseEventArgs e)
    {
        Atom AtomAdded = e.Atom!;

        FlowLayoutPanel AccordionAtom = CreateAccordion(new()
        {
            Title = AtomAdded.Name,
            IsDraggable = true,
            Atom = AtomAdded
        });

        ContextMenuStrip AccordionMenuStrip = new();
        ToolStripMenuItem EditAtomMenuItem = new() { Text = "Edit Atom" };
        EditAtomMenuItem.Click += new((s, ev) =>
        {
            FrmAtom frmAtom = new(AtomAdded) { Owner = this, StartPosition = FormStartPosition.CenterParent }; ;
            DialogResult dialogResult = frmAtom.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                Atom atomUpdated = frmAtom.Atom;
                AtomAdded.Name = atomUpdated.Name;
                AtomAdded.Color = atomUpdated.Color;
                AtomAdded.Radius = atomUpdated.Radius;
                AtomAdded.UpdateParticles(atomUpdated.Particles.Count);

                foreach (Force force in atomUpdated.Forces)
                    if (AtomAdded.HasForceWith(force.AtomTarget))
                        AtomAdded.GetForceWith(force.AtomTarget)!.Value = force.Value;
                    else
                    {
                        if (AtomAdded.Name == force.AtomTarget.Name)
                            AtomAdded.Forces.SingleOrDefault(f => f.Name == force.Name)!.Value = force.Value;
                    }
                frmAtom.Atom.Dispose();
                frmAtom.Dispose();
                AccordionAtom.Invalidate(true);

            }
        });
        ToolStripMenuItem RemoveAtomMenuItem = new() { Text = "Remove Atom" };
        RemoveAtomMenuItem.Click += new((s, ev) => Universe.RemoveAtom(AtomAdded));
        AccordionMenuStrip.Items.AddRange(new ToolStripItem[]
        {
            EditAtomMenuItem,
            RemoveAtomMenuItem
        });
        AccordionAtom.ContextMenuStrip = AccordionMenuStrip;

        AtomAdded.AtomNameChanged += new((s, ev) => ((AccordionProperties)AccordionAtom.Tag).Title = AtomAdded.Name);

        Universe.UniverseAtomRemoved += new((s, ev) =>
        {
            Atom atomRemoved = ev.Atom!;
            if (atomRemoved == AtomAdded)
                AccordionAtom.Dispose();
            else
                AtomAdded.RemoveForceWith(atomRemoved);
        });

        Slider sldNumber = new() { MinValue = 0.0, MaxValue = 500.0, Value = AtomAdded.Particles.Count, Text = "Number" };
        sldNumber.OnValueChanged += new((s, ev) => AtomAdded.UpdateParticles(Convert.ToInt32(sldNumber.Value)));
        AtomAdded.AtomParticlesChanged += new((s, ev) => sldNumber.Value = AtomAdded.Particles.Count);
        AccordionAtom.Controls.Add(sldNumber);

        Button btnResetParticles = new() { Text = "Reset Particles Position", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter };
        btnResetParticles.Click += new((s, e) => AtomAdded.ResetParticles());
        AccordionAtom.Controls.Add(btnResetParticles);

        Slider sldRadius = new() { MinValue = 1.0, MaxValue = 10.0, Value = AtomAdded.Radius, ValueFormat = "F1", Text = "Radius" };
        sldRadius.OnValueChanged += new((s, ev) => AtomAdded.Radius = sldRadius.Value);
        AtomAdded.AtomRadiusChanged += new((s, ev) => sldRadius.Value = AtomAdded.Radius);
        AccordionAtom.Controls.Add(sldRadius);

        Label lblForces = new() { Text = "Forces :", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft };
        AccordionAtom.Controls.Add(lblForces);

        AtomAdded.AtomForceAdded += new((s, ev) =>
        {
            Force forceAdded = ev.Force!;
            Slider sldForce = new() { Text = forceAdded.Name, MinValue = -1.0, MaxValue = 1.0, Value = forceAdded.Value, ValueFormat = "F2" };
            sldForce.OnValueChanged += new((s, ev) => forceAdded.Value = sldForce.Value);
            AccordionAtom.Controls.Add(sldForce);
            forceAdded.ForceNameChanged += new((s, ev) => sldForce.Text = forceAdded.Name);
            forceAdded.ForceValueChanged += new((s, ev) => sldForce.Value = forceAdded.Value);
            AtomAdded.AtomForceRemoved += new((s, ev) =>
            {
                if (ev.Force! == forceAdded)
                    sldForce.Dispose();
            });
            AtomAdded.AtomForcesCleared += new((s, ev) => sldForce.Dispose());
        });
        foreach (Atom atomTarget in Universe.Atoms)
        {
            AtomAdded.AddForce(new(atomTarget));
            if (AtomAdded != atomTarget)
                if (!atomTarget.HasForceWith(AtomAdded))
                    atomTarget.AddForce(new(AtomAdded));
        }

        Button btnRemoveAtom = new() { Text = "Remove Atom", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Red, FlatStyle = FlatStyle.Flat, Tag = "LAST" };
        btnRemoveAtom.Click += new((s, e) => Universe.RemoveAtom(AtomAdded));
        AccordionAtom.Controls.Add(btnRemoveAtom);

        PanelSettings.Controls.Add(AccordionAtom);
    }
    #endregion

    public void UpdateEnvironment(long deltaTime)
    {
        elapsedTime += deltaTime;
        if (elapsedTime >= 1000)
        {
            framesPerSecond = frames;
            elapsedTime = 0;
            frames = 0;
        }
        frames++;
        ToolStripStatsTotalParticles.Text = Universe.ParticlesCount().ToString();
        ToolStripStatsFPS.Text = framesPerSecond.ToString();
    }
    public void RenderEnvironment(long deltaTime)
    {
        if (Animated)
        {
            using (Graphics gfx = Graphics.FromImage(Buffer))
            {
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                gfx.Clear(BackColor);
                if (BackgroundBitmap is not null) gfx.DrawImage(BackgroundBitmap, 0, 0, Buffer.Width, Buffer.Height);
                for (int i = 1; i <= StepsPerFrame; i++)
                {
                    int alpha = i * 255 / StepsPerFrame;
                    Universe.Step();
                    Universe_Draw(gfx, alpha);
                }
            }
            BackgroundImage = Buffer;
            if (AviRecorder is not null) AviRecorder.AddFrame();
            Invalidate();
        }
    }
}