using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleLifeSimulation
{
    public class Force
    {
        public string Name { get; set; }
        //public Atom Atom1 { get; set; }
        //public Atom Atom2 { get; set; }
        public double Value { get; set; }

        public Force(Atom atom1, Atom atom2)
        {
            this.Name = $"{atom1.Name} -> {atom2.Name}";
            //this.Atom1 = atom1;
            //this.Atom2 = atom2;
            this.Value = Random.Shared.NextDouble() * 2.0 - 1.0;
        }
        public Force(Atom atom1, Atom atom2, double value): this(atom1, atom2)
        {
            this.Value = value;
        }
    }
}
