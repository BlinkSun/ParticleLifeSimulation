namespace ParticleLifeSimulation.UserControls
{
    public partial class Slider : UserControl
    {
        #region Events
        public event EventHandler? OnValueChanged;
        #endregion

        #region Properties
        public bool AutoEllipsis { get; set; } = true;
        public bool AutoWidth { get; set; } = false;
        public bool AutoHeight { get; set; } = true;
        public double MinValue { get; set; } = 0.0;
        public double MaxValue { get; set; } = 10.0;
        private double _value = 5.0;
        public double Value
        {
            get { return _value; }
            set
            {
                if (_value != Math.Clamp(value, MinValue, MaxValue))
                {
                    _value = Math.Clamp(value, MinValue, MaxValue);
                    Invalidate();
                    OnValueChanged?.Invoke(this, new EventArgs());
                }
            }
        }
        private Color _sliderColor = SystemColors.Highlight;
        public Color SliderColor
        {
            get { return _sliderColor; }
            set
            {
                _sliderColor = value;
                Invalidate();
            }
        }
        public string ValueFormat { get; set; } = "F0";

        #endregion

        #region Constructors
        public Slider()
        {
            InitializeComponent();
        }
        #endregion

        #region Methodes
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            SizeF textSizeF = Graphics.FromHwndInternal(Handle).MeasureString($"{Text} {Value.ToString(ValueFormat)}", Font);
            if (AutoWidth || AutoHeight)
            {
                if (AutoWidth) Width = Convert.ToInt32(textSizeF.Width);
                if (AutoHeight) Height = Convert.ToInt32(textSizeF.Height);
                Invalidate();
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            // Slider Bar
            StringFormat formatter = new(StringFormatFlags.NoWrap) { LineAlignment = StringAlignment.Center };
            Brush brush = new SolidBrush(SliderColor);
            double normalizeValue = MinValue * -1;
            int x = (int)((Value + normalizeValue) * Width / (MaxValue + normalizeValue));
            e.Graphics.FillRectangle(brush, new Rectangle(0, 0, x, Height));

            // Slider Texts:
            string value = Value.ToString(ValueFormat);
            SizeF valueSizeF = e.Graphics.MeasureString(value, Font);
            // - Text
            RectangleF textRectF = new(0f, 0f, Width - (AutoEllipsis ? valueSizeF.Width : 0), Height);
            formatter.Alignment = StringAlignment.Near;
            if (AutoEllipsis) formatter.Trimming = StringTrimming.EllipsisCharacter;
            e.Graphics.DrawString(Text, Font, new SolidBrush(ForeColor), textRectF, formatter);
            // - Value
            RectangleF valueRectF = new(Width - valueSizeF.Width, 0f, valueSizeF.Width, Height);
            formatter.Alignment = StringAlignment.Far;
            formatter.Trimming = StringTrimming.Character;
            e.Graphics.DrawString(value, Font, new SolidBrush(ForeColor), valueRectF, formatter);
        }
        private void Slider_MouseUpDownMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                double normValue = MinValue * -1;
                Value = (e.X * (MaxValue + normValue) / Width) - normValue;
            }
        }
        #endregion
    }
}
