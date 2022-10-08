using CustomControls;
using ParticleLifeSimulation.Core;
using ParticleLifeSimulation.Properties;
using static ParticleLifeSimulation.Core.Extensions;

namespace ParticleLifeSimulation;

public partial class FrmUniverse : Form
{
    #region Properties
    private readonly Settings settings;
    private readonly Universe universe;
    private readonly ContextMenuStrip contextMenuStrip;
    public bool Contraste { get; set; }
    #endregion

    #region Constructor
    public FrmUniverse()
    {
        InitializeComponent();

        settings = new();
        contextMenuStrip = new();
        universe = new(Canvas.Width, Canvas.Height);

        InitializeSettings();
        InitializeCanvas();
        InitializeUniverse();
    }
    #endregion

    #region Settings
    private class AccordionProperties
    {
        public event EventHandler<EventArgs>? AccordionTitleChanged;
        private string title = "accordion";
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
    }
    private FlowLayoutPanel BuildAccordion(AccordionProperties accordionProperties)
    {
        FlowLayoutPanel Accordion = new()
        {
            WrapContents = false,
            BorderStyle = BorderStyle.FixedSingle,
            FlowDirection = FlowDirection.TopDown,
            Tag = accordionProperties
        };

        CheckBox BtnTitleBar = new()
        {
            Name = "BtnTitleBar",
            Appearance = Appearance.Button,
            FlatStyle = FlatStyle.Flat,
            TextAlign = ContentAlignment.MiddleLeft,
            TextImageRelation = TextImageRelation.ImageBeforeText,
            ImageAlign = ContentAlignment.MiddleLeft,
            UseVisualStyleBackColor = true,
            Text = $"{accordionProperties.Title}",
            Tag = "TITLEBAR",
            Margin = new(0),
        };
        BtnTitleBar.FlatAppearance.BorderSize = 0;
        BtnTitleBar.CheckedChanged += new((s, ev) => ResizeAccordionControls());
        if (accordionProperties.IsDraggable) BtnTitleBar.MouseDown += new((s, ev) =>
        {
            int height = BtnTitleBar.ClientSize.Height * 4 / 5;
            int width = height;
            RectangleF rectangleF = new(BtnTitleBar.ClientSize.Width - width - ((BtnTitleBar.ClientSize.Height - height) / 2f), (BtnTitleBar.ClientSize.Height - height) / 2f, width, height);
            if (rectangleF.Contains(ev.Location)) Accordion.DoDragDrop(Accordion, DragDropEffects.Move);
        });
        if (accordionProperties.Atom != null) BtnTitleBar.Paint += new((s, ev) =>
        {
            int height = BtnTitleBar.ClientSize.Height;
            int width = height;

            Bitmap bitmap = new(width, height);
            using (Graphics gfx = Graphics.FromImage(bitmap))
            {
                gfx.Clear(Color.Transparent);
                Color fill, draw;
                if (Contraste)
                {
                    fill = ControlPaint.Light(accordionProperties.Atom.Color);
                    draw = ControlPaint.Dark(accordionProperties.Atom.Color);
                }
                else
                {
                    fill = ControlPaint.Dark(accordionProperties.Atom.Color);
                    draw = ControlPaint.Light(accordionProperties.Atom.Color);
                }
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                gfx.FillEllipse(new SolidBrush(fill), bitmap.Width * 0.25f, bitmap.Height * 0.25f, bitmap.Width / 2.0f, bitmap.Height / 2.0f);
                gfx.DrawEllipse(new Pen(draw, 2f), bitmap.Width * 0.25f, bitmap.Height * 0.25f, bitmap.Width / 2.0f, bitmap.Height / 2.0f);
            }
            Graphics g = ev.Graphics;
            g.DrawImage(bitmap, BtnTitleBar.ClientSize.Width - width - ((BtnTitleBar.ClientSize.Height - height) / 2f), (BtnTitleBar.ClientSize.Height - height) / 2f, width, height);
        });
        accordionProperties.AccordionTitleChanged += new((s, ev) => BtnTitleBar.Text = accordionProperties.Title);

        void ResizeAccordionControls()
        {
            Size size = new(BtnTitleBar.ClientSize.Height / 4 * 3, BtnTitleBar.ClientSize.Height / 4 * 3);
            IEnumerable<Control> resetChildIndexes = Enumerable.Empty<Control>();

            //width
            foreach (Control control in Accordion.Controls)
                control.Width = control.Parent.ClientSize.Width - control.Margin.Right - control.Margin.Left;

            //height
            if (BtnTitleBar.Checked)
            {
                Bitmap bitmap = new(Resources.arrow_bottom, size);
                BtnTitleBar.Image = bitmap;
                int height = Accordion.Margin.Top;
                foreach (Control control in Accordion.Controls)
                {
                    height += control.Height + control.Margin.Top + control.Margin.Bottom;
                    if (control.Tag is string) resetChildIndexes = resetChildIndexes.Append(control);
                }
                Accordion.Height = height;
            }
            else
            {
                Bitmap bitmap = new(Resources.arrow_right, size);
                BtnTitleBar.Image = bitmap;
                Accordion.Height = BtnTitleBar.ClientSize.Height + 2;
            }

            //position
            foreach (Control control in resetChildIndexes)
            {
                switch ((string)control.Tag)
                {
                    case "TITLEBAR":
                        Accordion.Controls.SetChildIndex(control, 0);
                        break;
                    case "LAST":
                        Accordion.Controls.SetChildIndex(control, Accordion.Controls.Count - 1);
                        break;
                }
            }
        };

        Accordion.Controls.Add(BtnTitleBar);
        Accordion.ControlAdded += new((s, ev) => ResizeAccordionControls());
        Accordion.ControlRemoved += new((s, ev) => ResizeAccordionControls());
        Accordion.ClientSizeChanged += new((s, ev) => ResizeAccordionControls());

        return Accordion;
    }
    private void InitializeSettings()
    {
        void ResizePanelControls()
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
        PanelSettings.ControlAdded += new((s, ev) => ResizePanelControls());
        PanelSettings.ClientSizeChanged += new((s, ev) => ResizePanelControls());
    }
    #endregion

    #region Canvas
    private void InitializeCanvas()
    {
        // Canvas OnPainting Event
        Canvas.OnPainting += new((s, pev) =>
        {
            pev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pev.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            universe.Step();
            Universe_Draw(pev.Graphics);
        });

        // Canvas Interval/Animated/BackColor Set/Bind
        Canvas.DataBindings.Add("BackColor", settings, "CanvasBackColor", true, DataSourceUpdateMode.OnPropertyChanged);
        Canvas.DataBindings.Add("Interval", settings, "CanvasInterval", true, DataSourceUpdateMode.OnPropertyChanged);
        Canvas.DataBindings.Add("Animated", settings, "CanvasAnimated", true, DataSourceUpdateMode.OnPropertyChanged);
        Canvas.ClientSizeChanged += new((s, ev) => universe.Size = Canvas.Size);

        // Canvas BackgroundImage Set/Bind/Converter
        Canvas.BackgroundImageLayout = ImageLayout.Stretch;
        Binding bndCanvasBackgroundImage = new("BackgroundImage", settings, "CanvasBackgroundImage", true, DataSourceUpdateMode.OnPropertyChanged);
        bndCanvasBackgroundImage.Format += new((s, ev) => ev.Value = !string.IsNullOrWhiteSpace((string?)ev.Value) && File.Exists((string)ev.Value) ? Image.FromFile((string)ev.Value) : null);
        bndCanvasBackgroundImage.Parse += new((s, ev) => ev.Value = ((Image)ev.Value!).ToString());
        Canvas.DataBindings.Add(bndCanvasBackgroundImage);

        // Canvas Accordion Set
        FlowLayoutPanel accCanvasSettings = BuildAccordion(new() { Title = "Canvas Settings" });

        // Canvas Contraste Set/Bind/Events
        CheckBox chkContraste = new() { Text = "Contraste" };
        chkContraste.DataBindings.Add("Checked", this, "Contraste", true, DataSourceUpdateMode.OnPropertyChanged);
        ToolStripMenuItem CanvasSettingsChkContraste = new() { Text = "Contraste", CheckOnClick = true, Checked = Contraste };
        CanvasSettingsChkContraste.CheckedChanged += new((s, ev) => { chkContraste.Checked = CanvasSettingsChkContraste.Checked; PanelSettings.Invalidate(true); });
        chkContraste.CheckedChanged += new((s, ev) => { CanvasSettingsChkContraste.Checked = chkContraste.Checked; PanelSettings.Invalidate(true); });
        accCanvasSettings.Controls.Add(chkContraste);

        PanelSettings.Controls.Add(accCanvasSettings);

        // Canvas ContextMenuStrip/ToolStripMenuItem Set/Bind/Events
        Canvas.ContextMenuStrip = contextMenuStrip;
        ToolStripMenuItem CanvasSettingsMenuItem = new() { Text = "Canvas Settings" };
        CanvasSettingsMenuItem.Click += (s, ev) =>
        {
            FrmCanvasSettings frmCanvasSettings = new(settings) { Owner = this, StartPosition = FormStartPosition.CenterParent };
            DialogResult dialogResult = frmCanvasSettings.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
                settings.Save();
            else
                settings.Reload();
        };
        Canvas.ContextMenuStrip.Items.Add(CanvasSettingsMenuItem);
        Canvas.ContextMenuStrip.Items.Add(CanvasSettingsChkContraste);
    }
    #endregion

    #region Universe
    private void InitializeUniverse()
    {
        universe.UniverseAtomAdded += new(Universe_AtomAdded);

        // Universe Settings Accordion
        FlowLayoutPanel accUniverseSettings = BuildAccordion(new() { Title = "Universe Settings" });

        // Reset All Particles Button
        Button BtnResetAllParticles = new()
        {
            FlatStyle = FlatStyle.Flat,
            Text = "Reset All Particles Position",
            UseVisualStyleBackColor = true
        };
        BtnResetAllParticles.Click += new((s, e) => universe.ResetAllParticles());
        accUniverseSettings.Controls.Add(BtnResetAllParticles);

        // Wrap CheckBox
        CheckBox ChkWrap = new()
        {
            Text = "Wrap",
            Checked = universe.Wrap,
            UseVisualStyleBackColor = true
        };
        ChkWrap.CheckedChanged += new((s, e) => universe.Wrap = ChkWrap.Checked);
        accUniverseSettings.Controls.Add(ChkWrap);

        PanelSettings.Controls.Add(accUniverseSettings);

        // Add Atom Button
        Label lblAddAtom = new()
        {
            BorderStyle = BorderStyle.FixedSingle,
            Tag = "LAST",
            Text = "+ Add new Atom",
            TextAlign = ContentAlignment.MiddleCenter
        };
        lblAddAtom.Click += new((s, ev) =>
        {
            Color randomKnownColor = GetRandomKnownColor();
            while (universe.Atoms.Any(atom => atom.Color == randomKnownColor))
                randomKnownColor = GetRandomKnownColor();
            universe.AddAtom(new(randomKnownColor.Name, randomKnownColor, universe.Width, universe.Height, 150));
        });

        PanelSettings.Controls.Add(lblAddAtom);
    }
    private void Universe_Draw(Graphics graphics)
    {
        foreach (Atom atom in universe.Atoms)
        {
            foreach (Particle particle in atom.Particles)
            {
                Color fill, draw;
                if (Contraste)
                {
                    fill = ControlPaint.Light(atom.Color);
                    draw = ControlPaint.Dark(atom.Color);
                }
                else
                {
                    fill = ControlPaint.Dark(atom.Color);
                    draw = ControlPaint.Light(atom.Color);
                }

                float x = (float)particle.X - (float)atom.Radius;
                float y = (float)particle.Y - (float)atom.Radius;

                if (universe.Wrap)
                {
                    if (x < atom.Diameter)
                    {
                        graphics.FillEllipse(new SolidBrush(fill), x + (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
                        graphics.DrawEllipse(new Pen(draw, 0.1f), x + (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
                    }
                    if (y < atom.Diameter)
                    {
                        graphics.FillEllipse(new SolidBrush(fill), x, y + (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
                        graphics.DrawEllipse(new Pen(draw, 0.1f), x, y + (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
                    }
                    if (x > universe.Width - atom.Diameter)
                    {
                        graphics.FillEllipse(new SolidBrush(fill), x - (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
                        graphics.DrawEllipse(new Pen(draw, 0.1f), x - (float)universe.Width, y, (float)atom.Diameter, (float)atom.Diameter);
                    }
                    if (y < universe.Height - atom.Diameter)
                    {
                        graphics.FillEllipse(new SolidBrush(fill), x, y - (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
                        graphics.DrawEllipse(new Pen(draw, 0.1f), x, y - (float)universe.Height, (float)atom.Diameter, (float)atom.Diameter);
                    }
                }
                graphics.FillEllipse(new SolidBrush(fill), x, y, (float)atom.Diameter, (float)atom.Diameter);
                graphics.DrawEllipse(new Pen(draw, 0.1f), x, y, (float)atom.Diameter, (float)atom.Diameter);
            }

        }
    }
    private void Universe_AtomAdded(object? sender, UniverseEventArgs e)
    {
        Atom atomAdded = e.Atom!;
        //ContextMenuStrip AccordionMenuStrip = new();
        //ToolStripMenuItem RemoveAtomMenuItem = new() { Text = "Remove Atom" };
        //RemoveAtomMenuItem.Click += new((s, ev) => Universe.RemoveAtom(atom));
        //AccordionMenuStrip.Items.AddRange(new ToolStripItem[] { RemoveAtomMenuItem });
        FlowLayoutPanel accAtom = BuildAccordion(new() { Title = $"{atomAdded.Name}", IsDraggable = true, Atom = atomAdded });

        universe.UniverseAtomRemoved += new((s, ev) =>
        {
            Atom atomRemoved = ev.Atom!;
            if (atomRemoved == atomAdded) accAtom.Dispose();
            else atomAdded.RemoveForceWith(atomRemoved);
        });

        Slider sldNumber = new() { MinValue = 0.0, MaxValue = 500.0, Value = atomAdded.Particles.Count, Text = "Number" };
        sldNumber.OnValueChanged += new((s, ev) => atomAdded.UpdateParticles(Convert.ToInt32(sldNumber.Value)));
        accAtom.Controls.Add(sldNumber);

        Button btnResetParticles = new() { Text = "Reset Particles Position", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter };
        btnResetParticles.Click += new((s, e) => atomAdded.ResetParticles());
        accAtom.Controls.Add(btnResetParticles);

        Slider sldRadius = new() { MinValue = 1.0, MaxValue = 10.0, Value = atomAdded.Radius, ValueFormat = "F1", Text = "Radius" };
        sldRadius.OnValueChanged += new((s, ev) => atomAdded.Radius = sldRadius.Value);
        accAtom.Controls.Add(sldRadius);

        Label lblForces = new() { Text = "Forces :", AutoSize = true, TextAlign = ContentAlignment.MiddleLeft };
        accAtom.Controls.Add(lblForces);

        foreach (Force force in atomAdded.Forces)    // add forces that already exist in that atom, if needed
        {
            Slider sldForce = new() { Text = $"{force.Name}", MinValue = -1.0, MaxValue = 1.0, Value = force.Value, ValueFormat = "F2" };
            sldForce.OnValueChanged += new((s, ev) => force.Value = sldForce.Value);
            accAtom.Controls.Add(sldForce);
        }

        atomAdded.AtomForceAdded += new((s, ev) =>
        {
            Force forceAdded = ev.Force!;
            Slider sldForce = new() { Text = $"{forceAdded.Name}", MinValue = -1.0, MaxValue = 1.0, Value = forceAdded.Value, ValueFormat = "F2" };
            sldForce.OnValueChanged += new((s, ev) => forceAdded.Value = sldForce.Value);
            accAtom.Controls.Add(sldForce);
            atomAdded.AtomForceRemoved += new((s, ev) =>
            {
                if (ev.Force! == forceAdded)
                    accAtom.Controls.Remove(sldForce);
            });
        });

        List<Force> forces = new();
        foreach (Atom atomTarget in universe.Atoms)
        {
            Force force = new($"{atomAdded.Name} - {atomTarget.Name}", atomTarget);
            forces.Add(force);
            if (atomAdded != atomTarget)
            {
                Force forceTarget = new($"{atomTarget.Name} - {atomAdded.Name}", atomAdded);
                atomTarget.AddForce(forceTarget);
            }
        }
        atomAdded.AddForces(forces);

        Button btnRemoveAtom = new() { Text = "Remove Atom", UseVisualStyleBackColor = true, TextAlign = ContentAlignment.MiddleCenter, BackColor = Color.Red, FlatStyle = FlatStyle.Flat, Tag = "LAST" };
        btnRemoveAtom.Click += new((s, e) => universe.RemoveAtom(atomAdded));
        accAtom.Controls.Add(btnRemoveAtom);

        PanelSettings.Controls.Add(accAtom);
    }
    #endregion
}