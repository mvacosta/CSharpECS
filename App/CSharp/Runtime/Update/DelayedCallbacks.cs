using System;

namespace App.Update
{
    public sealed partial class UpdateManager
    {
        /// <summary>
        /// Invoke callback after a certain amount of frames.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="frame">How many frames to wait to invoke.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallLaterFrame(Action callback, ulong frame)
        {
            return GetNextHelper().CallLaterFrame(callback, frame);
        }

        /// <summary>
        /// Invoke callback on the next frame.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallNextFrame(Action callback)
        {
            return GetNextHelper().CallNextFrame(callback);
        }

        /// <summary>
        /// Invoke callback after a certain amount of frames and repeat.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="frame">How many frames to wait to invoke.</param>
        /// <param name="repeat">How many times to repeat. Pass -1 or less to repeat indefinitely.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallRepeatFrame(Action callback, ulong frame, int repeat)
        {
            return GetNextHelper().CallRepeatFrame(callback, frame, repeat);
        }

        /// <summary>
        /// Invoke callback after a certain amount of seconds.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="seconds">How many seconds to wait to invoke.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallLaterSeconds(Action callback, double seconds)
        {
            return GetNextHelper().CallLaterSeconds(callback, seconds);
        }

        /// <summary>
        /// Invoke callback after a certain amount of seconds and repeat.
        /// </summary>
        /// <param name="callback">Callback to invoke.</param>
        /// <param name="seconds">How many seconds to wait to invoke.</param>
        /// <param name="repeat">How many times to repeat. Pass -1 or less to repeat indefinitely.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallRepeatSeconds(Action callback, double seconds, int repeat)
        {
            return GetNextHelper().CallRepeatSeconds(callback, seconds, repeat);
        }

        /// <summary>
        /// Invoke callback after a condition has been met.
        /// </summary>
        /// <param name="delayedCallBack">Callback to invoke.</param>
        /// <param name="condition">When true, callback will be invoked.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallAfterCondition(Action delayedCallBack, Func<bool> condition)
        {
            return GetNextHelper().CallAfterCondition(delayedCallBack, condition);
        }

        /// <summary>
        /// Invoke callback while a condition is true.
        /// </summary>
        /// <param name="delayedCallBack">Callback to invoke.</param>
        /// <param name="condition">While true, callback will be invoked.</param>
        /// <returns>ID for this delayed callback. Use it for <b>CancelDelayedCall</b> if needed.</returns>
        public int CallWhileCondition(Action delayedCallBack, Func<bool> condition)
        {
            return GetNextHelper().CallWhileCondition(delayedCallBack, condition);
        }

        /// <summary>
        /// Cancel a delayed callback waiting to be invoked.
        /// </summary>
        /// <param name="delayedCallID">The ID for the delayed callback.</param>
        /// <returns><b>True</b> if delayed callback was successfully canceled.</returns>
        public bool CancelDelayedCall(int delayedCallID)
        {
            foreach (var helper in helpers)
            {
                if (helper.ID == delayedCallID)
                {
                    helper.Retire();

                    return true;
                }
            }

            return false;
        }

        private UpdateHelper GetNextHelper()
        {
            foreach (var helper in helpers)
            {
                if (helper.IsRetired) return helper;
            }

            UpdateHelper newHelper = new UpdateHelper(helpers.Count, this);
            helpers.Add(newHelper);

            return newHelper;
        }
    }
}
