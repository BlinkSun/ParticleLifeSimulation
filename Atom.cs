namespace ParticleLifeSimulation
{
    public class Atom
    {
        #region Properties
        public string Name { get; set; }
        public int Number { get; set; }
        public Color Color { get; set; }
        public double MaxWidth { get; set; }
        public double MaxHeight { get; set; }
        public double Radius { get; set; }
        public double Diameter { get { return 2 * this.Radius; } }
        public List<Particle> Particles { get; set; }
        public Dictionary<string, Force> Forces { get; set; }
        #endregion

        #region Constructors
        public Atom(string name, int number, Color color, double maxWidth = 0.0, double maxHeight = 0.0, double radius = 2.5)
        {
            // Init Atom
            this.Name = name;
            this.Number = number;
            this.Color = color;
            this.MaxWidth = maxWidth;
            this.MaxHeight = maxHeight;
            this.Radius = radius;

            // Init Forces
            this.Forces = new Dictionary<string, Force>();
            this.Forces.Add($"{name} -> {name}", new Force(this, this));

            // Init Particles
            this.Particles = new List<Particle>();
            this.BuildParticles(this.Number, this.MaxWidth, this.MaxHeight);
        }
        #endregion

        #region Particles
        public void BuildParticles(int number, double maxWidth, double maxHeight)
        {
            this.Particles.Clear();
            double xOffSet = maxWidth * 0.1;
            double yOffSet = maxHeight * 0.1;
            for (int i = 0; i < number; i++)
            {
                double x = Random.Shared.NextDouble() * (maxWidth - 2 * xOffSet) + xOffSet;
                double y = Random.Shared.NextDouble() * (maxHeight - 2 * yOffSet) + yOffSet;
                this.Particles.Add(new(x, y));
            }
        }
        public void ResetParticles()
        {
            this.BuildParticles(this.Number, this.MaxWidth, this.MaxHeight);
        }
        #endregion

        #region Forces
        public Force? GetForce(Atom atom)
        {
            if (this.Forces.ContainsKey($"{this.Name} -> {atom.Name}"))
                return this.Forces[$"{this.Name} -> {atom.Name}"];
            else return null;
        }
        public Force? GetForce(string name)
        {
            if (this.Forces.ContainsKey($"{this.Name} -> {name}"))
                return this.Forces[$"{this.Name} -> {name}"];
            else return null;
        }
        public double GetForceValue(Atom atom)
        {
            if (this.Forces.ContainsKey($"{this.Name} -> {atom.Name}"))
                return this.Forces[$"{this.Name} -> {atom.Name}"].Value;
            else return double.NaN;
        }
        public double GetForceValue(string name)
        {
            if (this.Forces.ContainsKey($"{this.Name} -> {name}"))
                return this.Forces[$"{this.Name} -> {name}"].Value;
            else return double.NaN;
        }
        public void AddForce(Force force)
        {
            if (force == null) return;
            if (this.Forces.ContainsKey(force.Name)) return;
            this.Forces.Add(force.Name, force);
        }
        public void AddForceWith(Atom atom, double force, bool reciproc = true)
        {
            if (atom == null) return;
            if (this.Forces.ContainsKey($"{this.Name} -> {atom.Name}")) return;
            this.Forces.Add($"{this.Name} -> {atom.Name}", new Force(this, atom, force));
            if (reciproc) atom.AddForceWith(this, force, false);
        }
        public void AddRandomForceWith(Atom atom, bool reciproc = true)
        {
            if (atom == null) return;
            if (this.Forces.ContainsKey($"{this.Name} -> {atom.Name}")) return;
            this.Forces.Add($"{this.Name} -> {atom.Name}", new Force(this, atom));
            if(reciproc) atom.AddRandomForceWith(this, false);
        }
        public void RemoveForce(Force force)
        {
            if (force == null) return;
            this.Forces.Remove(force.Name);
        }
        public void RemoveForceWith(Atom atom, bool reciproc = true)
        {
            if (atom == null) return;
            this.Forces.Remove($"{this.Name} -> {atom.Name}");
            if (reciproc) atom.RemoveForceWith(this, false);
        }
        public void UpdateForce(Force force)
        {
            if (force == null) return;
            if (!this.Forces.ContainsKey(force.Name)) return;
            this.Forces[force.Name] = force;
        }
        public void UpdateForceWith(Atom atom, double value, bool reciproc = true)
        {
            if (atom == null) return;
            if (!this.Forces.ContainsKey($"{this.Name} -> {atom.Name}")) return;
            this.Forces[$"{this.Name} -> {atom.Name}"].Value = value;
            if (reciproc) atom.UpdateForceWith(this, value, false);
        }
        public void UpdateRandomForceWith(Atom atom, bool reciproc = true)
        {
            if (atom == null) return;
            if (!this.Forces.ContainsKey($"{this.Name} -> {atom.Name}")) return;
            Force force = this.Forces[$"{this.Name} -> {atom.Name}"];
            this.Forces[force.Name] = new Force(this, atom);
            if (reciproc) atom.UpdateRandomForceWith(this, false);
        }
        #endregion
    }
}
