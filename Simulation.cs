namespace ParticleLifeSimulation
{
    public class Simulation
    {
        #region Events
        public event EventHandler<AtomEventArgs>? AtomAdded;
        public event EventHandler<AtomEventArgs>? AtomRemoved;
        public event EventHandler<AtomEventArgs>? AtomUpdated;
        #endregion

        #region Properties
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Wrap { get; set; }
        public bool Contrast { get; set; }
        public Dictionary<string, Atom> Atoms { get; set; }
        #endregion

        #region Constructors
        public Simulation(double width, double height, bool wrap = false)
        {
            // Init Simulation
            this.Width = width;
            this.Height = height;
            this.Wrap = wrap;

            // Init Atoms
            this.Atoms = new Dictionary<string, Atom>();
            //this.AddAtom("Default", 200, Color.White);

            // Init ???
        }
        #endregion

        #region Atoms
        public bool IsAtomExist(Atom atom)
        {
            return IsAtomExist(atom.Name);
        }
        public bool IsAtomExist(string name)
        {
            return Atoms.ContainsKey(name);
        }
        public Atom? GetAtom(string name)
        {
            if (!this.Atoms.ContainsKey(name)) return null;
            return this.Atoms[name];
        }
        public void AddAtom(string name, int number, Color color, double radius = 2.5)
        {
            if (this.Atoms.ContainsKey(name)) return;
            Atom atom = new(name, number, color, this.Width, this.Height, radius);
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                atom.AddRandomForceWith(keyValuePair.Value);
            }
            this.Atoms.Add(atom.Name, atom);
            AtomAdded?.Invoke(this, new AtomEventArgs(atom));
        }
        public void AddAtom(Atom atom)
        {
            if (atom == null || this.Atoms.ContainsKey(atom.Name)) return;
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                atom.AddRandomForceWith(keyValuePair.Value);
            }
            this.Atoms.Add(atom.Name, atom);
            AtomAdded?.Invoke(this, new AtomEventArgs(atom));
        }
        public void RemoveAtom(string name)
        {
            if (!this.Atoms.ContainsKey(name)) return;
            Atom atom = this.Atoms[name];
            this.Atoms.Remove(name);
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                atom.RemoveForceWith(keyValuePair.Value);
            }
            AtomRemoved?.Invoke(this, new AtomEventArgs(atom));
        }
        public void RemoveAtom(Atom atom)
        {
            if (atom == null || !this.Atoms.ContainsKey(atom.Name)) return;
            this.RemoveAtom(atom.Name);
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                atom.RemoveForceWith(keyValuePair.Value);
            }
            AtomRemoved?.Invoke(this, new AtomEventArgs(atom));
        }
        #endregion

        #region Particles
        public void ResetParticles(Atom atom)
        {
            if (atom == null) return;
            atom.ResetParticles();
        }
        public void ResetAllParticles()
        {
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                Atom atom = keyValuePair.Value;
                atom.ResetParticles();
            }
        }
        #endregion

        #region Simulation
        public void Step()
        {
            //For each Atom
            foreach (KeyValuePair<string, Atom> keyValuePair1 in this.Atoms)
            {
                Atom atom1 = keyValuePair1.Value;
                //With each Atom
                foreach (KeyValuePair<string, Atom> keyValuePair2 in this.Atoms)
                {
                    Atom atom2 = keyValuePair2.Value;

                    //Get forces between Atom1 and Atom2
                    double g = atom1.GetForceValue(atom2);
                    if (double.IsNaN(g)) continue;

                    //For every Particles of Atom1
                    for (int i = 0; i < atom1.Particles.Count; i++)
                    {
                        Particle a = atom1.Particles[i];
                        double fx = 0;
                        double fy = 0;
                        //With every Particles of Atom2
                        for (int j = 0; j < atom2.Particles.Count; j++)
                        {
                            Particle b = atom2.Particles[j];

                            //Calculate delta (distance)
                            double dx = a.X - b.X;
                            double dy = a.Y - b.Y;

                            //If Wrapping
                            if (this.Wrap)
                            {
                                if (dx > this.Width * 0.5)
                                {
                                    dx -= this.Width;
                                }
                                else if (dx < -this.Width * 0.5)
                                {
                                    dx += this.Width;
                                }

                                if (dy > this.Height * 0.5)
                                {
                                    dy -= this.Height;
                                }
                                else if (dy < -this.Height * 0.5)
                                {
                                    dy += this.Height;
                                }
                            }

                            //Calculate the intensity of force
                            double d = Math.Sqrt(dx * dx + dy * dy);
                            if (d > 0 && d < 80)
                            {
                                double F = (g * 1) / d;
                                fx += F * dx;
                                fy += F * dy;
                            }
                        }

                        //Apply force to Particles velocity
                        a.VX = (a.VX + fx) * 0.5;
                        a.VY = (a.VY + fy) * 0.5;
                        a.X += a.VX;
                        a.Y += a.VY;

                        //Apply velocity to Particles position
                        //If Wrapping,
                        if (this.Wrap)
                        {
                            if (a.X < 0)
                            {
                                a.X += this.Width;
                            }
                            else if (a.X >= this.Width)
                            {
                                a.X -= this.Width;
                            }

                            if (a.Y < 0)
                            {
                                a.Y += this.Height;
                            }
                            else if (a.Y >= this.Height)
                            {
                                a.Y -= this.Height;
                            }
                        }
                        else
                        {
                            if (a.X < atom1.Diameter)
                            {
                                a.VX = -a.VX;
                                a.X = atom1.Diameter;
                            }
                            else if (a.X >= this.Width - atom1.Diameter)
                            {
                                a.VX = -a.VX;
                                a.X = this.Width - atom1.Diameter;
                            }

                            if (a.Y < atom1.Diameter)
                            {
                                a.VY = -a.VY;
                                a.Y = atom1.Diameter;
                            }
                            else if (a.Y >= this.Height - atom1.Diameter)
                            {
                                a.VY = -a.VY;
                                a.Y = this.Height - atom1.Diameter;
                            }
                        }
                    }
                }
            }
        }
		public void Draw(Graphics graphics)
		{
            foreach (KeyValuePair<string, Atom> keyValuePair in this.Atoms)
            {
                Atom atom = keyValuePair.Value;
                foreach (Particle particle in atom.Particles)
                {
                    Color fill, draw;
                    if (this.Contrast)
                    {
                        fill = ControlPaint.Light(atom.Color);
                        draw = ControlPaint.Dark(atom.Color);
                    }
                    else
                    {
                        fill = ControlPaint.Dark(atom.Color);
                        draw = ControlPaint.Light(atom.Color);
                    }
                    graphics.FillEllipse(new SolidBrush(fill), (float)particle.X, (float)particle.Y, (float)atom.Diameter, (float)atom.Diameter);
                    graphics.DrawEllipse(new Pen(draw, 0.1f), (float)particle.X, (float)particle.Y, (float)atom.Diameter, (float)atom.Diameter);
                }
            }
		}
        #endregion
	}

    public class AtomEventArgs : EventArgs
    {
        public Atom Atom { get; set; }
        public AtomEventArgs(Atom atom) => this.Atom = atom;
    }
}