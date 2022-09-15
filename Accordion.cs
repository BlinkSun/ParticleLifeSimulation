namespace ParticleLifeSimulation;

public partial class Accordion : UserControl
{
    public event EventHandler? OnOpened;
    public event EventHandler? OnClosed;

    public Accordion()
    {
        InitializeComponent();
        this.Height = ChkTitleBar.Height;
    }

    private void Accordion_Load(object sender, EventArgs e)
    {
        ReSize();
    }

    private void ReSize()
    {
        if (ChkTitleBar.Checked)
        {
            int height = 0;
            foreach (Control control in PanelControles.Controls)
            {
                height += control.Height;
            }
            this.Height = ChkTitleBar.Height + height;// + defaultHeight;
            OnOpened?.Invoke(this, new EventArgs());
        }
        else
        {
            this.Height = ChkTitleBar.Height;
            OnClosed?.Invoke(this, new EventArgs());
        }
    }

    private void ChkTitleBar_CheckedChanged(object sender, EventArgs e)
    {
        ReSize();
    }

    public void Add(Control ctrl)
    {
        int top = 0;
        foreach (Control control in PanelControles.Controls) top += control.Height;
        ctrl.Top = top;
        PanelControles.Controls.Add(ctrl);
        ReSize();
    }

    public void Clear()
    {
        PanelControles.Controls.Clear();
    }

    public void Title(string title)
    {
        ChkTitleBar.Text = title;
    }
}
