using Microsoft.Xna.Framework;

namespace App.ECS
{
    public struct Transform : IComponent<Transform>
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Vector3 Scale;

        public static Transform Identity => new()
            {
                Position = Vector3.Zero,
                Rotation = Quaternion.Identity,
                Scale = Vector3.One
            };

        public Transform(Vector3 position) : this(position, Quaternion.Identity, Vector3.One) { }

        public Transform(Vector3 position, Quaternion rotation) : this(position, rotation, Vector3.One) { }

        public Transform(Vector3 position, Vector3 scale) : this(position, Quaternion.Identity, scale) { }

        public Transform(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        #region Overrides and Operators

        public override readonly bool Equals(object obj) => obj is Transform c && Equals(c);

        public readonly bool Equals(Transform other) => ReferenceEquals(this, other);

        public readonly int CompareTo(Transform other) => ReferenceEquals(this, other) ? 0 : 1;

        public override readonly int GetHashCode() => (Position.GetHashCode() * 3)
                                                    + (Rotation.GetHashCode() * 7)
                                                    + (Scale.GetHashCode() * 5);

        public override readonly string ToString() => $"Position: {Position}, Rotation: {Rotation}, Scale: {Scale}";

        public static bool operator ==(Transform c1, Transform c2) => c1.Equals(c2);

        public static bool operator !=(Transform c1, Transform c2) => !(c1 == c2);

        public static bool operator >(Transform c1, Transform c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(Transform c1, Transform c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(Transform c1, Transform c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(Transform c1, Transform c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}
