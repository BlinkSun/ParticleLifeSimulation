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
        /// Occurs when the attraction value have changed.
        /// </summary>
        public event EventHandler<ForceEventArgs>? ForceAttractionChanged;
        /// <summary>
        /// Occurs when the radiation value have changed.
        /// </summary>
        public event EventHandler<ForceEventArgs>? ForceRadiationChanged;
        #endregion

        #region Constants
        private const double MINVALUE = -1.0;
        private const double MAXVALUE = 1.0;
        #endregion

        #region Properties
        public Atom Target { get; private set; }
        public string Name { get => Target.Name; }
        public double Radiation
        {
            get => radiation;
            set
            {
                if (radiation != Math.Clamp(value, 0.0, MAXVALUE))
                {
                    radiation = Math.Clamp(value, 0.0, MAXVALUE);
                    ForceRadiationChanged?.Invoke(this, new());
                }
            }
        }
        private double radiation;
        public double Attraction
        {
            get => attraction;
            set
            {
                if (attraction != Math.Clamp(value, MINVALUE, MAXVALUE))
                {
                    attraction = Math.Clamp(value, MINVALUE, MAXVALUE);
                    ForceAttractionChanged?.Invoke(this, new());
                }
            }
        }
        private double attraction;
        #endregion

        #region Constructors
        public Force(Atom target, double radiation, double attraction)
        {
            this.radiation = radiation;
            this.attraction = attraction;
            Target = target;
            target.AtomNameChanged += Force_AtomNameChanged;
        }
        #endregion

        #region Force
        private void Force_AtomNameChanged(object? sender, EventArgs e)
        {
            ForceNameChanged?.Invoke(this, new());
        }
        #endregion

        #region Finalize
        public void Dispose()
        {
            Target.AtomNameChanged -= Force_AtomNameChanged;

            ForceNameChanged = null;
            ForceRadiationChanged = null;
            ForceAttractionChanged = null;

            GC.SuppressFinalize(this);
        }
        #endregion
    }
}