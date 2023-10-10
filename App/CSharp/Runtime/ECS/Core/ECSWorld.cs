using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ECSWorld : AbstractDisposable
    {
        private ECSManager ecsManager = App.ECSManager;

        private HashSet<Entity> entities = new();
        private Dictionary<Type, ComponentContainer> components = new();
        private Dictionary<Type, AbstractSystem> systems = new();

        private ComponentContainer<T> GetComponents<T>() where T : IComponent<T>
            => components[typeof(T)] as ComponentContainer<T>;

        public string WorldName { get; set; } = "New World";

        protected override void DisposeManagedResources()
        {
            foreach (var system in systems.Values)
            {
                system.Disable();
            }

            systems.Clear();
            systems = null;

            foreach (var component in components.Values)
            {
                component.Dispose();
            }

            components.Clear();
            components = null;
        }

        protected override void DisposeUnmanagedResources()
        {
            // Entities are managed by ECSManager so return them first
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
            var test = new Random();
            foreach (var entity in entities)
            {
                AttachComponent(entity, new Transform(new Vector3((float)test.NextDouble() * w,
                                                                  (float)test.NextDouble() * h, 0.0f),
                                                      new Vector3(1.0f, 1.0f, (float)test.NextDouble() * 0.8f + 0.2f)));

                AttachComponent(entity, new Sprite(tex, new Color((float)test.NextDouble(),
                                                                  (float)test.NextDouble(),
                                                                  (float)test.NextDouble(), 1.0f)));
            }
        }

        #region Component

        private void AddNewComponentType<T>() where T : IComponent<T>
        {
            if (!components.ContainsKey(typeof(T)))
            {
                components.Add(typeof(T), new ComponentContainer<T>());
            }
        }

        /// <summary>
        /// Attach the default component to this entity.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entity">The entity we want to attach a component to.</param>
        /// <returns>The default attached component.</returns>
        public T AttachComponent<T>(Entity entity) where T : IComponent<T>
        {
            return AttachComponent<T>(entity, default);
        }

        /// <summary>
        /// Attach this component to this entity.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entity">The entity we want to attach this component to.</param>
        /// <param name="component">The component we want to attach to this entity.</param>
        /// <returns>The newly attached component.</returns>
        public T AttachComponent<T>(Entity entity, T component) where T : IComponent<T>
        {
            AddNewComponentType<T>();

            return GetComponents<T>().AttachComponent(entity, component);
        }

        /// <summary>
        /// Attach the default component to these entities.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entities">A hashset of entities we want to attach the default component to.</param>
        public void AttachComponents<T>(HashSet<Entity> entities) where T : IComponent<T>
        {
            AttachComponents(entities, (T)default);
        }

        /// <summary>
        /// Attach the same component to these entities.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entities">A hashset of entities we want to attach this component to.</param>
        /// <param name="component">The component value we want to attach to the entities.</param>
        public void AttachComponents<T>(HashSet<Entity> entities, T component) where T : IComponent<T>
        {
            AddNewComponentType<T>();

            GetComponents<T>().AttachRangeComponents(entities, component);
        }

        /// <summary>
        /// Attach components to entities paired in the dictionary.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="pairs">A dictionary of entities paired with the component to attach to it.</param>
        public void AttachComponents<T>(Dictionary<Entity, T> pairs) where T : IComponent<T>
        {
            AddNewComponentType<T>();

            GetComponents<T>().AttachRangeComponents(pairs);
        }

        /// <summary>
        /// Detach the component type from this entity.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entities">The hashset of entities we want to detach the component type from.</param>
        public void DetachComponent<T>(Entity entity) where T : IComponent<T>
        {
            GetComponents<T>().DetachComponent(entity);
        }

        /// <summary>
        /// Detach the component type from these entities.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entities">The hashset of entities we want to detach the component type from.</param>
        public void DetachComponents<T>(HashSet<Entity> entities) where T : IComponent<T>
        {
            GetComponents<T>().DetachRangeComponents(entities);
        }

        /// <summary>
        /// Detach the component type from all entities that it is attached to.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        public void DetachAllComponents<T>() where T : IComponent<T>
        {
            GetComponents<T>().DetachAllComponents();
        }

        /// <summary>
        /// Detaches all components from this entity.
        /// </summary>
        /// <param name="entity">The entity we want to detach completely from.</param>
        public void ClearComponents(Entity entity)
        {
            foreach (var component in components.Values)
            {
                component.DetachComponent(entity);
            }
        }
        
        /// <summary>
        /// Detaches all components from these entities.
        /// </summary>
        /// <param name="entities">The hashset of entities we want to detach completely from.</param>
        public void ClearRangeComponents(HashSet<Entity> entities)
        {
            foreach (var component in components.Values)
            {
                component.DetachRangeComponents(entities);
            }
        }
        
        /// <summary>
        /// Detaches all components from all entities.
        /// </summary>
        public void ClearAllComponents()
        {
            foreach (var component in components.Values)
            {
                component.DetachAllComponents();
            }
        }

        /// <summary>
        /// Gets the component from this entity.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entity">The entity we want to get the component from.</param>
        /// <returns>The component attached to this entity. <b>Note:</b> this will return a pre-zero
        /// default component if there is not one attached to the entity.</returns>
        public T GetComponent<T>(Entity entity) where T : IComponent<T>
        {
            return GetComponents<T>().GetComponent(entity);
        }

        /// <summary>
        /// Sets the component for this entity.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="entity">The entity whose component we want to set.</param>
        /// <param name="value">The new value of the component.</param>
        public void SetComponent<T>(Entity entity, T value) where T : IComponent<T>
        {
            GetComponents<T>().SetComponent(entity, value);
        }

        /// <summary>
        /// Sets the components to these values based on this hashset of EntityComponentPairs.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <param name="pairs">A hashset of EntityComponentPairs that contains entities with the new component values.</param>
        public void SetRangeComponents<T>(Dictionary<Entity, T> pairs) where T : IComponent<T>
        {
            GetComponents<T>().SetComponents(pairs);
        }

        #endregion

        #region System

        public T AddSystem<T>() where T : AbstractSystem, new()
        {
            Type type = typeof(T);
            if (!systems.ContainsKey(type))
            {
                systems.Add(type, new T());
            }

            T system = systems[type] as T;
            if (!system.IsActive)
            {
                system.Enable(this);
            }

            return system;
        }

        public void RemoveSystem<T>() where T : AbstractSystem, new()
        {
            Type type = typeof(T);
            if (systems.ContainsKey(type) && systems[type].IsActive)
            {
                systems[type].Disable();
            }
        }

        public void ReceiveEntities(HashSet<Entity> newEntities)
        {
            entities.UnionWith(newEntities);

            AttachComponents(entities, new EntityName(EntityName.DEFAULT_NAME));
        }

        #endregion
    }
}
