using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.ECS
{
    public class Sprite : IComponent<Sprite>
    {
        public Texture2D Texture;
        public Vector2 Origin;
        public Color Color;
        public SpriteEffects Flip;

        public Sprite(Texture2D texture, SpriteEffects flip = SpriteEffects.None)
            : this(texture, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), Color.White, flip) { }

        public Sprite(Texture2D texture, Vector2 origin, SpriteEffects flip = SpriteEffects.None)
            : this(texture, origin, Color.White, flip) { }

        public Sprite(Texture2D texture, Color color, SpriteEffects flip = SpriteEffects.None)
            : this(texture, new Vector2(texture.Width / 2.0f, texture.Height / 2.0f), color, flip) { }

        public Sprite(Texture2D texture, Vector2 origin, Color color, SpriteEffects flip = SpriteEffects.None)
        {
            Texture = texture;
            Origin = origin;
            Color = color;
            Flip = flip;
        }

        #region Overrides and Operators

        public override bool Equals(object obj) => obj is Sprite c && Equals(c);

        public bool Equals(Sprite other) => ReferenceEquals(this, other);

        public int CompareTo(Sprite other) => ReferenceEquals(this, other) ? 0 : 1;

        public override int GetHashCode() => Texture.GetHashCode();

        public override string ToString() => Texture.ToString();

        public static bool operator ==(Sprite e1, Sprite e2) => e1.Equals(e2);

        public static bool operator !=(Sprite e1, Sprite e2) => !(e1 == e2);

        public static bool operator >(Sprite e1, Sprite e2) => e1.CompareTo(e2) == 1;

        public static bool operator <(Sprite e1, Sprite e2) => e1.CompareTo(e2) == -1;

        public static bool operator >=(Sprite e1, Sprite e2) => e1.CompareTo(e2) >= 0;

        public static bool operator <=(Sprite e1, Sprite e2) => e1.CompareTo(e2) <= 0;

        #endregion
    }
}
