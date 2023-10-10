namespace App.ECS
{
    /// <summary>
    /// AbstractSystem will iterate through our components to compute whatever tasks are required.
    /// </summary>
    public abstract class AbstractSystem
    {
        public bool IsActive => World != null;

        protected ECSWorld World { get; private set; } = null;

        public virtual void Enable(ECSWorld world)
        {
            World = world;
        }

        public virtual void Disable()
        {
            World = null;
        }
    }
}
