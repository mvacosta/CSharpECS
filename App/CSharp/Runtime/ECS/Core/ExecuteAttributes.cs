using System;

namespace App.ECS
{
    /// <summary>
    /// Which thread the system executes on.
    /// </summary>
    public enum ExecuteUpdateType
    {
        Update,
        FixedUpdate,
        LateUpdate,
        Draw
    }

    /// <summary>
    /// Whether the system reads and/or writes to components.
    /// </summary>
    [Flags]
    public enum ExecuteAccess
    {
        Read = 1 << 0,
        Write = 1 << 1,
        ReadWrite = Read | Write
    }

    /// <summary>
    ///
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ExecuteAttribute : Attribute
    {
        public readonly ExecuteUpdateType UpdateType;
        public readonly ExecuteAccess Access;
        public readonly Type[] ExecuteBefore;
        public readonly Type[] ExecuteAfter;

        /// <summary>
        ///
        /// </summary>
        /// <param name="updateType"></param>
        /// <param name="access"></param>
        /// <param name="executeBefore"></param>
        /// <param name="executeAfter"></param>
        public ExecuteAttribute(ExecuteUpdateType updateType,
                                ExecuteAccess access = ExecuteAccess.Read,
                                Type[] executeBefore = null,
                                Type[] executeAfter = null)
        {
            UpdateType = updateType;
            Access = access;
            ExecuteBefore = executeBefore;
            ExecuteAfter = executeAfter;
        }
    }
}
