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
        public event EventHandler<AtomEventArgs>? AtomParticlesReset;
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
        public List<Force> Forces { get; private set; }
        public List<Particle> Particles { get; private set; }
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
        private string name;
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
        private Color color;
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
        private double radius;
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
        public double MaxWidth { get; private set; }
        public double MaxHeight { get; private set; }
        #endregion

        #region Constructors
        public Atom(string name, Color color, double maxWidth, double maxHeight, double radius)
        {
            Forces = new();
            Particles = new();
            this.name = name;
            this.color = color;
            this.radius = radius;
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }
        public Atom(string name, Color color, double maxWidth, double maxHeight, double radius, int particleNumber) : this(name, color, maxWidth, maxHeight, radius) => AddParticles(particleNumber);
        #endregion

        #region Atom
        public void SetMaxSize(double maxWidth, double maxHeight)
        {
            MaxWidth = maxWidth;
            MaxHeight = maxHeight;
        }
        #endregion

        #region Particles
        public void AddParticles(IEnumerable<Particle> particles)
        {
            foreach (Particle particle in particles)
                Particles.Add(particle);
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
            }
            AddParticles(particlesAdded);
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
                while (particlesRemoved.Contains(particleToRemove))
                {
                    index = Random.Shared.Next(Particles.Count);
                    particleToRemove = Particles[index];
                }
                particlesRemoved.Add(particleToRemove);
            }
            RemoveParticles(particlesRemoved);
        }
        public void RemoveParticles(IEnumerable<Particle> particles)
        {
            foreach (Particle particle in particles)
                Particles.Remove(particle);
            AtomParticlesRemoved?.Invoke(this, new(particles));
        }

        public void ClearParticles()
        {
            Particles.Clear();
            AtomParticlesChanged?.Invoke(this, EventArgs.Empty);
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
            double xOffSet = MaxWidth * 0.1;
            double yOffSet = MaxHeight * 0.1;
            double x = (Random.Shared.NextDoubleInclusive() * (MaxWidth - (2 * xOffSet))) + xOffSet;
            double y = (Random.Shared.NextDoubleInclusive() * (MaxHeight - (2 * yOffSet))) + yOffSet;
            particle.X = x;
            particle.Y = y;
            particle.VX = 0;
            particle.VY = 0;
        }
        public void ResetParticles(IEnumerable<Particle> particles)
        {
            foreach (Particle particle in particles)
                ResetParticle(particle);
            AtomParticlesReset?.Invoke(this, new(particles));
        }
        public void ResetParticles() => ResetParticles(Particles);
        #endregion

        #region Forces
        public bool HasForceWith(Atom target) => Forces.FirstOrDefault(force => force.Target == target) is not null;
        public Force GetForceWith(Atom target) => Forces.Single(force => force.Target == target);
        public void AddForce(Force force)
        {
            if (!Forces.Contains(force))
            {
                Forces.Add(force);
                AtomForceAdded?.Invoke(this, new(force));
            }
        }
        public void AddForces(IEnumerable<Force> forces)
        {
            foreach (Force force in forces) AddForce(force);
        }
        public void RemoveForce(Force force)
        {
            Forces.Remove(force);
            force.Dispose();
            AtomForceRemoved?.Invoke(this, new(force));
        }
        public void RemoveForceWith(Atom target)
        {
            if (HasForceWith(target)) RemoveForce(GetForceWith(target));
        }
        public void RemoveForces(IEnumerable<Force> forces)
        {
            foreach (Force force in forces) RemoveForce(force);
        }
        public void ClearForces()
        {
            foreach (Force force in Forces) force.Dispose();
            Forces.Clear();
            AtomForcesCleared?.Invoke(this, EventArgs.Empty);
        }
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
            AtomParticlesAdded = null;
            AtomParticlesRemoved = null;
            AtomParticlesChanged = null;

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
