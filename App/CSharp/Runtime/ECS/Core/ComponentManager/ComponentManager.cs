using System;
using System.Collections.Generic;

namespace App.ECS
{
    /// <summary>
    /// Component Manager keeps tracks of different components and which entity they're attached to.
    /// </summary>
    public sealed partial class ComponentManager : AbstractDisposable, IReusable
    {
        private Dictionary<Type, ComponentContainer> components;

        public bool IsRetired { get; private set; }

        public ComponentManager()
        {
            components = new Dictionary<Type, ComponentContainer>();
        }

        protected override void DisposeManagedResources()
        {
            foreach (var c in components.Values)
            {
                c.Dispose();
            }

            components.Clear();
            components = null;
        }

        public void Retire()
        {
            foreach (var c in components.Values)
            {
                c.Retire();
            }
        }

        /// <summary>
        /// Add new type of tracked component.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        private void AddNewComponentType<T>() where T : struct, IComponent<T>
        {
            if (!components.ContainsKey(typeof(T)))
                components.Add(typeof(T), new ComponentContainer());
        }

        /// <summary>
        /// Attach the default component to this entity. <b>Note:</b> most defaults for IComponent structs are pre-zeroed.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entity">The entity we want to attach a component to.</param>
        /// <returns>The default attached component.</returns>
        public T AttachComponent<T>(Entity entity) where T : struct, IComponent<T>
        {
            return AttachComponent<T>(entity, default);
        }

        /// <summary>
        /// Attach this component to this entity.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entity">The entity we want to attach this component to.</param>
        /// <param name="component">The component we want to attach to this entity.</param>
        /// <returns>The newly attached component.</returns>
        public T AttachComponent<T>(Entity entity, T component) where T : struct, IComponent<T>
        {
            AddNewComponentType<T>();

            return components[typeof(T)].AttachComponent(entity, component);
        }

        /// <summary>
        /// Attach the default component to these entities. <b>Note:</b> most defaults for IComponent structs are pre-zeroed.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entities">A hashset of entities we want to attach the default component to.</param>
        public void AttachComponents<T>(HashSet<Entity> entities) where T : struct, IComponent<T>
        {
            AttachComponents(entities, (T)default);
        }

        /// <summary>
        /// Attach the same component to these entities.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entities">A hashset of entities we want to attach this component to.</param>
        /// <param name="component">The component value we want to attach to the entities.</param>
        public void AttachComponents<T>(HashSet<Entity> entities, T component) where T : struct, IComponent<T>
        {
            AddNewComponentType<T>();

            components[typeof(T)].AttachRangeComponents(entities, component);
        }

        /// <summary>
        /// Attach components to entity paired in dictionary.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="pairs">A dictionary of entities paired with the component to attach to it.</param>
        public void AttachComponents<T>(Dictionary<Entity, T> pairs) where T : struct, IComponent<T>
        {
            AddNewComponentType<T>();

            components[typeof(T)].AttachRangeComponents(pairs);
        }

        /// <summary>
        /// Detach the component type from this entity.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entities">The hashset of entities we want to detach the component type from.</param>
        public void DetachComponent<T>(Entity entity) where T : struct, IComponent<T>
        {
            components[typeof(T)].DetachComponent(entity);
        }

        /// <summary>
        /// Detach the component type from these entities.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entities">The hashset of entities we want to detach the component type from.</param>
        public void DetachComponents<T>(HashSet<Entity> entities) where T : struct, IComponent<T>
        {
            components[typeof(T)].DetachRangeComponents(entities);
        }

        /// <summary>
        /// Detach the component type from all entities that it is attached to.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        public void DetachAllComponents<T>() where T : struct, IComponent<T>
        {
            components[typeof(T)].DetachAllComponents();
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
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entity">The entity we want to get the component from.</param>
        /// <returns>The component attached to this entity. <b>Note:</b> this will return a pre-zero
        /// default component if there is not one attached to the entity.</returns>
        public T GetComponent<T>(Entity entity) where T : struct, IComponent<T>
        {
            return components[typeof(T)].GetComponent<T>(entity);
        }

        /// <summary>
        /// Sets the component for this entity.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="entity">The entity whose component we want to set.</param>
        /// <param name="value">The new value of the component.</param>
        public void SetComponent<T>(Entity entity, T value) where T : struct, IComponent<T>
        {
            components[typeof(T)].SetComponent(entity, value);
        }

        /// <summary>
        /// Sets the components to these values based on this hashset of EntityComponentPairs.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <param name="pairs">A hashset of EntityComponentPairs that contains entities with the new component values.</param>
        public void SetRangeComponents<T>(Dictionary<Entity, T> pairs) where T : struct, IComponent<T>
        {
            components[typeof(T)].SetComponents(pairs);
        }
    }
}
