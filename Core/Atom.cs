namespace ParticleLifeSimulation.Core
{
    public class Atom : IDisposable
    {
        #region Events
        /// <summary>
        /// Occurs when the name of the atom has been changed.
        /// </summary>
        public event EventHandler<EventArgs>? AtomNameChanged;
        /// <summary>
        /// Occurs when the color of the atom has been changed.
        /// </summary>
        public event EventHandler<EventArgs>? AtomColorChanged;
        /// <summary>
        /// Occurs when the radius of the atom has been changed.
        /// </summary>
        public event EventHandler<EventArgs>? AtomRadiusChanged;
        /// <summary>
        /// Occurs when the forces of the atom have been cleared.
        /// </summary>
        public event EventHandler<EventArgs>? AtomForcesCleared;
        /// <summary>
        /// Occurs when force of the atom have been added.
        /// </summary>
        public event EventHandler<AtomEventArgs>? AtomForceAdded;
        /// <summary>
        /// Occurs when force(s) of the atom have been removed.
        /// </summary>
        public event EventHandler<AtomEventArgs>? AtomForceRemoved;
        /// <summary>
        /// Occurs when particle of the atom have been reset.
        /// </summary>
        public event EventHandler<EventArgs>? AtomParticlesReset;
        /// <summary>
        /// Occurs when particle of the atom have been cleared.
        /// </summary>
        public event EventHandler<EventArgs>? AtomParticlesCleared;
        /// <summary>
        /// Occurs when particle of the atom have been added.
        /// </summary>
        public event EventHandler<AtomEventArgs>? AtomParticlesAdded;
        /// <summary>
        /// Occurs when particle of the atom have been removed.
        /// </summary>
        public event EventHandler<AtomEventArgs>? AtomParticlesRemoved;
        /// <summary>
        /// Occurs when particle of the atom have been changed.
        /// </summary>
        public event EventHandler<EventArgs>? AtomParticlesChanged;
        #endregion

        #region Properties
        private string name;
        private Color color;
        private double radius = 2.0;
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get => name;
            set
            {
                if (name != value)
                {
                    name = value;
                    AtomNameChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color
        {
            get => color;
            set
            {
                if (color != value)
                {
                    color = value;
                    AtomColorChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets the particles.
        /// </summary>
        /// <value>
        /// The particles.
        /// </value>
        public List<Particle> Particles { get; private set; }
        /// <summary>
        /// Gets the forces.
        /// </summary>
        /// <value>
        /// The forces.
        /// </value>
        public List<Force> Forces { get; private set; }
        /// <summary>
        /// Gets or sets the radius.
        /// </summary>
        /// <value>
        /// The radius.
        /// </value>
        public double Radius
        {
            get => radius;
            set
            {
                if (radius != value)
                {
                    radius = value;
                    AtomRadiusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the diameter.
        /// </summary>
        /// <value>
        /// The diameter.
        /// </value>
        public double Diameter
        {
            get => radius * 2.0;
            set
            {
                if (radius != value / 2.0)
                {
                    radius = value / 2.0;
                    AtomRadiusChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// Gets or sets the max width.
        /// </summary>
        /// <value>
        /// The max width.
        /// </value>
        public double MaxWidth { get; set; }
        /// <summary>
        /// Gets or sets the max height.
        /// </summary>
        /// <value>
        /// The max height.
        /// </value>
        public double MaxHeight { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class with default values.
        /// </summary>
        public Atom(double maxWidth, double maxHeight)
        {
            Particles = new();
            Forces = new();
            color = Color.White;
            name = color.Name;
            radius = 2.0;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class with defined name and default values.
        /// </summary>
        /// <param name="name">The name of the atom.</param>
        public Atom(string name, double maxWidth, double maxHeight) : this(maxWidth, maxHeight) => this.name = name;
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class with defined color and default values.
        /// </summary>
        /// <param name="color">The color of the atom.</param>
        public Atom(Color color, double maxWidth, double maxHeight) : this(color.Name, maxWidth, maxHeight) => this.color = color;
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class with defined name, color and default values.
        /// </summary>
        /// <param name="name">The name of the atom.</param>
        /// <param name="color">The color of the atom.</param>
        public Atom(string name, Color color, double maxWidth, double maxHeight, double radius) : this(maxWidth, maxHeight)
        {
            this.color = color;
            this.name = name;
            this.radius = radius;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class with defined name, color and particles.
        /// </summary>
        /// <param name="name">The name of the atom.</param>
        /// <param name="color">The color of the atom.</param>
        /// <param name="particlesNumber">The number of particles in the atom.</param>
        public Atom(string name, Color color, double maxWidth, double maxHeight, double radius, int particleNumber) : this(name, color, maxWidth, maxHeight, radius) => AddParticles(particleNumber);
        #endregion

        #region Particles
        public void AddParticle(Particle particle)
        {
            if (Particles.Contains(particle)) throw new InvalidAtomException("The particle already exists in that atom.");
            Particles.Add(particle);
        }
        public void AddParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles)
                AddParticle(particle);
            AtomParticlesAdded?.Invoke(this, new(particles));
        }
        public void AddParticles(int number)
        {
            double xOffSet = MaxWidth * 0.1;
            double yOffSet = MaxHeight * 0.1;
            List<Particle> particlesAdded = new();
            for (int i = 0; i < number; i++)
            {
                double x = (Random.Shared.NextDoubleInclusive() * (MaxWidth - (2 * xOffSet))) + xOffSet;
                double y = (Random.Shared.NextDoubleInclusive() * (MaxHeight - (2 * yOffSet))) + yOffSet;
                Particle particle = new(x, y);
                particlesAdded.Add(particle);
                Particles.Add(particle);
            }
            AtomParticlesAdded?.Invoke(this, new(particlesAdded));
        }

        public void RemoveParticle(Particle particle)
        {
            if (Particles.Contains(particle))
                Particles.Remove(particle);
        }
        public void RemoveParticles(int number)
        {
            if (number < 0 || number > Particles.Count)
                number = Math.Clamp(number, 0, Particles.Count);
            List<Particle> particlesRemoved = new();
            for (int i = 0; i < number; i++)
            {
                int index = Random.Shared.Next(Particles.Count);
                Particle particleToRemove = Particles[index];
                particlesRemoved.Add(particleToRemove);
                RemoveParticle(particleToRemove);
            }

            AtomParticlesRemoved?.Invoke(this, new(particlesRemoved));

            foreach (Particle particle in particlesRemoved)
                particle.Dispose();
        }
        public void RemoveParticles(List<Particle> particles)
        {
            foreach (Particle particle in particles)
                RemoveParticle(particle);

            AtomParticlesRemoved?.Invoke(this, new(particles));

            foreach (Particle particle in particles)
                particle.Dispose();
        }

        public void ClearParticles()
        {
            foreach (Particle particle in Particles)
                particle.Dispose();

            Particles.Clear();

            AtomParticlesCleared?.Invoke(this, EventArgs.Empty);
        }
        public void UpdateParticles(int newCount)
        {
            int diffCount = newCount - Particles.Count;
            if (diffCount > 0)
                AddParticles(diffCount);
            else if (diffCount < 0)
                RemoveParticles(Math.Abs(diffCount));

            AtomParticlesChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ResetParticle(Particle particle)
        {
            if (!Particles.Contains(particle)) throw new InvalidAtomException("The particle doesn't exists in that atom.");
            double xOffSet = MaxWidth * 0.1;
            double yOffSet = MaxHeight * 0.1;
            double x = (Random.Shared.NextDoubleInclusive() * (MaxWidth - (2 * xOffSet))) + xOffSet;
            double y = (Random.Shared.NextDoubleInclusive() * (MaxHeight - (2 * yOffSet))) + yOffSet;
            particle.X = x;
            particle.Y = y;
            particle.VX = 0;
            particle.VY = 0;
        }
        public void ResetParticles(List<Particle> particles)
        {
            foreach (Particle particle in Particles)
                ResetParticle(particle);
            AtomParticlesReset?.Invoke(this, EventArgs.Empty);
        }
        public void ResetParticles() => ResetParticles(Particles);
        #endregion

        #region Forces
        /// <summary>
        /// Gets the force with another atom target.
        /// </summary>
        /// <param name="atom">The atom target.</param>
        /// <returns>The Force.</returns>
        public Force? GetForceWith(Atom atomTarget) => Forces.FirstOrDefault(force => force.AtomTarget == atomTarget);
        /// <summary>
        /// Add the force to the atom.
        /// </summary>
        /// <param name="force">The force to add.</param>
        /// <exception cref="InvalidAtomException">The force already exist.</exception>
        public void AddForce(Force force)
        {
            if (!Forces.Contains(force))
            {
                Forces.Add(force);
                AtomForceAdded?.Invoke(this, new(force));
            }
        }
        /// <summary>
        /// Add the forces to the atom.
        /// </summary>
        /// <param name="forces">The forces to add.</param>
        public void AddForces(IEnumerable<Force> forces)
        {
            foreach (Force force in forces)
                AddForce(force);
        }
        /// <summary>
        /// Remove the force to the atom.
        /// </summary>
        /// <param name="force">The force to remove.</param>
        /// <exception cref="InvalidAtomException">The force doesn't exists.</exception>
        public void RemoveForce(Force force)
        {
            if (Forces.Contains(force))
            {
                Forces.Remove(force);
                AtomForceRemoved?.Invoke(this, new(force));
                force.Dispose();
            }
        }
        /// <summary>
        /// Remove the force to the atom with atomTarget.
        /// </summary>
        /// <param name="atomTarget">The force to remove with atomTarget.</param>
        /// <exception cref="InvalidAtomException">The force doesn't exists.</exception>
        public void RemoveForceWith(Atom atomTarget)
        {
            if (HasForceWith(atomTarget))
                RemoveForce(GetForceWith(atomTarget)!);
        }
        /// <summary>
        /// Remove the forces to the atom.
        /// </summary>
        /// <param name="forces">The forces to remove.</param>
        public void RemoveForces(IEnumerable<Force> forces)
        {
            foreach (Force force in forces)
                RemoveForce(force);
        }
        /// <summary>
        /// Clears all forces of the atom.
        /// </summary>
        public void ClearForces()
        {

            foreach (Force force in Forces)
                force.Dispose();
            Forces.Clear();
            AtomForcesCleared?.Invoke(this, EventArgs.Empty);
        }
        /// <summary>
        /// Reset max width and max height.
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        public void ResetMaxSize(double maxWidth, double maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }
        /// <summary>
        /// Determines if atom have force with atom target.
        /// </summary>
        /// <param name="atomTarget">The atom target.</param>
        /// <returns>True if has force with, else false.</returns>
        public bool HasForceWith(Atom atomTarget) => Forces.FirstOrDefault(force => force.AtomTarget == atomTarget) is not null;
        #endregion

        #region Finalize
        public void Dispose()
        {
            ClearForces();
            ClearParticles();
            AtomNameChanged = null;
            AtomColorChanged = null;
            AtomRadiusChanged = null;

            AtomForcesCleared = null;
            AtomForceAdded = null;
            AtomForceRemoved = null;

            AtomParticlesReset = null;
            AtomParticlesCleared = null;
            AtomParticlesChanged = null;
            AtomParticlesAdded = null;
            AtomParticlesRemoved = null;
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}
