using System.Collections.Generic;

namespace App.ECS
{
    public sealed partial class ECSWorld
    {
        /// <summary>
        /// Creates an archetype made up of one component.
        /// </summary>
        /// <typeparam name="T">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T[] C1) GetArchetype<T>() where T : IComponent<T>
        {
            var t = GetComponents<T>();
            var entities = new HashSet<Entity>(t.EntityIndices.Keys);

            T[] c = new T[entities.Count];

            var eIndices = t.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c[e] = t.Components[e];
            }

            return (entities.Count, c);
        }

        /// <summary>
        /// Creates an archetype made up of two components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2) GetArchetype<T1, T2>()
            where T1 : IComponent<T1> where T2 : IComponent<T2>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e];
            }

            return (entities.Count, c1, c2);
        }

        /// <summary>
        /// Creates an archetype made up of three components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3) GetArchetype<T1, T2, T3>()
            where T1 : IComponent<T1> where T2 : IComponent<T2> where T3 : IComponent<T3>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>(); var t3 = GetComponents<T3>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count]; T3[] c3 = new T3[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e]; c3[e] = t3.Components[e];
            }

            return (entities.Count, c1, c2, c3);
        }

        /// <summary>
        /// Creates an archetype made up of four components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <typeparam name="T4">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3, T4[] C4) GetArchetype<T1, T2, T3, T4>()
            where T1 : IComponent<T1> where T2 : IComponent<T2>
            where T3 : IComponent<T3> where T4 : IComponent<T4>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>();
            var t3 = GetComponents<T3>(); var t4 = GetComponents<T4>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);
            entities.IntersectWith(t4.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count];
            T3[] c3 = new T3[entities.Count]; T4[] c4 = new T4[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e];
                c3[e] = t3.Components[e]; c4[e] = t4.Components[e];
            }

            return (entities.Count, c1, c2, c3, c4);
        }

        /// <summary>
        /// Creates an archetype made up of five components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <typeparam name="T4">Components must be an IComponent.</typeparam>
        /// <typeparam name="T5">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3, T4[] C4, T5[] C5) GetArchetype<T1, T2, T3, T4, T5>()
            where T1 : IComponent<T1> where T2 : IComponent<T2> where T3 : IComponent<T3>
            where T4 : IComponent<T4> where T5 : IComponent<T5>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>(); var t3 = GetComponents<T3>();
            var t4 = GetComponents<T4>(); var t5 = GetComponents<T5>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);
            entities.IntersectWith(t4.EntityIndices.Keys); entities.IntersectWith(t5.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count]; T3[] c3 = new T3[entities.Count];
            T4[] c4 = new T4[entities.Count]; T5[] c5 = new T5[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e]; c3[e] = t3.Components[e];
                c4[e] = t4.Components[e]; c5[e] = t5.Components[e];
            }

            return (entities.Count, c1, c2, c3, c4, c5);
        }

        /// <summary>
        /// Creates an archetype made up of six components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <typeparam name="T4">Components must be an IComponent.</typeparam>
        /// <typeparam name="T5">Components must be an IComponent.</typeparam>
        /// <typeparam name="T6">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3, T4[] C4, T5[] C5, T6[] C6) GetArchetype<T1, T2, T3, T4, T5, T6>()
            where T1 : IComponent<T1> where T2 : IComponent<T2> where T3 : IComponent<T3>
            where T4 : IComponent<T4> where T5 : IComponent<T5> where T6 : IComponent<T6>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>(); var t3 = GetComponents<T3>();
            var t4 = GetComponents<T4>(); var t5 = GetComponents<T5>(); var t6 = GetComponents<T6>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);
            entities.IntersectWith(t4.EntityIndices.Keys); entities.IntersectWith(t5.EntityIndices.Keys);
            entities.IntersectWith(t6.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count]; T3[] c3 = new T3[entities.Count];
            T4[] c4 = new T4[entities.Count]; T5[] c5 = new T5[entities.Count]; T6[] c6 = new T6[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e]; c3[e] = t3.Components[e];
                c4[e] = t4.Components[e]; c5[e] = t5.Components[e]; c6[e] = t6.Components[e];
            }

            return (entities.Count, c1, c2, c3, c4, c5, c6);
        }

        /// <summary>
        /// Creates an archetype made up of seven components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <typeparam name="T4">Components must be an IComponent.</typeparam>
        /// <typeparam name="T5">Components must be an IComponent.</typeparam>
        /// <typeparam name="T6">Components must be an IComponent.</typeparam>
        /// <typeparam name="T7">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3, T4[] C4, T5[] C5, T6[] C6, T7[] C7) GetArchetype<T1, T2, T3, T4, T5, T6, T7>()
            where T1 : IComponent<T1> where T2 : IComponent<T2> where T3 : IComponent<T3>
            where T4 : IComponent<T4> where T5 : IComponent<T5>
            where T6 : IComponent<T6> where T7 : IComponent<T7>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>(); var t3 = GetComponents<T3>();
            var t4 = GetComponents<T4>(); var t5 = GetComponents<T5>();
            var t6 = GetComponents<T6>(); var t7 = GetComponents<T7>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);
            entities.IntersectWith(t4.EntityIndices.Keys); entities.IntersectWith(t5.EntityIndices.Keys);
            entities.IntersectWith(t6.EntityIndices.Keys); entities.IntersectWith(t7.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count]; T3[] c3 = new T3[entities.Count];
            T4[] c4 = new T4[entities.Count]; T5[] c5 = new T5[entities.Count];
            T6[] c6 = new T6[entities.Count]; T7[] c7 = new T7[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e]; c3[e] = t3.Components[e];
                c4[e] = t4.Components[e]; c5[e] = t5.Components[e];
                c6[e] = t6.Components[e]; c7[e] = t7.Components[e];
            }

            return (entities.Count, c1, c2, c3, c4, c5, c6, c7);
        }

        /// <summary>
        /// Creates an archetype made up of eight components.
        /// </summary>
        /// <typeparam name="T1">Components must be an IComponent.</typeparam>
        /// <typeparam name="T2">Components must be an IComponent.</typeparam>
        /// <typeparam name="T3">Components must be an IComponent.</typeparam>
        /// <typeparam name="T4">Components must be an IComponent.</typeparam>
        /// <typeparam name="T5">Components must be an IComponent.</typeparam>
        /// <typeparam name="T6">Components must be an IComponent.</typeparam>
        /// <typeparam name="T7">Components must be an IComponent.</typeparam>
        /// <typeparam name="T8">Components must be an IComponent.</typeparam>
        /// <returns>A tuple that will contain the entity count and arrays for each component.</returns>
        public (int Count, T1[] C1, T2[] C2, T3[] C3, T4[] C4, T5[] C5, T6[] C6, T7[] C7, T8[] C8) GetArchetype<T1, T2, T3, T4, T5, T6, T7, T8>()
            where T1 : IComponent<T1> where T2 : IComponent<T2> where T3 : IComponent<T3>
            where T4 : IComponent<T4> where T5 : IComponent<T5> where T6 : IComponent<T6>
            where T7 : IComponent<T7> where T8 : IComponent<T8>
        {
            var t1 = GetComponents<T1>(); var t2 = GetComponents<T2>(); var t3 = GetComponents<T3>();
            var t4 = GetComponents<T4>(); var t5 = GetComponents<T5>(); var t6 = GetComponents<T6>();
            var t7 = GetComponents<T7>(); var t8 = GetComponents<T8>();

            var entities = new HashSet<Entity>(t1.EntityIndices.Keys);
            entities.IntersectWith(t2.EntityIndices.Keys); entities.IntersectWith(t3.EntityIndices.Keys);
            entities.IntersectWith(t4.EntityIndices.Keys); entities.IntersectWith(t5.EntityIndices.Keys);
            entities.IntersectWith(t6.EntityIndices.Keys); entities.IntersectWith(t7.EntityIndices.Keys);
            entities.IntersectWith(t8.EntityIndices.Keys);

            T1[] c1 = new T1[entities.Count]; T2[] c2 = new T2[entities.Count]; T3[] c3 = new T3[entities.Count];
            T4[] c4 = new T4[entities.Count]; T5[] c5 = new T5[entities.Count]; T6[] c6 = new T6[entities.Count];
            T7[] c7 = new T7[entities.Count]; T8[] c8 = new T8[entities.Count];

            var eIndices = t1.EntityIndices;
            foreach (Entity entity in entities)
            {
                int e = eIndices[entity];
                c1[e] = t1.Components[e]; c2[e] = t2.Components[e]; c3[e] = t3.Components[e];
                c4[e] = t4.Components[e]; c5[e] = t5.Components[e]; c6[e] = t6.Components[e];
                c7[e] = t7.Components[e]; c8[e] = t8.Components[e];
            }

            return (entities.Count, c1, c2, c3, c4, c5, c6, c7, c8);
        }

        /* If more types end up being needed, we can add them */
    }
}
