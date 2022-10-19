namespace ParticleLifeSimulation.Core
{
    public class Particle
    {
        public double VX { get; set; }
        public double VY { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public Particle(double x, double y)
        {
            X = x;
            Y = y;
            VX = 0.0;
            VY = 0.0;
        }
    }
}