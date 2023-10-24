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
            //App.UpdateManager.CallNextFrame(SpriteRendererTest);
            App.UpdateManager.CallNextFrame(PhysicsTest);
        }

        private void PhysicsTest()
        {
            ECSWorld world = App.ECSManager.AddWorld();

            App.ECSManager.RequestEntities(ref world, App.ECSManager.Available);

            Model m = App.Content.Load<Model>("Earth");



            world.AddSystem<BasicEffectSystem>();
        }

        private void SpriteRendererTest()
        {
            ECSWorld world = App.ECSManager.AddWorld();

            App.ECSManager.RequestEntities(ref world, 10000);

            Texture2D tex = App.Content.Load<Texture2D>("TestTexture");

            float w = App.GraphicsDeviceManager.PreferredBackBufferWidth;
            float h = App.GraphicsDeviceManager.PreferredBackBufferHeight;
            var test = new Random();
            foreach (var entity in world.Entities)
            {
                world.AttachComponent(entity, new Transform(new Vector3((float)test.NextDouble() * w,
                                                                        (float)test.NextDouble() * h, 0.0f),
                                                            new Vector3(1.0f, 1.0f, (float)test.NextDouble() * 0.8f + 0.2f)));

                world.AttachComponent(entity, new Sprite(tex, new Color((float)test.NextDouble(),
                                                                        (float)test.NextDouble(),
                                                                        (float)test.NextDouble(), 1.0f)));
            }

            world.AddSystem<SpriteRendererSystem>();
        }

        protected override void DisposeManagedResources()
        {
            //
        }
    }
}
