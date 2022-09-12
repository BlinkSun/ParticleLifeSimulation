namespace ParticleLifeSimulation
{
    public partial class FrmMain : Form
    {
        private readonly double CANVAS_SIZE = 400; // 500 - (2*50)

        private List<Atom> atoms;
        private List<Action> rules;

        public FrmMain()
        {
            InitializeComponent();
            atoms = new List<Atom>();
            rules = new List<Action>();

            Atom[] yellow = Create(200, Color.Yellow);
            Atom[] red = Create(200, Color.Red);
            Atom[] green = Create(200, Color.Green);
            Atom[] blue = Create(200, Color.Blue);

            rules.AddRange(new Action[] {
                () => Rule(green, green, -0.32),
                () => Rule(green, red, -0.17),
                () => Rule(green, yellow, 0.34),
                () => Rule(green, blue, 0.21),
                () => Rule(red, red, -0.1),
                () => Rule(red, green, -0.34),
                () => Rule(red, blue, -0.21),
                () => Rule(yellow, yellow, 0.15),
                () => Rule(yellow, green, -0.2),
                () => Rule(yellow, blue, 0.2),
                () => Rule(blue, blue, 0.15),
                () => Rule(blue, green, -0.2),
                () => Rule(blue, red, 0.2),
            });

            Canvas.OnPainting += (s, ev) =>
            {
                ev.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                ev.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;
                foreach (Action rule in rules) rule();
                foreach (Atom atom in atoms) Draw(atom.x, atom.y, atom.color, 5, ev.Graphics);
            };

            Canvas.Interval = 15;
            Canvas.Animated = true;
        }

        private void Draw(double x, double y, Color c, double s, Graphics g)
        {
            g.FillEllipse(new SolidBrush(c), (float)x, (float)y, (float)s, (float)s);
        }

        private void Rule(Atom[] atoms1, Atom[] atoms2, double g)
        {
            for (int i = 0; i < atoms1.Length; i++)
            {
                Atom a = atoms1[i];
                double fx = 0;
                double fy = 0;
                for (int j = 0; j < atoms2.Length; j++)
                {
                    Atom b = atoms2[j];
                    double dx = a.x - b.x;
                    double dy = a.y - b.y;
                    double d = Math.Sqrt(dx * dx + dy * dy);
                    if (d > 0 && d < 80)
                    {
                        double F = (g * 1) / d;
                        fx += F * dx;
                        fy += F * dy;
                    }
                }
                a.vx = (a.vx + fx) * 0.5;
                a.vy = (a.vy + fy) * 0.5;
                a.x += a.vx;
                a.y += a.vy;
                if (a.x <= 0 || a.x >= 500) { a.vx *= -1; }
                if (a.y <= 0 || a.y >= 500) { a.vy *= -1; }
            }
        }

        private double Randomxy() => Random.Shared.NextDouble() * CANVAS_SIZE + 50;
        
        private Atom[] Create(int number, Color color)
        {
            Atom[] group = new Atom[number];
            for (int i = 0; i < number; i++)
            {
                group[i] = new Atom(Randomxy(), Randomxy(), color);
                atoms.Add(group[i]);
            }
            return group;
        }
    }

    public class Atom
    {
        public double x { get; set; }
        public double y { get; set; }
        public double vx { get; set; }
        public double vy { get; set; }
        public Color color { get; set; }

        public Atom(double x, double y, Color c)
        {
            this.x = x;
            this.y = y;
            vx = 0;
            vy = 0;
            color = c;
        }
    }
}