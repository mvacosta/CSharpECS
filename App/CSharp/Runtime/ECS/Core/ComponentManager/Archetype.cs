using System;
using System.Collections.Generic;

namespace App.ECS
{
    /// <summary>
    /// An immutable struct that will contain all entities & components that make up an archetype for systems to use data from.
    /// </summary>
    public readonly struct Archetype
    {
        /// <summary>
        /// How many entities make up this archetype.
        /// </summary>
        public int EntityCount => Entities.Count;

        /// <summary>
        /// The entities that make up this archetype.
        /// </summary>
        public HashSet<Entity> Entities { get; }

        /// <summary>
        /// The components that are associated with these entities.
        /// </summary>
        public Dictionary<Type, List<IComponent>> Components { get; }

        public Archetype(HashSet<Entity> entities, Dictionary<Type, List<IComponent>> components)
        {
            Entities = entities;
            Components = components;
        }

        public List<T> GetComponents<T>() where T : struct, IComponent<T>
        {
            Type t = typeof(T);
            List<T> c = new List<T>(Components[t].Count);
            for (int i = 0; i < Components[t].Count; i++)
            {
                c.Add((T)Components[t][i]);
            }

            return c;
        }
    }
}
