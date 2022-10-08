namespace ParticleLifeSimulation.Core2
{
    #region Exceptions    
    /// <summary>
    /// The exception that is thrown when a method call is invalid for the object's current state.
    /// </summary>
    /// <seealso cref="System.InvalidOperationException" />
    public class InvalidUniverseException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidUniverseException"/> class.
        /// </summary>
        /// <param name="messageException">The message exception.</param>
        public InvalidUniverseException(string messageException) : base(messageException) { }
    }
    /// <summary>
    /// The exception that is thrown when a method call is invalid for the object's current state.
    /// </summary>
    /// <seealso cref="System.InvalidOperationException" />
    public class InvalidParticleException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidParticleException"/> class.
        /// </summary>
        /// <param name="messageException">The message exception.</param>
        public InvalidParticleException(string messageException) : base(messageException) { }
    }
    /// <summary>
    /// The exception that is thrown when a method call is invalid for the object's current state.
    /// </summary>
    /// <seealso cref="System.InvalidOperationException" />
    public class InvalidAtomException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidAtomException"/> class.
        /// </summary>
        /// <param name="messageException">The message exception.</param>
        public InvalidAtomException(string messageException) : base(messageException) { }
    }
    /// <summary>
    /// The exception that is thrown when a method call is invalid for the object's current state.
    /// </summary>
    /// <seealso cref="System.InvalidOperationException" />
    public class InvalidForceException : InvalidOperationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidForceException"/> class.
        /// </summary>
        /// <param name="messageException">The message exception.</param>
        public InvalidForceException(string messageException) : base(messageException) { }
    }
    #endregion
}
