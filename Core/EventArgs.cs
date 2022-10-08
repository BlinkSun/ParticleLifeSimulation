namespace ParticleLifeSimulation.Core2
{
    #region EventArgs    
    /// <summary>
    /// Contain Universe event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class UniverseEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the particle affected.
        /// </summary>
        /// <value>
        /// The particle affected.
        /// </value>
        public Particle? Particle { get; private set; }
        /// <summary>
        /// Gets the atom affected.
        /// </summary>
        /// <value>
        /// The atom affected.
        /// </value>
        public Atom? Atom { get; private set; }
        /// <summary>
        /// Gets the force affected.
        /// </summary>
        /// <value>
        /// The force affected.
        /// </value>
        public Force? Force { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="UniverseEventArgs"/> class.
        /// </summary>
        /// <param name="particle">The particle affected.</param>
        public UniverseEventArgs(Particle particle) => Particle = particle;
        /// <summary>
        /// Initializes a new instance of the <see cref="UniverseEventArgs"/> class.
        /// </summary>
        /// <param name="atom">The atom affected.</param>
        public UniverseEventArgs(Atom atom) => Atom = atom;
        /// <summary>
        /// Initializes a new instance of the <see cref="UniverseEventArgs"/> class.
        /// </summary>
        /// <param name="force">The force affected.</param>
        public UniverseEventArgs(Force force) => Force = force;
    }
    /// <summary>
    /// Contain Particle event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ParticleEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the atom affected.
        /// </summary>
        /// <value>
        /// The atom affected.
        /// </value>
        public Atom Atom { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEventArgs"/> class.
        /// </summary>
        /// <param name="atom">The atom affected.</param>
        public ParticleEventArgs(Atom atom) => Atom = atom;
    }
    /// <summary>
    /// Contain Atom event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class AtomEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the force affected.
        /// </summary>
        /// <value>
        /// The force affected.
        /// </value>
        public Force? Force { get; private set; }
        /// <summary>
        /// Gets the particles affected.
        /// </summary>
        /// <value>
        /// The particles affected.
        /// </value>
        public List<Particle>? Particles { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="AtomEventArgs"/> class.
        /// </summary>
        /// <param name="force">The force affected.</param>
        public AtomEventArgs() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="AtomEventArgs"/> class.
        /// </summary>
        /// <param name="force">The force affected.</param>
        public AtomEventArgs(Force force) => Force = force;
        /// <summary>
        /// Initializes a new instance of the <see cref="AtomEventArgs"/> class.
        /// </summary>
        /// <param name="particle">The particles affected.</param>
        public AtomEventArgs(List<Particle> particles) => Particles = particles;
    }
    /// <summary>
    /// Contain Force event data.
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class ForceEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ForceEventArgs"/> class.
        /// </summary>
        public ForceEventArgs() { }
    }
    #endregion
}
