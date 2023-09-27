using System;
using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ComponentManager
    {
        #region GetArchetypes Public Methods

        /// <summary>
        /// An archetype made up of one component. // TODO: Is this one needed?
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T>()
            where T : struct, IComponent<T>
        {
            return InternalGetArchetype(typeof(T));
        }

        /// <summary>
        /// An archetype made up of two components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2));
        }

        /// <summary>
        /// An archetype made up of three components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2), typeof(T3));
        }

        /// <summary>
        /// An archetype made up of four components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3, T4>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2), typeof(T3), typeof(T4));
        }

        /// <summary>
        /// An archetype made up of five components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T5">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2),
                                        typeof(T3), typeof(T4), typeof(T5));
        }

        /// <summary>
        /// An archetype made up of six components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T5">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T6">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2), typeof(T3),
                                        typeof(T4), typeof(T5), typeof(T6));
        }

        /// <summary>
        /// An archetype made up of seven components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T5">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T6">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T7">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
            where T7 : struct, IComponent<T7>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2), typeof(T3),
                                        typeof(T4), typeof(T5), typeof(T6), typeof(T7));
        }

        /// <summary>
        /// An archetype made up of eight components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T5">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T6">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T7">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T8">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public Archetype GetArchetype<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
            where T7 : struct, IComponent<T7>
            where T8 : struct, IComponent<T8>
        {
            return InternalGetArchetype(typeof(T1), typeof(T2), typeof(T3), typeof(T4),
                                        typeof(T5), typeof(T6), typeof(T7), typeof(T8));
        }

        // If more types end up being needed, add them

        #endregion

        /// <summary>
        /// How component manager actually grabs and returns our entities that make up an archetype.
        /// </summary>
        /// <param name="groupedTypes">An array of types that make up an entity archetype.</param>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        private Archetype InternalGetArchetype(params Type[] groupedTypes)
        {
            var entities = new HashSet<Entity>();
            var components = new Dictionary<Type, List<IComponent>>();

            // Get entities that make up this archetype
            bool firstSet = false;
            for (int i = 0; i < groupedTypes.Length; i++)
            {
                if (this.components.ContainsKey(groupedTypes[i]))
                {
                    if (!firstSet)
                    {
                        firstSet = true;
                        entities.UnionWith(this.components[groupedTypes[i]].EntityIndices.Keys);
                    }
                    else
                        entities.IntersectWith(this.components[groupedTypes[i]].EntityIndices.Keys);
                }
            }

            // Now get the components
            for (int i = 0; i < groupedTypes.Length; i++)
            {
                var eIndices = this.components[groupedTypes[i]].EntityIndices;
                var cList = this.components[groupedTypes[i]].Components;

                components.Add(groupedTypes[i], new List<IComponent>());
                foreach (Entity entity in entities)
                {
                    components[groupedTypes[i]].Add(cList[eIndices[entity]]);
                }
            }

            return new Archetype(entities, components);
        }
    }
}
