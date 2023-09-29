using System;
using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ComponentManager
    {
        #region GetArchetypes Public Methods

        /// <summary>
        /// An archetype made up of one component.
        /// </summary>
        /// <typeparam name="T">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public (int count, T[]) GetArchetype<T>()
            where T : struct, IComponent<T>
        {
            Type t = typeof(T);

            var entities = new HashSet<Entity>(components[t].EntityIndices.Keys);

            T[] tArray = new T[entities.Count];

            var eIndices = components[t].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                tArray[e] = (T)components[t].Components[e];
            }

            return (entities.Count, tArray);
        }

        /// <summary>
        /// An archetype made up of two components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public (int count, T1[], T2[]) GetArchetype<T1, T2>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
        {
            Type t1 = typeof(T1), t2 = typeof(T2);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
            }

            return (entities.Count, t1Array, t2Array);
        }

        /// <summary>
        /// An archetype made up of three components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public (int count, T1[], T2[], T3[]) GetArchetype<T1, T2, T3>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
        {
            Type t1 = typeof(T1), t2 = typeof(T2), t3 = typeof(T3);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array);
        }

        /// <summary>
        /// An archetype made up of four components.
        /// </summary>
        /// <typeparam name="T1">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T2">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T3">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <typeparam name="T4">Any IComponent struct manipulable by a BaseSystem.</typeparam>
        /// <returns>A container of all of the entities and associated components that make up the requested archetype.</returns>
        public (int count, T1[], T2[], T3[], T4[]) GetArchetype<T1, T2, T3, T4>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
        {
            Type t1 = typeof(T1), t2 = typeof(T2), t3 = typeof(T3), t4 = typeof(T4);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);
            entities.IntersectWith(components[t4].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];
            T4[] t4Array = new T4[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
                t4Array[e] = (T4)components[t4].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array, t4Array);
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
        public (int count, T1[], T2[], T3[], T4[], T5[]) GetArchetype<T1, T2, T3, T4, T5>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
        {
            Type t1 = typeof(T1), t2 = typeof(T2),
                 t3 = typeof(T3), t4 = typeof(T4), t5 = typeof(T5);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);
            entities.IntersectWith(components[t4].EntityIndices.Keys);
            entities.IntersectWith(components[t5].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];
            T4[] t4Array = new T4[entities.Count];
            T5[] t5Array = new T5[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
                t4Array[e] = (T4)components[t4].Components[e];
                t5Array[e] = (T5)components[t5].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array, t4Array, t5Array);
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
        public (int count, T1[], T2[], T3[], T4[], T5[], T6[]) GetArchetype<T1, T2, T3, T4, T5, T6>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
        {
            Type t1 = typeof(T1), t2 = typeof(T2), t3 = typeof(T3),
                 t4 = typeof(T4), t5 = typeof(T5), t6 = typeof(T6);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);
            entities.IntersectWith(components[t4].EntityIndices.Keys);
            entities.IntersectWith(components[t5].EntityIndices.Keys);
            entities.IntersectWith(components[t6].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];
            T4[] t4Array = new T4[entities.Count];
            T5[] t5Array = new T5[entities.Count];
            T6[] t6Array = new T6[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
                t4Array[e] = (T4)components[t4].Components[e];
                t5Array[e] = (T5)components[t5].Components[e];
                t6Array[e] = (T6)components[t6].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array, t4Array, t5Array, t6Array);
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
        public (int count, T1[], T2[], T3[], T4[], T5[], T6[], T7[]) GetArchetype<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
            where T7 : struct, IComponent<T7>
        {
            Type t1 = typeof(T1), t2 = typeof(T2), t3 = typeof(T3),
                 t4 = typeof(T4), t5 = typeof(T5), t6 = typeof(T6), t7 = typeof(T7);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);
            entities.IntersectWith(components[t4].EntityIndices.Keys);
            entities.IntersectWith(components[t5].EntityIndices.Keys);
            entities.IntersectWith(components[t6].EntityIndices.Keys);
            entities.IntersectWith(components[t7].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];
            T4[] t4Array = new T4[entities.Count];
            T5[] t5Array = new T5[entities.Count];
            T6[] t6Array = new T6[entities.Count];
            T7[] t7Array = new T7[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
                t4Array[e] = (T4)components[t4].Components[e];
                t5Array[e] = (T5)components[t5].Components[e];
                t6Array[e] = (T6)components[t6].Components[e];
                t7Array[e] = (T7)components[t7].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array, t4Array, t5Array, t6Array, t7Array);
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
        public (int count, T1[], T2[], T3[], T4[], T5[], T6[], T7[], T8[]) GetArchetype<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : struct, IComponent<T1>
            where T2 : struct, IComponent<T2>
            where T3 : struct, IComponent<T3>
            where T4 : struct, IComponent<T4>
            where T5 : struct, IComponent<T5>
            where T6 : struct, IComponent<T6>
            where T7 : struct, IComponent<T7>
            where T8 : struct, IComponent<T8>
        {
            Type t1 = typeof(T1), t2 = typeof(T2), t3 = typeof(T3), t4 = typeof(T4),
                 t5 = typeof(T5), t6 = typeof(T6), t7 = typeof(T7), t8 = typeof(T8);

            var entities = new HashSet<Entity>(components[t1].EntityIndices.Keys);
            entities.IntersectWith(components[t2].EntityIndices.Keys);
            entities.IntersectWith(components[t3].EntityIndices.Keys);
            entities.IntersectWith(components[t4].EntityIndices.Keys);
            entities.IntersectWith(components[t5].EntityIndices.Keys);
            entities.IntersectWith(components[t6].EntityIndices.Keys);
            entities.IntersectWith(components[t7].EntityIndices.Keys);
            entities.IntersectWith(components[t8].EntityIndices.Keys);

            T1[] t1Array = new T1[entities.Count];
            T2[] t2Array = new T2[entities.Count];
            T3[] t3Array = new T3[entities.Count];
            T4[] t4Array = new T4[entities.Count];
            T5[] t5Array = new T5[entities.Count];
            T6[] t6Array = new T6[entities.Count];
            T7[] t7Array = new T7[entities.Count];
            T8[] t8Array = new T8[entities.Count];

            var eIndices = components[t1].EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                t1Array[e] = (T1)components[t1].Components[e];
                t2Array[e] = (T2)components[t2].Components[e];
                t3Array[e] = (T3)components[t3].Components[e];
                t4Array[e] = (T4)components[t4].Components[e];
                t5Array[e] = (T5)components[t5].Components[e];
                t6Array[e] = (T6)components[t6].Components[e];
                t7Array[e] = (T7)components[t7].Components[e];
                t8Array[e] = (T8)components[t8].Components[e];
            }

            return (entities.Count, t1Array, t2Array, t3Array, t4Array, t5Array, t6Array, t7Array, t8Array);
        }

        // If more types end up being needed, add them

        #endregion
    }
}
