﻿using System.Collections.Generic;

namespace App.ECS
{
    /// <summary>
    /// ECSManager will manage all of the ECSWorlds this app is using.
    /// It will also create our entities and hand them off to the worlds that need it.
    /// </summary>
    public sealed class ECSManager : AbstractDisposable
    {
        private const int ENTITY_INIT_COUNT = 256;
        private const int ENTITY_ADD_AMOUNT = ENTITY_INIT_COUNT / 2;

        private int entityCount = 0;
        private HashSet<Entity> entities = new(ENTITY_INIT_COUNT * 2);
        private HashSet<ECSWorld> worlds = new();

        public int Available { get { return entities.Count; } }

        public ECSManager()
        {
            CreateNewEntities(ENTITY_INIT_COUNT);
        }

        protected override void DisposeManagedResources()
        {
            foreach (var world in worlds)
            {
                world.Dispose();
            }

            worlds.Clear();
            worlds = null;

            entities.Clear();
            entities = null;
        }

        public ECSWorld AddWorld()
        {
            var world = new ECSWorld();

            if (worlds.Add(world))
            {
                return world;
            }

            return null;
        }

        public void ReturnEntities(HashSet<Entity> returning)
        {
            if (entities == null || entities.Count <= 0)
            {
                return;
            }

            entities.UnionWith(returning);

            entities.Clear();
        }

        public void RequestEntities(ref ECSWorld toWorld, int amount)
        {
            // Create more entities if requred
            if (amount > entities.Count)
            {
                CreateNewEntities(amount - entities.Count + ENTITY_ADD_AMOUNT);
            }

            int count = 0;
            var worldEntities = new HashSet<Entity>();
            foreach(var entity in entities)
            {
                worldEntities.Add(entity);

                if (++count >= amount)
                {
                    break;
                }
            }

            entities.ExceptWith(worldEntities);

            toWorld.ReceiveEntities(worldEntities);
        }

        private void CreateNewEntities(int newToAdd = ENTITY_ADD_AMOUNT)
        {
            for (int i = 0; i < newToAdd; i++)
            {
                entities.Add(new Entity(entityCount));
                entityCount++;
            }
        }
    }
}
