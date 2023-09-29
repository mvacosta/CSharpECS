using System;
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
        private ECSWorld world = null;

        public override void Initialize(ECSWorld world)
        {
            this.world = world;
            App.UpdateManager.OnDraw += DoDrawTest;
        }

        public override void Retire()
        {
            App.UpdateManager.OnDraw -= DoDrawTest;
        }

        private void DoDrawTest(double dt)
        {
            var data = world.Components.GetArchetype<Transform, Sprite>();
            DrawSprites(data.count, data.Item2, data.Item3);
        }

        public void DrawSprites(int count, ReadOnlySpan<Transform> transforms, ReadOnlySpan<Sprite> sprites)
        {
            SpriteBatch spriteBatch = App.SpriteBatch;
            spriteBatch.Begin();

            for (int i = 0; i < count; i++)
            {
                var transform = transforms[i];
                var sprite = sprites[i];

                spriteBatch.Draw(sprite.Texture,
                                 new Vector2(transform.Position.X,
                                             transform.Position.Y),
                                 null,
                                 sprite.Color,
                                 0.0f,
                                 sprite.Origin,
                                 transform.Scale.Z,
                                 sprite.Flip,
                                 transform.Position.Z);
            }

            spriteBatch.End();
        }
    }
}
