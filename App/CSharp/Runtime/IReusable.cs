namespace App
{
    /// <summary>
    /// For systems & structures that are meant to be retired and then reused.
    /// </summary>
    public interface IReusable
    {
        /// <summary>
        /// Check to see if reusable class has been retired or not.
        /// </summary>
        bool IsRetired { get; }

        /// <summary>
        /// Reset whichever properties need to be reset so that the object can be reused. Should set IsRetired to true.
        /// </summary>
        void Retire();
    }
}
