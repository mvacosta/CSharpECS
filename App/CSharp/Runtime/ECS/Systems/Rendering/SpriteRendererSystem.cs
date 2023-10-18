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
            App.UpdateManager.OnDraw += OnDraw;

            base.Enable(world);
        }

        public override void Disable()
        {
            App.UpdateManager.OnDraw -= OnDraw;

            base.Disable();
        }

        public void OnDraw(double dt)
        {
            SpriteBatch spriteBatch = App.SpriteBatch;
            var (Count, C1, C2) = World.GetArchetype<Transform, Sprite>();

            spriteBatch.Begin();

            for (int i = 0; i < Count; i++)
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

            spriteBatch.End();
        }
    }
}
