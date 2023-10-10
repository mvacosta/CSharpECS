using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ECSWorld
    {
        internal abstract class ComponentContainer : AbstractDisposable
        {
            internal abstract void DetachComponent(Entity entity);
            internal abstract void DetachRangeComponents(HashSet<Entity> entities);
            internal abstract void DetachAllComponents();
        }

        internal sealed class ComponentContainer<T> : ComponentContainer where T : IComponent<T>
        {
            private List<int> openIndices = new();

            internal List<T> Components = new();
            internal Dictionary<Entity, int> EntityIndices = new();

            protected override void DisposeManagedResources()
            {
                openIndices.Clear();
                openIndices = null;

                EntityIndices.Clear();
                EntityIndices = null;

                Components.Clear();
                Components = null;
            }

            internal T GetComponent(Entity entity)
            {
                return Components[EntityIndices[entity]];
            }

            internal void SetComponent(Entity entity, T component)
            {
                Components[EntityIndices[entity]] = component;
            }

            internal void SetComponents(Dictionary<Entity, T> pairs)
            {
                foreach (var entity in pairs.Keys)
                {
                    Components[EntityIndices[entity]] = pairs[entity];
                }
            }

            internal T AttachComponent(Entity entity, T value)
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

                return Components[EntityIndices[entity]];
            }

            internal void AttachRangeComponents(HashSet<Entity> entities, T value)
            {
                foreach (var entity in entities)
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

            internal void AttachRangeComponents(Dictionary<Entity, T> pairs)
            {
                foreach (var entity in pairs.Keys)
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

            internal override void DetachComponent(Entity entity)
            {
                openIndices.Add(EntityIndices[entity]);
                EntityIndices.Remove(entity);
            }

            internal override void DetachRangeComponents(HashSet<Entity> entities)
            {
                foreach (var entity in entities)
                {
                    DetachComponent(entity);
                }
            }

            internal override void DetachAllComponents()
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
