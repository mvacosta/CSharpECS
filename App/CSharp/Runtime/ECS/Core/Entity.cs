using System;

namespace App.ECS
{
    public sealed class Entity : IEquatable<Entity>, IComparable<Entity>
    {
        public readonly int ID;

        public Entity(int entityID)
        {
            ID = entityID;
        }

        #region Overrides and Operators

        public override bool Equals(object obj) => Equals(obj as Entity);

        public bool Equals(Entity other)
        {
            if (other is null)
                return false;

            if (GetType() != other.GetType())
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return ID.Equals(other.ID);
        }

        public int CompareTo(Entity other)
        {
            if (other is null)
                return 1;

            if (GetType() != other.GetType())
                return 1;

            if (ReferenceEquals(this, other))
                return 0;

            return ID.CompareTo(other.ID);
        }

        public override int GetHashCode() => ID;

        public override string ToString() => $"Entity {ID}";

        public static bool operator ==(Entity e1, Entity e2)
        {
            if (e1 is null)
            {
                if (e2 is null)
                    return true;

                return false;
            }

            return e1.Equals(e2);
        }

        public static bool operator !=(Entity e1, Entity e2) => !(e1 == e2);

        public static bool operator >(Entity e1, Entity e2)  => e1.CompareTo(e2) == 1;

        public static bool operator <(Entity e1, Entity e2)  => e1.CompareTo(e2) == -1;

        public static bool operator >=(Entity e1, Entity e2) => e1.CompareTo(e2) >= 0;

        public static bool operator <=(Entity e1, Entity e2) => e1.CompareTo(e2) <= 0;

        #endregion
    }
}
