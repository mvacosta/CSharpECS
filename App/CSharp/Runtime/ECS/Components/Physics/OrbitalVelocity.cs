using Microsoft.Xna.Framework;

namespace App.ECS
{
    public sealed class OrbitalVelocity : IComponent<OrbitalVelocity>
    {
        public Vector3 Acceleration;
        public Vector3 Speed;

        public static OrbitalVelocity SetConstant(Vector3 speed) => new()
        {
            Acceleration = Vector3.Zero,
            Speed = speed,
        };

        #region Overrides and Operators

        public override bool Equals(object obj) => Equals(obj as OrbitalVelocity);

        public bool Equals(OrbitalVelocity other)
        {
            if (other is null) return false;

            return ReferenceEquals(this, other);
        }

        public int CompareTo(OrbitalVelocity other)
        {
            if (other is null) return 1;

            return ReferenceEquals(this, other) ? 0 : 1;
        }

        public override int GetHashCode() => (Acceleration.GetHashCode() * 7)
                                           + (Speed.GetHashCode() * 5);

        public override string ToString() => $"Orbiting: {Speed} (Acceleration: {Acceleration})";

        public static bool operator ==(OrbitalVelocity c1, OrbitalVelocity c2) => c1.Equals(c2);

        public static bool operator !=(OrbitalVelocity c1, OrbitalVelocity c2) => !(c1 == c2);

        public static bool operator >(OrbitalVelocity c1, OrbitalVelocity c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(OrbitalVelocity c1, OrbitalVelocity c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(OrbitalVelocity c1, OrbitalVelocity c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(OrbitalVelocity c1, OrbitalVelocity c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}

