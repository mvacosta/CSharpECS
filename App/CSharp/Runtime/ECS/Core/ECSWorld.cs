using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace App.ECS
{
    public sealed class ECSWorld : AbstractDisposable, IReusable
    {
        private HashSet<Entity> entities = new();
        private Dictionary<Type, AbstractSystem> systems = new();
        private ComponentManager components = new();
        private ECSManager ecsManager = App.ECSManager;

        public string WorldName { get; set; } = "New World";
        public bool IsRetired { get; private set; } = false;
        public ComponentManager Components => components;

        public void TEST()
        {
            Texture2D tex = App.Content.Load<Texture2D>("TestTexture");

            float w = App.GraphicsDeviceManager.PreferredBackBufferWidth;
            float h = App.GraphicsDeviceManager.PreferredBackBufferHeight;
            var test = new Random();
            foreach (var entity in entities)
            {
                Components.AttachComponent(entity, new Transform(new Vector3((float)test.NextDouble() * w,
                                                                             (float)test.NextDouble() * h, 0.0f),
                                                                 new Vector3(1.0f, 1.0f, (float)test.NextDouble() * 0.8f + 0.2f)));

                Components.AttachComponent(entity, new Sprite(tex, new Color((float)test.NextDouble(),
                                                                             (float)test.NextDouble(),
                                                                             (float)test.NextDouble(), 1.0f)));
            }
        }

        protected override void DisposeManagedResources()
        {
            ecsManager.ReturnEntities(entities);

            foreach (var system in systems.Values)
            {
                system.Dispose();
            }

            systems.Clear();
            systems = null;

            components.Dispose();
            components = null;
        }

        protected override void DisposeUnmanagedResources()
        {
            // Entities are managed by ECSManager so return them first
            if(ecsManager != null && !IsDisposed)
            {
                ecsManager.ReturnEntities(entities);
            }

            ecsManager = null;

            entities?.Clear();
            entities = null;
        }

        public void Retire()
        {
            foreach (var system in systems.Values)
            {
                system.Retire();
            }

            components.Retire();

            ecsManager.ReturnEntities(entities);
        }

        public T AddSystem<T>() where T : AbstractSystem, new()
        {
            Type type = typeof(T);
            if (!systems.ContainsKey(type))
            {
                systems.Add(type, new T());
            }

            if (systems[type].IsRetired)
            {
                systems[type].Initialize(this);
            }

            return systems[type] as T;
        }

        public bool RemoveSystem<T>() where T : AbstractSystem, new()
        {
            Type type = typeof(T);
            if (systems.ContainsKey(type) && !systems[type].IsRetired)
            {
                systems[type].Retire();
            }

            return false;
        }

        public void ReceiveEntities(HashSet<Entity> newEntities)
        {
            entities.UnionWith(newEntities);

            components.AttachComponents(entities, new EntityName(EntityName.DEFAULT_NAME));
        }
    }
}
