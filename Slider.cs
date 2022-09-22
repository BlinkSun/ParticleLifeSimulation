using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParticleLifeSimulation
{
    public partial class Slider : UserControl
    {
        private double _minValue = 0;
        public double MinValue
        {
            get { return _minValue; }
            set { _minValue = value; }
        }

        private double _maxValue = 10;
        public double MaxValue
        {
            set { _maxValue = value; }
            get { return _maxValue; }
        }

        private double _currentValue = 5;
        public double CurrentValue
        {
            get { return _currentValue; }
            set {
                _currentValue = value;
                this.LblCurrentValue.Text = this._currentValue.ToString(this._valueFormat);
            }
        }

        private Color _sliderColor = Color.LightGray;

        public Color SliderColor
        {
            get { return _sliderColor; }
            set { _sliderColor = value; }
        }

        private string _valueFormat = "F0";

        public string ValueFormat
        {
            get { return _valueFormat; }
            set {
                _valueFormat = value;
                this.LblCurrentValue.Text = this._currentValue.ToString(this._valueFormat);
            }
        }

        public string GetText() => this.LblSlider.Text;
        public void SetText(string value) => this.LblSlider.Text = value;

        public event EventHandler? OnValueChanged;

        public Slider()
        {
            InitializeComponent();
            this.LblCurrentValue.Text = this._currentValue.ToString(this._valueFormat);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Brush brush = new SolidBrush(_sliderColor);
            int x = (int)(this.CurrentValue * this.Width / this.MaxValue);
            e.Graphics.FillRectangle(brush, new Rectangle(0, 0, x, this.Height));
        }

        private void Slider_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.CurrentValue = Math.Clamp(e.X * this.MaxValue / this.Width, this._minValue, this._maxValue);
                this.LblCurrentValue.Text = this._currentValue.ToString(this._valueFormat);
                this.Invalidate();
            }
        }

        private void Slider_MouseUp(object sender, MouseEventArgs e)
        {
            OnValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
