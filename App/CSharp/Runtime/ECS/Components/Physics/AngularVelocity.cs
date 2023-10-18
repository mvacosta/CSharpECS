using Microsoft.Xna.Framework;

namespace App.ECS
{
    public sealed class AngularVelocity : IComponent<AngularVelocity>
    {
        public Vector3 Acceleration;
        public Vector3 Speed;

        public static AngularVelocity SetConstant(Vector3 speed) => new()
        {
            Acceleration = Vector3.Zero,
            Speed = speed,
        };

        #region Overrides and Operators

        public override bool Equals(object obj) => Equals(obj as AngularVelocity);

        public bool Equals(AngularVelocity other)
        {
            if (other is null) return false;

            return ReferenceEquals(this, other);
        }

        public int CompareTo(AngularVelocity other)
        {
            if (other is null) return 1;

            return ReferenceEquals(this, other) ? 0 : 1;
        }

        public override int GetHashCode() => (Acceleration.GetHashCode() * 5)
                                           + (Speed.GetHashCode() * 3);

        public override string ToString() => $"Spinning: {Speed} (Acceleration: {Acceleration})";

        public static bool operator ==(AngularVelocity c1, AngularVelocity c2) => c1.Equals(c2);

        public static bool operator !=(AngularVelocity c1, AngularVelocity c2) => !(c1 == c2);

        public static bool operator >(AngularVelocity c1, AngularVelocity c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(AngularVelocity c1, AngularVelocity c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(AngularVelocity c1, AngularVelocity c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(AngularVelocity c1, AngularVelocity c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}

