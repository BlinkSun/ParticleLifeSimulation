namespace ParticleLifeSimulation.Core
{
    public class Particle : IDisposable
    {
        #region Events        
        /// <summary>
        /// Occurs when the particle position have changed.
        /// </summary>
        public event EventHandler<EventArgs>? ParticlePositionChanged;
        /// <summary>
        /// Occurs when the particle velocity have changed.
        /// </summary>
        public event EventHandler<EventArgs>? ParticleVelocityChanged;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the x velocity.
        /// </summary>
        /// <value>
        /// The x velocity.
        /// </value>
        public double VX { get; set; }
        /// <summary>
        /// Gets the y velocity.
        /// </summary>
        /// <value>
        /// The y velocity.
        /// </value>
        public double VY { get; set; }
        /// <summary>
        /// Gets the x position.
        /// </summary>
        /// <value>
        /// The x position.
        /// </value>
        public double X { get; set; }
        /// <summary>
        /// Gets the y position.
        /// </summary>
        /// <value>
        /// The y position.
        /// </value>
        public double Y { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class with defined atom and default values.
        /// </summary>
        /// <param name="atom">The atom.</param>
        public Particle()
        {
            X = 0.0;
            Y = 0.0;
            VX = 0.0;
            VY = 0.0;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class with defined position and atom.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="atom">The atom.</param>
        public Particle(double x, double y) : this()
        {
            X = x;
            Y = y;
        }

        public void Dispose()
        {
            ParticlePositionChanged = null;
            ParticleVelocityChanged = null;
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}