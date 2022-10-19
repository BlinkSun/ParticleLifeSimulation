using System.Reflection;

namespace ParticleLifeSimulation.Core
{
    public static class Extensions
    {
        #region Particle        
        /// <summary>
        /// Converts to clone particle.
        /// </summary>
        /// <param name="particle">The particle to clone.</param>
        /// <returns>Clone of particle.</returns>
        public static Particle ToClone(this Particle particle) => new(particle.X, particle.Y);
        #endregion

        #region Atom        
        /// <summary>
        /// Converts to clone atom.
        /// </summary>
        /// <param name="atom">The atom to clone.</param>
        /// <returns>Clone of atom.</returns>
        public static Atom ToClone(this Atom atom) => new(atom.Name, atom.Color, atom.MaxWidth, atom.MaxHeight, atom.Radius);
        #endregion

        #region Force        
        /// <summary>
        /// Converts to clone force.
        /// </summary>
        /// <param name="force">The force to clone.</param>
        /// <returns>Clone of force.</returns>
        public static Force ToClone(this Force force) => new(force.Target, force.Radiation, force.Attraction);
        #endregion

        #region Random        
        /// <summary>
        /// Gets random double with range [0.0, 1.0] (inclusive).
        /// </summary>
        /// <param name="random">Pseudo-random number generator.</param>
        /// <returns>double [0.0, 1.0] (inclusive).</returns>
        public static double NextDoubleInclusive(this Random? random) => (random ?? Random.Shared).Next() * (1.0 / (int.MaxValue - 1.0));
        /// <summary>
        /// Gets random double with range [min, max] (inclusive).
        /// </summary>
        /// <param name="random">Pseudo-random number generator.</param>
        /// <param name="min">Minimum double.</param>
        /// <param name="max">Maximum double.</param>
        /// <returns>double [min, max] (inclusive).</returns>
        public static double NextDoubleInclusive(this Random? random, double min, double max) => (random.NextDoubleInclusive() * (max - min)) + min;
        #endregion

        #region Colors
        public static Color GetRandomKnownColor(bool systemColorsIncluded = false, bool compoundColorNamesIncluded = false) => GetKnownColors(systemColorsIncluded, compoundColorNamesIncluded).ElementAt(Random.Shared.Next(GetKnownColors(systemColorsIncluded, compoundColorNamesIncluded).Count()));
        public static IEnumerable<Color> GetKnownColors(bool systemColorsIncluded = false, bool compoundColorNamesIncluded = false)
        {
            IEnumerable<Color> knownsColors = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().Select(knownColor => Color.FromKnownColor(knownColor));
            IEnumerable<string> systemColorNames = typeof(SystemColors).GetRuntimeProperties().Select(systemColor => systemColor.Name);
            if (!systemColorsIncluded)  // SystemColors
                knownsColors = knownsColors.Where(knownColor => !systemColorNames.Contains(knownColor.Name));
            if (!compoundColorNamesIncluded)    // Compound Color Name
                knownsColors = knownsColors.Where(knownColor => knownColor.Name.Count(character => char.IsUpper(character)) == 1);
            return knownsColors.Where(color => color != Color.Transparent); // Of cours !
        }
        #endregion
    }
}