using System.ComponentModel;

namespace ParticleLifeSimulation
{
    [DefaultEvent("Click")]
    public partial class Canvas : UserControl
    {
        private readonly System.Windows.Forms.Timer timer;

        public event PaintEventHandler? OnPainting;

        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Comportement")]
        [Description("Gets or sets whether the Canvas is animating.")]
        public bool Animated
        {
            get { return this.timer.Enabled; }
            set { this.timer.Enabled = value; }
        }

        [Browsable(true)]
        [DefaultValue(100)]
        [Category("Comportement")]
        [Description("Gets or sets the time, in milliseconds, before the OnPainting event is raised relative to the last occurrence of the OnPainting event.")]
        public int Interval
        {
            get { return this.timer.Interval; }
            set { this.timer.Interval = value; }
        }

        public Canvas()
        {
            InitializeComponent();
            timer = new();
            timer.Tick += (s, e) => { Refresh(); };
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            OnPainting?.Invoke(this, e);
        }
    }
}
