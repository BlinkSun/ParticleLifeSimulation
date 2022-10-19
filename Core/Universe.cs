namespace ParticleLifeSimulation.Core
{
    public class Universe
    {
        #region Events
        //Universe's Events
        public event EventHandler<EventArgs>? UniverseSizeChanged;
        public event EventHandler<EventArgs>? UniverseWrapChanged;
        public event EventHandler<EventArgs>? UniverseFrictionChanged;
        public event EventHandler<EventArgs>? UniverseFlatForceChanged;
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
                if (Width != value.Width || Height != value.Height)
                {
                    Width = value.Width;
                    Height = value.Height;
                    if (Atoms != null) foreach (Atom atom in Atoms) atom.SetMaxSize(Width, Height);
                    UniverseSizeChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        public double Width { get; private set; }
        public double Height { get; private set; }
        public bool Wrap
        {
            get => wrap;
            set
            {
                if (wrap != value)
                {
                    wrap = value;
                    UniverseWrapChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private bool wrap;
        public bool FlatForce
        {
            get => flatForce;
            set
            {
                if (flatForce != value)
                {
                    flatForce = value;
                    UniverseFlatForceChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private bool flatForce;
        public double Friction
        {
            get => friction;
            set
            {
                if (value != friction)
                {
                    friction = value;
                    UniverseFrictionChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }
        private double friction = 0.5;
        public List<Atom> Atoms { get; private set; }
        #endregion

        #region Constructors
        public Universe(double width, double height)
        {
            Atoms = new();

            Width = width;
            Height = height;
        }
        #endregion

        #region Universe
        public void Step()
        {
            //For each Atom
            Parallel.ForEach(Atoms, (Atom atomSource) =>
            //foreach (Atom atomSource in Atoms)
            {
                //With each Atom
                Parallel.ForEach(Atoms, (Atom atomTarget) =>
                //foreach (Atom atomTarget in Atoms)
                {
                    //Get forces between AtomSource and AtomTarget
                    double g = atomSource.Forces.Find(force => force.Target == atomTarget)?.Attraction ?? double.NaN;
                    if (double.IsNaN(g)) return; //continue;

                    //For every Particles of AtomSource
                    //Parallel.For(0, atomSource.Particles.Count, (int i) =>
                    for (int i = 0; i < atomSource.Particles.Count; i++)
                    {
                        Particle a = atomSource.Particles[i];
                        double fx = 0;
                        double fy = 0;
                        //With every Particles of Atom2
                        //Parallel.For(0, atomTarget.Particles.Count, (int j) =>
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

                            // Get distance squared
                            double r2 = (dx * dx) + (dy * dy);

                            //// Normalize displacement
                            //double r = Math.Sqrt(r2);
                            //dx /= r;
                            //dy /= r;

                            //Calculate the intensity of force
                            double d = Math.Sqrt(r2);
                            if (d > 0 && d < 80)
                            {
                                double F = g * 1 / d;
                                fx += F * dx;
                                fy += F * dy;
                            }
                        }

                        //Apply force to Particles velocity
                        a.VX = (a.VX + fx) * (1.0 - friction);// * 0.5;
                        a.VY = (a.VY + fy) * (1.0 - friction);// * 0.5;
                        a.X += a.VX;
                        a.Y += a.VY;
                        //a.VX *= 1.0 - friction;
                        //a.VY *= 1.0 - friction;

                        //Apply Edge Collision or Wrapping to Particles position
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
                });
            });
        }
        #endregion

        #region Atoms
        public void AddAtom(Atom atom)
        {
            if (!Atoms.Contains(atom))
            {
                Atoms.Add(atom);
                UniverseAtomAdded?.Invoke(this, new UniverseEventArgs(atom));
            }
        }
        public void RemoveAtom(Atom atom)
        {
            if (Atoms.Contains(atom))
            {
                atom.Dispose();
                Atoms.Remove(atom);
                UniverseAtomRemoved?.Invoke(this, new(atom));
            }
        }
        #endregion

        #region Particles
        public void ResetParticles() => Atoms.ForEach(atom => atom.ResetParticles());
        public int ParticlesCount() => Atoms.Sum(atom => atom.Particles.Count);
        #endregion
    }
}