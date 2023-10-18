using Microsoft.Xna.Framework;

namespace App.ECS
{
    public sealed class MotionVelocity : IComponent<MotionVelocity>
    {
        public Vector3 Acceleration;
        public Vector3 Speed;

        public static MotionVelocity SetConstant(Vector3 speed) => new()
        {
            Acceleration = Vector3.Zero,
            Speed = speed,
        };

        #region Overrides and Operators

        public override bool Equals(object obj) => Equals(obj as MotionVelocity);

        public bool Equals(MotionVelocity other)
        {
            if (other is null) return false;

            return ReferenceEquals(this, other);
        }

        public int CompareTo(MotionVelocity other)
        {
            if (other is null) return 1;

            return ReferenceEquals(this, other) ? 0 : 1;
        }

        public override int GetHashCode() => (Acceleration.GetHashCode() * 3)
                                           + (Speed.GetHashCode() * 7);

        public override string ToString() => $"Moving: {Speed} (Acceleration: {Acceleration})";

        public static bool operator ==(MotionVelocity c1, MotionVelocity c2) => c1.Equals(c2);

        public static bool operator !=(MotionVelocity c1, MotionVelocity c2) => !(c1 == c2);

        public static bool operator >(MotionVelocity c1, MotionVelocity c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(MotionVelocity c1, MotionVelocity c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(MotionVelocity c1, MotionVelocity c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(MotionVelocity c1, MotionVelocity c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}

