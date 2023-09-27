using System;

namespace App.Update
{
    public sealed partial class UpdateManager
    {
        internal sealed class UpdateHelper : AbstractDisposable, IReusable
        {
            private int repeat = 0;
            private ulong framesAmount = 0;
            private ulong callOnFrame = 0;
            private double secondsAmount = 0.0;
            private double callOnSeconds = 0.0;
            private Action delayedCallback = null;
            private Func<bool> condition = null;
            private UpdateManager updateManager = null;

            public readonly int ID;
            public bool IsRetired { get; private set; }

            public UpdateHelper(int id, UpdateManager updateManager)
            {
                ID = id;

                IsRetired = true;

                this.updateManager = updateManager;
            }

            public override int GetHashCode() => ID;

            protected override void DisposeManagedResources()
            {
                updateManager.CancelDelayedCall(ID);

                updateManager = null;
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

                updateManager.OnUpdate -= OnFramesUpdate;
                updateManager.OnUpdate -= OnSecondsUpdate;
                updateManager.OnUpdate -= OnAfterConditionUpdate;
                updateManager.OnUpdate -= OnWhileConditionUpdate;

                IsRetired = true;
            }

            #region Frame-based Callbacks

            public int CallLaterFrame(Action delayedCallback, ulong frame)
            {
                framesAmount = frame + 1;
                callOnFrame = updateManager.TotalFrames + framesAmount;
                this.delayedCallback = delayedCallback;

                updateManager.OnUpdate += OnFramesUpdate;

                IsRetired = false;

                return ID;
            }

            public int CallNextFrame(Action delayedCallback) => CallLaterFrame(delayedCallback, 1);

            public int CallRepeatFrame(Action delayedCallback, ulong frame, int repeat)
            {
                this.repeat = repeat;

                return CallLaterFrame(delayedCallback, frame);
            }

            private void OnFramesUpdate(double _)
            {
                if (updateManager.TotalFrames >= callOnFrame)
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

            public int CallLaterSeconds(Action delayedCallback, double seconds)
            {
                secondsAmount = seconds;
                callOnSeconds = updateManager.TotalSeconds + secondsAmount;
                this.delayedCallback = delayedCallback;

                updateManager.OnUpdate += OnSecondsUpdate;

                IsRetired = false;

                return ID;
            }

            public int CallRepeatSeconds(Action delayedCallback, double seconds, int repeat)
            {
                this.repeat = repeat;

                return CallLaterSeconds(delayedCallback, seconds);
            }

            private void OnSecondsUpdate(double _)
            {
                if (updateManager.TotalSeconds >= callOnSeconds)
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

            public int CallAfterCondition(Action delayedCallback, Func<bool> condition)
            {
                this.delayedCallback = delayedCallback;
                this.condition = condition;

                updateManager.OnUpdate += OnAfterConditionUpdate;

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

            public int CallWhileCondition(Action delayedCallback, Func<bool> condition)
            {
                this.delayedCallback = delayedCallback;
                this.condition = condition;

                updateManager.OnUpdate += OnWhileConditionUpdate;

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
