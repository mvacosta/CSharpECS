using System;
using App.ECS;

namespace App
{
    // All of the game will be handled by this
    public sealed class GameManager : AbstractDisposable
    {
        //

        public GameManager()
        {
            App.UpdateManager.CallNextFrame(DoIt); // TEST
        }

        private void DoIt()
        {
            ECSWorld appWorld = App.ECSManager.AddWorld();

            App.ECSManager.RequestEntities(appWorld, 1000);

            appWorld.TEST();
            appWorld.AddSystem<SpriteRendererSystem>();
        }

        protected override void DisposeManagedResources()
        {
            //
        }
    }
}
