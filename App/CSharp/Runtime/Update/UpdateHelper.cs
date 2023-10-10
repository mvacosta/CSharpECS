using System;

namespace App.Update
{
    public sealed partial class UpdateManager
    {
        internal sealed class UpdateHelper : AbstractDisposable, IReusable
        {
            internal static UpdateManager manager = null;

            private int repeat = 0;
            private ulong framesAmount = 0;
            private ulong callOnFrame = 0;
            private double secondsAmount = 0.0;
            private double callOnSeconds = 0.0;
            private Action delayedCallback = null;
            private Func<bool> condition = null;

            public readonly int ID;
            public bool IsRetired { get; private set; } = true;

            internal UpdateHelper(int id)
            {
                ID = id;
            }

            public override int GetHashCode() => ID;

            protected override void DisposeManagedResources()
            {
                manager.CancelDelayedCall(ID);
            }

            public void Retire()
            {
                repeat = 0;
                framesAmount = 0;
                callOnFrame = 0;
                secondsAmount = 0.0;
                callOnSeconds = 0.0;

                delayedCallback = null;
                condition = null;

                manager.onEndOfFrame -= OnFramesUpdate;
                manager.onEndOfFrame -= OnSecondsUpdate;
                manager.onEndOfFrame -= OnAfterConditionUpdate;
                manager.onEndOfFrame -= OnWhileConditionUpdate;

                IsRetired = true;
            }

            #region Frame-based Callbacks

            internal int CallLaterFrame(Action delayedCallback, ulong frame)
            {
                framesAmount = frame + 1;
                callOnFrame = manager.TotalFrames + framesAmount;
                this.delayedCallback = delayedCallback;

                manager.onEndOfFrame += OnFramesUpdate;

                IsRetired = false;

                return ID;
            }

            internal int CallEndOfFrame(Action delayedCallback) => CallLaterFrame(delayedCallback, 0);

            internal int CallNextFrame(Action delayedCallback) => CallLaterFrame(delayedCallback, 1);

            internal int CallRepeatFrame(Action delayedCallback, ulong frame, int repeat)
            {
                this.repeat = repeat;

                return CallLaterFrame(delayedCallback, frame);
            }

            private void OnFramesUpdate(double _)
            {
                if (manager.TotalFrames >= callOnFrame)
                {
                    delayedCallback();
                    callOnFrame += framesAmount;

                    if (repeat > 0)
                    {
                        repeat--;
                    }
                    else if (repeat == 0)
                        Retire();
                }
            }

            #endregion

            #region Seconds-based Callbacks

            internal int CallLaterSeconds(Action delayedCallback, double seconds)
            {
                secondsAmount = seconds;
                callOnSeconds = manager.TotalSeconds + secondsAmount;
                this.delayedCallback = delayedCallback;

                manager.onEndOfFrame += OnSecondsUpdate;

                IsRetired = false;

                return ID;
            }

            internal int CallRepeatSeconds(Action delayedCallback, double seconds, int repeat)
            {
                this.repeat = repeat;

                return CallLaterSeconds(delayedCallback, seconds);
            }

            private void OnSecondsUpdate(double _)
            {
                if (manager.TotalSeconds >= callOnSeconds)
                {
                    delayedCallback();
                    callOnSeconds += secondsAmount;

                    if (repeat > 0)
                    {
                        repeat--;
                    }
                    else if (repeat == 0)
                        Retire();
                }
            }

            #endregion

            #region Conditional Callbacks

            internal int CallAfterCondition(Action delayedCallback, Func<bool> condition)
            {
                this.delayedCallback = delayedCallback;
                this.condition = condition;

                manager.onEndOfFrame += OnAfterConditionUpdate;

                IsRetired = false;

                return ID;
            }

            private void OnAfterConditionUpdate(double dt)
            {
                if (condition())
                {
                    delayedCallback();
                    Retire();
                }
            }

            internal int CallWhileCondition(Action delayedCallback, Func<bool> condition)
            {
                this.delayedCallback = delayedCallback;
                this.condition = condition;

                manager.onEndOfFrame += OnWhileConditionUpdate;

                IsRetired = false;

                return ID;
            }

            private void OnWhileConditionUpdate(double _)
            {
                if (condition())
                {
                    delayedCallback();
                    return;
                }

                Retire();
            }

            #endregion
        }
    }
}
