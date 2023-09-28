using System;

namespace App.ECS
{
    /// <summary>
    /// AbstractSystem will iterate through our components to compute whatever tasks are required.
    /// </summary>
    public abstract class AbstractSystem : AbstractDisposable, IReusable
    {
        public bool IsRetired { get; private set; } = true;

        public abstract void Initialize(ECSWorld world);

        public virtual void Retire() { }
    }
}
