using System;
using System.Collections.Generic;
using App.Update;

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
        private HashSet<Entity> entities = null;
        private HashSet<ECSWorld> worlds = null;

        public int AvailableEntities { get { return entities.Count; } }

        public ECSManager()
        {
            entities = new HashSet<Entity>();
            CreateNewEntities(ENTITY_INIT_COUNT);

            worlds = new HashSet<ECSWorld>();
        }

        protected override void DisposeManagedResources()
        {
            foreach (ECSWorld world in worlds)
                world.Dispose();

            worlds.Clear();
            worlds = null;

            entities.Clear();
            entities = null;
        }

        public ECSWorld AddWorld()
        {
            ECSWorld world = new ECSWorld();

            if (worlds.Add(world))
                return world;

            return null;
        }

        public void ReturnEntities(HashSet<Entity> entities)
        {
            if (entities == null || entities.Count <= 0)
                return;

            this.entities.UnionWith(entities);

            entities.Clear();
        }

        public void RequestEntities(ECSWorld toWorld, int amount)
        {
            // Create more entities if requred
            if (amount > entities.Count)
                CreateNewEntities(amount - entities.Count + ENTITY_ADD_AMOUNT);

            int count = 0;
            HashSet<Entity> worldEntities = new HashSet<Entity>();
            foreach(var entity in entities)
            {
                worldEntities.Add(entity);

                if (++count >= amount)
                    break;
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
