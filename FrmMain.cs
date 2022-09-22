using System.Reflection;

namespace ParticleLifeSimulation;

public partial class FrmMain : Form
{
    private Simulation simulation;

    public FrmMain()
    {
        InitializeComponent();

        simulation = new Simulation(500.0, 500.0, true);
        simulation.AtomAdded += new EventHandler<AtomEventArgs>(Simulation_AtomAdded);

        for (int i = 0; i < 4; i++)
        {
            Color randColor = GetRandomKnownColor();
            while (simulation.IsAtomExist(randColor.Name)) randColor = GetRandomKnownColor();
            simulation.AddAtom($"{randColor.Name}", 200, randColor, 2.5);
        }

        //simulation.AddAtom("Greeeeeens", 200, Color.Green, 1.5);
        //simulation.AddAtom("Reeeeeeeeds", 200, Color.Red, 1.5);

        Canvas.OnPainting += (s, pev) =>
        {
            pev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            pev.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
            simulation.Step();
            simulation.Draw(pev.Graphics);
        };

        Canvas.Interval = 15;
        Canvas.Animated = true;
    }

    private void Simulation_AtomAdded(object? sender, AtomEventArgs e)
    {
        Atom atom = e.Atom;
        Accordion accordion = new();
        accordion.Title($"{atom.Name}");

        Slider sldNumber = new() { MinValue = 1.0, MaxValue = 1000.0, CurrentValue = atom.Number };
        sldNumber.SetText("Number");
        sldNumber.OnValueChanged += new((s,ev) => atom.Number = Convert.ToInt32(sldNumber.CurrentValue));
        accordion.Add(sldNumber);

        Slider sldRadius = new() { MinValue = 1.0, MaxValue = 10.0, CurrentValue = atom.Radius, ValueFormat = "F1" };
        sldRadius.SetText("Radius");
        sldRadius.OnValueChanged += new((s,ev) => atom.Radius = Convert.ToInt32(sldRadius.CurrentValue));
        accordion.Add(sldRadius);

        Label lblForces = new() { Text = "Forces :" };
        accordion.Add(lblForces);

        foreach (KeyValuePair<string, Force> keyValuePair in atom.Forces)
        {
            Force force = keyValuePair.Value;
            Slider sldForce = new() { MinValue = -1.0, MaxValue = 1.0, CurrentValue = force.Value, ValueFormat = "F2" };
            sldForce.SetText($"{force.Name}");
            sldForce.OnValueChanged += new((s,ev) => force.Value = sldForce.CurrentValue);
            accordion.Add(sldForce);
        }

        PanelSettings.Controls.Add(accordion);
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        CreateGlobalsSettings();
    }

    #region PanelSettings
    private void CreateGlobalsSettings()
    {
        // Settings Globals
        Accordion accSettings = new Accordion();
        accSettings.Title("Settings");
        // Start/Reset button
        Button btn = new Button() { Text = "START/RESET", UseVisualStyleBackColor = true };
        btn.Click += new EventHandler((s,e) => simulation.ResetAllParticles());
        accSettings.Add(btn);
        //Wrap/Bounded
        CheckBox chk1 = new CheckBox() { Text = "Wrap", Checked = simulation.Wrap };
        chk1.CheckedChanged += new EventHandler((s,e) => simulation.Wrap = chk1.Checked);
        accSettings.Add(chk1);
        //Contrast
        CheckBox chk2 = new CheckBox() { Text = "Contrast", Checked = simulation.Contrast };
        chk2.CheckedChanged += new EventHandler((s,e) => simulation.Contrast = chk2.Checked);
        accSettings.Add(chk2);

        PanelSettings.Controls.Add(accSettings);
    }
    private void PopulatePanelSettings()
    {
        // GREENS
        Accordion accGREENS = new();
        accGREENS.Title("GREENS =>");
        Slider sldGREENSNumber = new() { MinValue = 1.0, MaxValue = 1000.0, CurrentValue = 500.0 };
        sldGREENSNumber.SetText("Number");
        sldGREENSNumber.OnValueChanged += new(Sliders_ValueChanged);
        accGREENS.Add(sldGREENSNumber);

        Slider sldGREENSGREENS = new() { MinValue = -1.0, MaxValue = 1.0, CurrentValue = 0.0, ValueFormat = "F2" };
        sldGREENSGREENS.SetText("greens -> greens");
        sldGREENSGREENS.OnValueChanged += new(Sliders_ValueChanged);
        accGREENS.Add(sldGREENSGREENS);

        Slider sldGREENSREDS = new() { MinValue = -1.0, MaxValue = 1.0, CurrentValue = 0.0, ValueFormat = "F2" };
        sldGREENSREDS.SetText("greens -> reds");
        sldGREENSREDS.OnValueChanged += new(Sliders_ValueChanged);
        accGREENS.Add(sldGREENSREDS);

        Slider sldGREENSYELLOWS = new() { MinValue = -1.0, MaxValue = 1.0, CurrentValue = 0.0, ValueFormat = "F2" };
        sldGREENSYELLOWS.SetText("greens -> yellows");
        sldGREENSYELLOWS.OnValueChanged += new(Sliders_ValueChanged);
        accGREENS.Add(sldGREENSYELLOWS);

        Slider sldGREENSBLUES = new() { MinValue = -1.0, MaxValue = 1.0, CurrentValue = 0.0, ValueFormat = "F2" };
        sldGREENSBLUES.SetText("greens -> blues");
        sldGREENSBLUES.OnValueChanged += new(Sliders_ValueChanged);
        accGREENS.Add(sldGREENSBLUES);

        PanelSettings.Controls.Add(accGREENS);

        // REDS
        Accordion accREDS = new Accordion();
        accREDS.Title("REDS =>");
        Slider sldREDSNumber = new Slider() { MinValue = 1.0, MaxValue = 1000.0, CurrentValue = 500.0 };
        sldREDSNumber.SetText("Number");
        sldREDSNumber.OnValueChanged += new EventHandler(Sliders_ValueChanged);
        accREDS.Add(sldREDSNumber);
        PanelSettings.Controls.Add(accREDS);

        // YELLOWS
        Accordion accYELLOWS = new Accordion();
        accYELLOWS.Title("YELLOWS =>");
        Slider sldYELLOWSNumber = new Slider() { MinValue = 1.0, MaxValue = 1000.0, CurrentValue = 500.0 };
        sldYELLOWSNumber.SetText("Number");
        sldYELLOWSNumber.OnValueChanged += new EventHandler(Sliders_ValueChanged);
        accYELLOWS.Add(sldYELLOWSNumber);
        PanelSettings.Controls.Add(accYELLOWS);

        // BLUES
        Accordion accBLUES = new Accordion();
        accBLUES.Title("BLUES =>");
        Slider sldBLUESNumber = new Slider() { MinValue = 1.0, MaxValue = 1000.0, CurrentValue = 500.0 };
        sldBLUESNumber.SetText("Number");
        sldBLUESNumber.OnValueChanged += new EventHandler(Sliders_ValueChanged);
        accBLUES.Add(sldBLUESNumber);
        PanelSettings.Controls.Add(accBLUES);
    }
    private void Sliders_ValueChanged(object? sender, EventArgs e)
    {
        Slider sld = (Slider)sender!;
        double value = sld.CurrentValue;
        //GenerateAtomsAndRules();
    }
    private void PanelSettings_SizeChanged(object sender, EventArgs e)
    {
        FlowLayoutPanel panel = (FlowLayoutPanel)sender;
        panel.SuspendLayout();
        foreach (Control control in panel.Controls)
            control.Width = panel.ClientSize.Width - control.Margin.Left - control.Margin.Right;
        panel.ResumeLayout();
    }
    private void PanelSettings_ControlAdded(object sender, ControlEventArgs e)
    {
        FlowLayoutPanel panel = (FlowLayoutPanel)sender;
        e.Control.Width = panel.ClientSize.Width - e.Control.Margin.Left - e.Control.Margin.Right;
    }
    #endregion

    private static Color GetRandomKnownColor()
    {
        List<Color> names = ((KnownColor[])Enum.GetValues(typeof(KnownColor))).Select(c => Color.FromKnownColor(c)).ToList();
        string[] systemColorNames = typeof(SystemColors).GetRuntimeProperties().Select(c => c.Name).ToArray();
        names.RemoveAll(c => systemColorNames.Contains(c.Name) || c.Name.Count(c => char.IsUpper(c)) > 1 || c.Name == "Transparent");
        return names[Random.Shared.Next(names.Count)];
    }
}