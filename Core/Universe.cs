namespace ParticleLifeSimulation.Core
{
    public class Universe
    {
        #region Events
        //Universe's Events
        public event EventHandler<EventArgs>? UniverseSizeChanged;
        public event EventHandler<EventArgs>? UniverseWrapChanged;
        //Particle's Events
        public event EventHandler<UniverseEventArgs>? UniverseParticleAdded;
        public event EventHandler<UniverseEventArgs>? UniverseParticleChanged;
        public event EventHandler<UniverseEventArgs>? UniverseParticleRemoved;
        //Atom's Events
        public event EventHandler<UniverseEventArgs>? UniverseAtomAdded;
        public event EventHandler<UniverseEventArgs>? UniverseAtomChanged;
        public event EventHandler<UniverseEventArgs>? UniverseAtomRemoved;
        //Force's Events
        public event EventHandler<UniverseEventArgs>? UniverseForceAdded;
        public event EventHandler<UniverseEventArgs>? UniverseForceChanged;
        public event EventHandler<UniverseEventArgs>? UniverseForceRemoved;
        #endregion

        #region Properties
        public SizeF Size
        {
            get => new((float)Width, (float)Height);
            set
            {
                Width = value.Width;
                Height = value.Height;
                if (Atoms != null) foreach (Atom atom in Atoms) atom.ResetMaxSize(Width, Height);
                UniverseSizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public bool Wrap
        {
            get => wrap;
            set
            {
                wrap = value;
                UniverseWrapChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        private bool wrap;
        public List<Atom> Atoms { get; private set; }
        #endregion

        #region Constructors
        public Universe(double width, double height, bool wrap = false)
        {
            // Init Universe
            Width = width;
            Height = height;
            this.wrap = wrap;

            // Init Atoms
            Atoms = new();
        }
        #endregion

        #region Universe
        public void Step()
        {
            //For each Atom
            foreach (Atom atomSource in Atoms)
            {
                //With each Atom
                foreach (Atom atomTarget in Atoms)
                {
                    //Get forces between AtomSource and AtomTarget
                    double g = atomSource.Forces.Find(force => force.AtomTarget == atomTarget)?.Value ?? double.NaN;
                    if (double.IsNaN(g)) continue;

                    //For every Particles of AtomSource
                    for (int i = 0; i < atomSource.Particles.Count; i++)
                    {
                        Particle a = atomSource.Particles[i];
                        double fx = 0;
                        double fy = 0;
                        //With every Particles of Atom2
                        for (int j = 0; j < atomTarget.Particles.Count; j++)
                        {
                            Particle b = atomTarget.Particles[j];

                            //Calculate delta (distance)
                            double dx = a.X - b.X;
                            double dy = a.Y - b.Y;

                            //If Wrapping
                            if (Wrap)
                            {
                                if (dx > Width * 0.5)
                                {
                                    dx -= Width;
                                }
                                else if (dx < -Width * 0.5)
                                {
                                    dx += Width;
                                }

                                if (dy > Height * 0.5)
                                {
                                    dy -= Height;
                                }
                                else if (dy < -Height * 0.5)
                                {
                                    dy += Height;
                                }
                            }

                            //Calculate the intensity of force
                            double d = Math.Sqrt((dx * dx) + (dy * dy));
                            if (d > 0 && d < 80)
                            {
                                double F = g * 1 / d;
                                fx += F * dx;
                                fy += F * dy;
                            }
                        }

                        //Apply force to Particles velocity
                        //a.Velocity = new((float)(a.VX + fx) * 0.5f, (float)(a.VY + fy) * 0.5f);
                        a.VX = (a.VX + fx) * 0.5;
                        a.VY = (a.VY + fy) * 0.5;
                        //a.Position = new((float)(a.X + a.VX), (float)(a.Y + a.VY));
                        a.X += a.VX;
                        a.Y += a.VY;

                        //Apply velocity to Particles position
                        //If Wrapping,
                        if (Wrap)
                        {
                            if (a.X < 0)
                            {
                                a.X += Width;
                                //a.Position = new((float)(a.X + Width), (float)a.Y);
                            }
                            else if (a.X >= Width)
                            {
                                a.X -= Width;
                                //a.Position = new((float)(a.X - Width), (float)a.Y);
                            }

                            if (a.Y < 0)
                            {
                                //a.Position = new((float)a.X, (float)(a.Y + Height));
                                a.Y += Height;
                            }
                            else if (a.Y >= Height)
                            {
                                //a.Position = new((float)a.X, (float)(a.Y - Height));
                                a.Y -= Height;
                            }
                        }
                        else
                        {
                            if (a.X < atomSource.Diameter)
                            {
                                //a.Velocity = new((float)-a.VX, (float)a.VY);
                                //a.Position = new((float)atomSource.Diameter, (float)a.Y);
                                a.VX = -a.VX;
                                a.X = atomSource.Diameter;
                            }
                            else if (a.X >= Width - atomSource.Diameter)
                            {
                                //a.Velocity = new((float)-a.VX, (float)a.VY);
                                //a.Position = new((float)(Width - atomSource.Diameter), (float)a.Y);
                                a.VX = -a.VX;
                                a.X = Width - atomSource.Diameter;
                            }

                            if (a.Y < atomSource.Diameter)
                            {
                                //a.Velocity = new((float)a.VX, (float)-a.VY);
                                //a.Position = new((float)a.X, (float)atomSource.Diameter);
                                a.VY = -a.VY;
                                a.Y = atomSource.Diameter;
                            }
                            else if (a.Y >= Height - atomSource.Diameter)
                            {
                                //a.Velocity = new((float)a.VX, (float)-a.VY);
                                //a.Position = new((float)a.X, (float)(Height - atomSource.Diameter));
                                a.VY = -a.VY;
                                a.Y = Height - atomSource.Diameter;
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region Atoms
        public void AddAtom(Atom atom)
        {
            if (Atoms.Contains(atom)) throw new InvalidAtomException("The atom already exists.");
            Atoms.Add(atom);
            UniverseAtomAdded?.Invoke(this, new UniverseEventArgs(atom));
        }
        public void RemoveAtom(Atom atom)
        {
            if (!Atoms.Contains(atom)) throw new InvalidAtomException("The atom doesn't exists.");
            Atoms.Remove(atom);
            UniverseAtomRemoved?.Invoke(this, new(atom));
        }
        #endregion

        #region Particles
        public void ResetAllParticles()
        {
            foreach (Atom atom in Atoms)
                atom.ResetParticles();
        }
        #endregion
    }
}