using System;
using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ComponentManager
    {
        /// <summary>
        /// Our container that will keep track of enity & component pairings.
        /// </summary>
        internal sealed class ComponentContainer : AbstractDisposable, IReusable
        {
            private List<int> openIndices = new List<int>();

            public List<IComponent> Components = new List<IComponent>();
            public Dictionary<Entity, int> EntityIndices = new Dictionary<Entity, int>();

            public bool IsRetired { get; private set; } = false;

            protected override void DisposeManagedResources()
            {
                openIndices.Clear();
                EntityIndices.Clear();
                Components.Clear();

                openIndices = null;
                Components = null;
                EntityIndices = null;
            }

            public void Retire()
            {
                DetachAllComponents();

                IsRetired = true;
            }

            public T GetComponent<T>(Entity entity) where T : struct, IComponent<T>
            {
                return (T)Components[EntityIndices[entity]];
            }

            public void SetComponent<T>(Entity entity, T component) where T : struct, IComponent<T>
            {
                Components[EntityIndices[entity]] = component;
            }

            public void SetComponents<T>(Dictionary<Entity, T> pairs) where T : struct, IComponent<T>
            {
                foreach (Entity entity in pairs.Keys)
                {
                    Components[EntityIndices[entity]] = pairs[entity];
                }
            }

            public T AttachComponent<T>(Entity entity, T value) where T : struct, IComponent<T>
            {
                if (!EntityIndices.ContainsKey(entity))
                {
                    int index = Components.Count;
                    if (openIndices.Count > 0)
                    {
                        index = openIndices[0];
                        openIndices.RemoveAt(0);
                    }

                    EntityIndices.Add(entity, index);
                    Components.Add(value);
                }

                return (T)Components[EntityIndices[entity]];
            }

            public void AttachRangeComponents<T>(HashSet<Entity> entities, T value) where T : struct, IComponent<T>
            {
                foreach (Entity entity in entities)
                {
                    if (!EntityIndices.ContainsKey(entity))
                    {
                        int index = Components.Count;
                        if (openIndices.Count > 0)
                        {
                            index = openIndices[0];
                            openIndices.RemoveAt(0);
                        }

                        EntityIndices.Add(entity, index);
                        Components.Add(value);
                    }
                }
            }

            public void AttachRangeComponents<T>(Dictionary<Entity, T> pairs) where T : struct, IComponent<T>
            {
                foreach (Entity entity in pairs.Keys)
                {
                    if (!EntityIndices.ContainsKey(entity))
                    {
                        int index = Components.Count;
                        if (openIndices.Count > 0)
                        {
                            index = openIndices[0];
                            openIndices.RemoveAt(0);
                        }

                        EntityIndices.Add(entity, index);
                        Components.Add(pairs[entity]);
                    }
                }
            }

            public void DetachComponent(Entity entity)
            {
                openIndices.Add(EntityIndices[entity]);
                EntityIndices.Remove(entity);
            }

            public void DetachRangeComponents(HashSet<Entity> entities)
            {
                foreach (Entity entity in entities)
                {
                    DetachComponent(entity);
                }
            }

            public void DetachAllComponents()
            {
                if (EntityIndices.Count > 0)
                {
                    openIndices.AddRange(EntityIndices.Values);
                    EntityIndices.Clear();
                }
            }
        }
    }
}
