namespace ParticleLifeSimulation
{
    public partial class FrmMandlebrot : Form
    {
        public FrmMandlebrot()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap bitmap = new(Width, Height);
            using Graphics g = Graphics.FromImage(bitmap);
            //using Graphics g = e.Graphics;
            g.Clear(Color.Black);
            Action<float, float, Color> atom = new((x, y, color) =>
            {
                g.FillEllipse(new SolidBrush(color), x, y, 3f, 3f);
            });
            float centerX = Width / 2f;
            float centerY = Height / 2f;
            for (int y = 0; y < Height; y += 3)
            {
                for (int x = 0; x < Width; x += 3)
                {
                    float dx = ((x - centerX) / hScrollBar1.Value) - 0.12f;
                    float dy = ((y - centerY) / hScrollBar1.Value) - 0.82f;
                    float a = dx;
                    float b = dy;

                    for (int t = 0; t < 200; t++)
                    {
                        float d = (a * a) - (b * b) + dx;
                        b = (2 * (a * b)) + dy;
                        a = d;
                        bool H = d > 200;

                        if (H)
                        {
                            atom(x, y, Color.FromArgb(Math.Clamp(t * 3, 0, 255), Math.Clamp(t, 0, 255), Math.Clamp(t / 2, 0, 255)));
                            break;
                        }
                    }
                }
            }
            e.Graphics.DrawImage(bitmap, 0, 0);
        }

        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
