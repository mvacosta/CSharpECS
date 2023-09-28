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
        public override void Initialize(ECSWorld world)
        {
            App.UpdateManager.OnDraw += (_) => DrawSprites(world.Components.GetArchetype<Transform, Sprite>());
        }

        public override void Retire()
        {
            //App.UpdateManager.OnDraw -= (_) => DrawSprites(world.Components.GetArchetype<Transform, Sprite>());
        }

        public void DrawSprites(Archetype components)
        {
            SpriteBatch spriteBatch = App.SpriteBatch;
            spriteBatch.Begin();

            for (int i = 0; i < components.EntityCount; i++)
            {
                var transform = components.GetComponents<Transform>()[i];
                var sprite = components.GetComponents<Sprite>()[i];

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
