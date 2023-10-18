using System;
using App.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App
{
    // All of the game will be handled by this
    public sealed class GameManager : AbstractDisposable
    {
        //

        public GameManager()
        {
            App.UpdateManager.CallNextFrame(TEST); // TEST
        }

        private void TEST()
        {
            ECSWorld appWorld = App.ECSManager.AddWorld();

            App.ECSManager.RequestEntities(ref appWorld, 10000);

            Texture2D tex = App.Content.Load<Texture2D>("TestTexture");

            float w = App.GraphicsDeviceManager.PreferredBackBufferWidth;
            float h = App.GraphicsDeviceManager.PreferredBackBufferHeight;
            var test = new Random();
            foreach (var entity in appWorld.Entities)
            {
                appWorld.AttachComponent(entity, new Transform(new Vector3((float)test.NextDouble() * w,
                                                                           (float)test.NextDouble() * h, 0.0f),
                                                      new Vector3(1.0f, 1.0f, (float)test.NextDouble() * 0.8f + 0.2f)));

                appWorld.AttachComponent(entity, new Sprite(tex, new Color((float)test.NextDouble(),
                                                                  (float)test.NextDouble(),
                                                                  (float)test.NextDouble(), 1.0f)));
            }

            appWorld.AddSystem<SpriteRendererSystem>();
        }

        protected override void DisposeManagedResources()
        {
            //
        }
    }
}
