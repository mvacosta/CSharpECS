namespace App.ECS
{
    public sealed class EntityName : IComponent<EntityName>
    {
        public const string DEFAULT_NAME = "(unnamed)";

        public string Name;

        public EntityName(string name = "")
        {
            Name = string.IsNullOrWhiteSpace(name) ? DEFAULT_NAME : name;
        }

        #region Overrides and Operators

        public override bool Equals(object obj) => Equals(obj as EntityName);

        public bool Equals(EntityName other)
        {
            if (other is null) return false;

            return ReferenceEquals(this, other) || Name == other.Name;
        }

        public int CompareTo(EntityName other)
        {
            if (other is null) return 1;

            if (string.IsNullOrWhiteSpace(Name))
            {
                if (string.IsNullOrWhiteSpace(other.Name))
                    return 0;

                return -1;
            }

            if (string.IsNullOrWhiteSpace(other.Name)) return 1;

            return Name.CompareTo(other.Name);
        }

        public override int GetHashCode() => Name.GetHashCode();

        public override string ToString() => $"Entity Name: {Name}";

        public static bool operator ==(EntityName c1, EntityName c2) => c1.Equals(c2);

        public static bool operator !=(EntityName c1, EntityName c2) => !(c1 == c2);

        public static bool operator >(EntityName c1, EntityName c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(EntityName c1, EntityName c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(EntityName c1, EntityName c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(EntityName c1, EntityName c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}
