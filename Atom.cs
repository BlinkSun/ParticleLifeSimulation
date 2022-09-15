namespace ParticleLifeSimulation;

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