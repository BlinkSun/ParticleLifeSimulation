namespace ParticleLifeSimulation.Core
{
    public class Force : IDisposable
    {
        #region Events
        /// <summary>
        /// Occurs when the force name have changed.
        /// </summary>
        public event EventHandler<ForceEventArgs>? ForceNameChanged;
        /// <summary>
        /// Occurs when the force value have changed.
        /// </summary>
        public event EventHandler<ForceEventArgs>? ForceValueChanged;
        #endregion

        #region Constantes
        private const double MINVALUE = -1.0;
        private const double MAXVALUE = 1.0;
        #endregion

        #region Properties
        private string name;
        private double value;
        /// <summary>
        /// Get or Set the force value. (Range [-1.0, 1.0] inclusive)
        /// </summary>
        /// <value>
        /// The value. (Range [-1.0, 1.0] inclusive)
        /// </value>
        public double Value
        {
            get => value;
            set
            {
                if (this.value != Math.Clamp(value, MINVALUE, MAXVALUE))
                {
                    this.value = Math.Clamp(value, MINVALUE, MAXVALUE);
                    ForceValueChanged?.Invoke(this, new());
                }
            }
        }
        /// <summary>
        /// Gets or sets the force name.
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
                    ForceNameChanged?.Invoke(this, new());
                }
            }
        }
        /// <summary>
        /// Gets the atom target.
        /// </summary>
        /// <value>
        /// The atom target.
        /// </value>
        public Atom AtomTarget { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Force"/> class with random force value. Range [-1.0, 1.0]
        /// </summary>
        /// <param name="atomTarget">The Atom target.</param>
        /// <param name="random">The pseudo-random number generator.</param>
        public Force(Atom atomTarget)
        {
            name = atomTarget.Name;
            value = (Random.Shared.NextDoubleInclusive() * (MAXVALUE - MINVALUE)) + MINVALUE;
            AtomTarget = atomTarget;
            AtomTarget.AtomNameChanged += new((s, ev) => Name = AtomTarget.Name);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Force"/> with defined name and force value. 
        /// </summary>
        /// <param name="atomTarget">The Atom target.</param>
        /// <param name="value">The force value. (Range [-1.0, 1.0] inclusive)</param>
        public Force(Atom atomTarget, double value) : this(atomTarget) => this.value = value;
        #endregion

        #region Force
        /// <summary>
        /// Reset random force value. (Range [-1.0, 1.0] inclusive)
        /// </summary>
        public void ResetRandomForce() => Value = (Random.Shared.NextDoubleInclusive() * (MAXVALUE - MINVALUE)) + MINVALUE;
        #endregion

        #region Finalize
        public void Dispose()
        {
            ForceNameChanged = null;
            ForceValueChanged = null;
            AtomTarget = null;
            //GC.SuppressFinalize(this);
        }
        #endregion
    }
}