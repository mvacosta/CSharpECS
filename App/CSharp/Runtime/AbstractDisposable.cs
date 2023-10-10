using System;

namespace App
{
    /// <summary><para>
    /// Almost all objects should derive from AbstractDisposable. When using this class, override <see cref="DisposeManagedResources"/>
    /// to dispose of all references used by the dervied object. Override <see cref="DisposeUnmanagedResources"/> when this class is
    /// using objects that are unmanged directly by this class, such as OS operations (writing to file, handles) or networked operations.
    /// </para>
    /// Make sure to Dispose correctly! Best practices: https://docs.microsoft.com/en-us/dotnet/api/system.idisposable?view=net-5.0
    /// </summary>
    public abstract class AbstractDisposable : IDisposable
    {
        /// <summary><para>
        /// Has this object been disposed yet?
        /// </para>
        /// Note: NEVER set this manually, <see cref="Dispose()"/> will handle it.
        /// </summary>
        public bool IsDisposed { get; private set; } = false;

        /// <summary><para>
        /// Releases mangaged resources. Most classes will override this.
        /// </para>
        /// Note: Do not call this manually, call <see cref="Dispose()"/> instead.
        /// </summary>
        protected virtual void DisposeManagedResources() { }

        /// <summary><para>
        /// Releases unmangaged resources. Only override if using OS (file systems) or Networked resources.
        /// </para>
        /// Note: Do not call this manually, call <see cref="Dispose()"/> instead.
        /// </summary>
        protected virtual void DisposeUnmanagedResources() { }

        /// <summary>
        /// Disposes this object manually by freeing up all of our managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed) return;

            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool manualDispose)
        {
            if (!IsDisposed)
            {
                if (manualDispose)
                {
                    DisposeManagedResources();
                }

                DisposeUnmanagedResources();
            }

            IsDisposed = true;
        }

        ~AbstractDisposable()
        {
            Dispose(false);
        }
    }
}
