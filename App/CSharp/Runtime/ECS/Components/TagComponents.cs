namespace App.ECS
{
#if FALSE // Never use this in-engine; this is just used to copy for future tags.
    public readonly struct TemplateTag : IComponent<TemplateTag>
    {
        private const string TAG = "YourTagNameHere";

        #region Overrides and Operators

        public override bool Equals(object obj) => obj is TemplateTag;

        public bool Equals(TemplateTag other) => true;

        public int CompareTo(TemplateTag other) => 0;

        public override int GetHashCode() => TAG.GetHashCode();

        public override string ToString() => $"Tag: {TAG}";

        public static bool operator ==(TemplateTag c1, TemplateTag c2) => c1.Equals(c2);

        public static bool operator !=(TemplateTag c1, TemplateTag c2) => !(c1 == c2);

        public static bool operator >(TemplateTag c1, TemplateTag c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(TemplateTag c1, TemplateTag c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(TemplateTag c1, TemplateTag c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(TemplateTag c1, TemplateTag c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
#endif

    /// <summary>
    /// A component tag that identifies an entity as the player character.
    /// </summary>
    public readonly struct PlayerTag : IComponent<PlayerTag>
    {
        private const string TAG = "Player";

        #region Overrides and Operators

        public override bool Equals(object obj) => obj is PlayerTag;

        public bool Equals(PlayerTag other) => true;

        public int CompareTo(PlayerTag other) => 0;

        public override int GetHashCode() => TAG.GetHashCode();

        public override string ToString() => $"Tag: {TAG}";

        public static bool operator ==(PlayerTag c1, PlayerTag c2) => c1.Equals(c2);

        public static bool operator !=(PlayerTag c1, PlayerTag c2) => !(c1 == c2);

        public static bool operator >(PlayerTag c1, PlayerTag c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(PlayerTag c1, PlayerTag c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(PlayerTag c1, PlayerTag c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(PlayerTag c1, PlayerTag c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }

    /// <summary>
    /// A component tag that identifies an entity as a camera.
    /// </summary>
    public readonly struct MainCameraTag : IComponent<MainCameraTag>
    {
        private const string TAG = "MainCamera";

        #region Overrides and Operators

        public override bool Equals(object obj) => obj is MainCameraTag;

        public bool Equals(MainCameraTag other) => true;

        public int CompareTo(MainCameraTag other) => 0;

        public override int GetHashCode() => TAG.GetHashCode();

        public override string ToString() => $"Tag: {TAG}";

        public static bool operator ==(MainCameraTag c1, MainCameraTag c2) => c1.Equals(c2);

        public static bool operator !=(MainCameraTag c1, MainCameraTag c2) => !(c1 == c2);

        public static bool operator >(MainCameraTag c1, MainCameraTag c2) => c1.CompareTo(c2) == 1;

        public static bool operator <(MainCameraTag c1, MainCameraTag c2) => c1.CompareTo(c2) == -1;

        public static bool operator >=(MainCameraTag c1, MainCameraTag c2) => c1.CompareTo(c2) >= 0;

        public static bool operator <=(MainCameraTag c1, MainCameraTag c2) => c1.CompareTo(c2) <= 0;

        #endregion
    }
}
