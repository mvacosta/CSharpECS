using System;

namespace App.ECS
{
    /// <summary>
    /// Please use <b>IComponent<T></b> for interfacing components!
    /// </summary>
    public interface IComponent
    {
        // Eventually we will have IMGUI implemented and we'll want to make use of this interface
    }

    /// <summary>
    /// For components used as part of the Entity-Component-System
    /// </summary>
    public interface IComponent<T> : IComponent, IEquatable<T>, IComparable<T> where T : struct { }
}
