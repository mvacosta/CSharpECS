using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace App.ECS
{
    /// <summary>
    /// BaseWorld will be the base of all Worlds that make use of entities. Entity-Component management is handled via ComponentManager.
    /// Worlds request entities from ECSManager and will hand them back when they are no longer needed.
    /// Worlds will also manage their own BaseSystems and will be the point of interaction between each BaseSystem and ComponentManager.
    /// </summary>
    public sealed class ECSWorld : AbstractDisposable, IReusable
    {
        private HashSet<Entity> entities = null;
        private HashSet<AbstractSystem> systems = null;
        private ComponentManager components = null;
        private ECSManager ecsManager = null;

        public bool IsRetired { get; private set; }
        public ComponentManager Components => components;

        public ECSWorld()
        {
            ecsManager = App.ECSManager;
            entities = new HashSet<Entity>();
            systems = new HashSet<AbstractSystem>();
            components = new ComponentManager();
        }

        protected override void DisposeManagedResources()
        {
            ecsManager.ReturnEntities(entities);

            foreach (AbstractSystem system in systems)
                system.Dispose();

            systems.Clear();
            systems = null;

            components.Dispose();
            components = null;
        }

        protected override void DisposeUnmanagedResources()
        {
            // Entities are managed by ECSManager so return them first
            if(ecsManager != null && !IsDisposed)
                ecsManager.ReturnEntities(entities);

            ecsManager = null;

            entities?.Clear();
            entities = null;
        }

        public void TEST()
        {
            Texture2D tex = App.Content.Load<Texture2D>("TestTexture");

            float w = App.GraphicsDeviceManager.PreferredBackBufferWidth;
            float h = App.GraphicsDeviceManager.PreferredBackBufferHeight;
            Random test = new Random();
            foreach (Entity entity in entities)
            {
                Components.AttachComponent(entity, new Transform(new Vector3((float)test.NextDouble() * w,
                                                                             (float)test.NextDouble() * h, 0.0f),
                                                                 Quaternion.Identity,
                                                                 new Vector3(1.0f, 1.0f, (float)test.NextDouble() * 0.8f + 0.2f)));

                Components.AttachComponent(entity, new Sprite(tex,
                                                              new Color((float)test.NextDouble(),
                                                                        (float)test.NextDouble(),
                                                                        (float)test.NextDouble(), 1.0f)));
            }
        }

        public void Retire()
        {
            foreach (AbstractSystem system in systems)
                system.Retire();

            components.Retire();

            ecsManager.ReturnEntities(entities);
        }

        public T AddSystem<T>() where T : AbstractSystem, new()
        {
            T system = new T();

            if(systems.Add(system))
            {
                system.WorldInitialize(this);
                return system;
            }

            return null; // This should return the system if it already has one (also not create an empty one first...)
        }

        public void ReceiveEntities(HashSet<Entity> newEntities)
        {
            entities.UnionWith(newEntities);

            components.AttachComponents(entities, new EntityName(EntityName.DEFAULT_NAME));
        }
    }
}
