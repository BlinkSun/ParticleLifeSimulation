namespace ParticleLifeSimulation;

public class Particle
{
    public double X { get; set; }
    public double Y { get; set; }
    public double VX { get; set; }
    public double VY { get; set; }

    public Particle(double x, double y)
    {
        this.X = x;
        this.Y = y;
        VX = 0;
        VY = 0;
    }
}