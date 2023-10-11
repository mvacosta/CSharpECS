using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.ECS
{
    /// <summary>
    /// Draws sprites in a basic manner using SpriteBatch.
    ///
    /// Components: Transform, Sprite
    /// </summary>
    public class SpriteRendererSystem : AbstractSystem
    {
        public override void Enable(ECSWorld world)
        {
            App.UpdateManager.OnFixedUpdate += OnFixedUpdate;
            App.UpdateManager.OnDraw += OnDraw;

            base.Enable(world);
        }

        public override void Disable()
        {
            App.UpdateManager.OnFixedUpdate -= OnFixedUpdate;
            App.UpdateManager.OnDraw -= OnDraw;

            base.Disable();
        }

        public void OnFixedUpdate(double dt)
        {
            var (Count, C1) = World.GetArchetype<Transform>();

            Transform trans = C1[Count - 1];
            float x = trans.Position.X + (1000.0f * (float)dt);
            x = x > 1280.0f ? 0.0f : x;
            trans.Position = new Vector3(x, trans.Position.Y, trans.Position.Z);
        }

        public void OnDraw(double dt)
        {
            SpriteBatch spriteBatch = App.SpriteBatch;
            var (Count, C1, C2) = World.GetArchetype<Transform, Sprite>();
            
            spriteBatch.Begin();

            for (int i = 0; i < Count - 1; i++)
            {
                var transform = C1[i];
                var sprite = C2[i];

                spriteBatch.Draw(sprite.Texture,
                                 new Vector2(transform.Position.X,
                                             transform.Position.Y),
                                 null,
                                 sprite.Color,
                                 transform.Rotation.Z,
                                 sprite.Origin,
                                 transform.Scale.Z,
                                 sprite.Flip,
                                 transform.Position.Z);
            }

            var t = C1[Count - 1];
            var s = C2[Count - 1];
            
            spriteBatch.Draw(s.Texture,
                             new Vector2(t.Position.X + (1000.0f * (float)dt),
                                         t.Position.Y),
                             null,
                             s.Color,
                             t.Rotation.Z,
                             s.Origin,
                             t.Scale.Z,
                             s.Flip,
                             t.Position.Z);

            spriteBatch.End();
        }
    }
}
