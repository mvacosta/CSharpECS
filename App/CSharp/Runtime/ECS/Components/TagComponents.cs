namespace App.ECS
{
#if FALSE // Never use this in-engine; this is just used to copy for future tags.
    public readonly struct TemplateTag : IComponent<TemplateTag>
    {
        private const string TAG = "YourTagNameHere";

        #region Overrides and Operators

        public override readonly bool Equals(object obj) => obj is TemplateTag;

        public readonly bool Equals(TemplateTag _) => true;

        public readonly int CompareTo(TemplateTag _) => 0;

        public override readonly int GetHashCode() => TAG.GetHashCode();

        public override readonly string ToString() => TAG;

        public static bool operator ==(TemplateTag c1, TemplateTag c2) => true;

        public static bool operator !=(TemplateTag c1, TemplateTag c2) => false;

        public static bool operator >(TemplateTag c1, TemplateTag c2) => false;

        public static bool operator <(TemplateTag c1, TemplateTag c2) => false;

        public static bool operator >=(TemplateTag c1, TemplateTag c2) => false;

        public static bool operator <=(TemplateTag c1, TemplateTag c2) => false;

        #endregion
    }
#endif

    /// <summary>
    /// A component tag that identifies an entity as a camera.
    /// </summary>
    public readonly struct MainCameraTag : IComponent<MainCameraTag>
    {
        private const string TAG = "MainCamera";

        #region Overrides and Operators

        public override readonly bool Equals(object obj) => obj is MainCameraTag;

        public readonly bool Equals(MainCameraTag _) => true;

        public readonly int CompareTo(MainCameraTag _) => 0;

        public override readonly int GetHashCode() => TAG.GetHashCode();

        public override readonly string ToString() => TAG;

        public static bool operator ==(MainCameraTag c1, MainCameraTag c2) => true;

        public static bool operator !=(MainCameraTag c1, MainCameraTag c2) => false;

        public static bool operator >(MainCameraTag c1, MainCameraTag c2) => false;

        public static bool operator <(MainCameraTag c1, MainCameraTag c2) => false;

        public static bool operator >=(MainCameraTag c1, MainCameraTag c2) => false;

        public static bool operator <=(MainCameraTag c1, MainCameraTag c2) => false;

        #endregion
    }

    /// <summary>
    /// A component tag that identifies an entity as the player character.
    /// </summary>
    public readonly struct PlayerTag : IComponent<PlayerTag>
    {
        private const string TAG = "Player";

        #region Overrides and Operators

        public override readonly bool Equals(object obj) => obj is PlayerTag;

        public readonly bool Equals(PlayerTag _) => true;

        public readonly int CompareTo(PlayerTag _) => 0;

        public override readonly int GetHashCode() => TAG.GetHashCode();

        public override readonly string ToString() => TAG;

        public static bool operator ==(PlayerTag c1, PlayerTag c2) => true;

        public static bool operator !=(PlayerTag c1, PlayerTag c2) => false;

        public static bool operator >(PlayerTag c1, PlayerTag c2) => false;

        public static bool operator <(PlayerTag c1, PlayerTag c2) => false;

        public static bool operator >=(PlayerTag c1, PlayerTag c2) => false;

        public static bool operator <=(PlayerTag c1, PlayerTag c2) => false;

        #endregion
    }
}
