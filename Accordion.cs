namespace ParticleLifeSimulation;

public partial class Accordion : UserControl
{
    public event EventHandler? OnOpened;
    public event EventHandler? OnClosed;

    public Accordion()
    {
        InitializeComponent();
    }

    private void Accordion_Load(object sender, EventArgs e)
    {
        ToggleResize();
    }

    private void ToggleResize()
    {
        if (this.ChkTitleBar.Checked)
        {
            int height = 0;
            foreach (Control control in this.PanelControles.Controls)
            {
                height += control.Height;
                control.Left = control.Margin.Left;
            }
            this.Height = this.ChkTitleBar.Height + height;
            this.ChkTitleBar.Image = Properties.Resources.arrow_bottom.ToBitmap();
            OnOpened?.Invoke(this, new EventArgs());
        }
        else
        {
            this.Height = this.ChkTitleBar.Height;
            this.ChkTitleBar.Image = Properties.Resources.arrow_right.ToBitmap();
            OnClosed?.Invoke(this, new EventArgs());
        }
    }

    private void ChkTitleBar_CheckedChanged(object sender, EventArgs e)
    {
        ToggleResize();
    }

    public void Add(Control ctrl)
    {
        int top = 0;
        foreach (Control control in this.PanelControles.Controls)
            top += control.Height;
        ctrl.Top = top;
        this.PanelControles.Controls.Add(ctrl);
        ToggleResize();
    }

    public void Clear()
    {
        this.PanelControles.Controls.Clear();
        ToggleResize();
    }

    public void Title(string title)
    {
        this.ChkTitleBar.Text = title;
    }

    private void Accordion_SizeChanged(object sender, EventArgs e)
    {
        this.SuspendLayout();
        foreach (Control control in this.PanelControles.Controls)
            control.Width = this.ClientSize.Width - control.Margin.Left - control.Margin.Right;
        this.ResumeLayout();
    }
}
