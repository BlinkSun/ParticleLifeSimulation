using System.ComponentModel;

namespace ParticleLifeSimulation.UserControls
{
    [DefaultEvent("Click")]
    public partial class Canvas : UserControl
    {
        private readonly System.Windows.Forms.Timer Timer;

        public event PaintEventHandler? OnPainting;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Canvas"/> is animated.
        /// </summary>
        /// <value>
        ///   <c>true</c> if animated; otherwise, <c>false</c>.
        /// </value>
        [Browsable(true)]
        [DefaultValue(false)]
        [Category("Comportement")]
        [Description("Gets or sets whether the Canvas is animated.")]
        public bool Animated
        {
            get => Timer.Enabled;
            set => Timer.Enabled = value;
        }

        /// <summary>
        /// Gets or sets the interval in milliseconds.
        /// </summary>
        /// <value>
        /// The interval in milliseconds.
        /// </value>
        [Browsable(true)]
        [DefaultValue(100)]
        [Category("Comportement")]
        [Description("Gets or sets the time, in milliseconds, before the OnPainting event is raised relative to the last occurrence of the OnPainting event.")]
        public int Interval
        {
            get => Timer.Interval;
            set => Timer.Interval = value;
        }

        public Canvas()
        {
            InitializeComponent();
            Timer = new();
            Timer.Tick += (s, e) => { Refresh(); };
            DoubleBuffered = true;
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            OnPainting?.Invoke(this, e);
        }
    }
}
