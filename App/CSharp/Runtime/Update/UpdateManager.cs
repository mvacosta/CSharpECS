//#define ALLOW_UPDATE_RESET

using System.Collections.Generic;

namespace App.Update
{
    public delegate void DeltaHandler(double dt);

    public sealed partial class UpdateManager : AbstractDisposable
    {
        private const int POOLED_HELPERS = 10;
        private const double FIXED_UPDATE_STEP = 1.0 / App.FIXED_FRAMERATE; // Simulate in 60 FPS
        private const double RESET_THRESHOLD = FIXED_UPDATE_STEP * 4.0;

        private double updateStep = 0.0;
        private HashSet<UpdateHelper> helpers = new(POOLED_HELPERS);
        private DeltaHandler onEndOfFrame = null;

        /// <summary>
        /// Use for user input, networked IO, or timing.
        /// </summary>
        public DeltaHandler OnUpdate = null;

        /// <summary>
        /// Use for game logic and physics updates.
        /// </summary>
        public DeltaHandler OnFixedUpdate = null;

        /// <summary>
        /// A second fixed update that occurs after; useful for ordering execution.
        /// </summary>
        public DeltaHandler OnLateUpdate = null;

        /// <summary>
        /// Use only for draw code. Interpolation is passed for smoother on-screen movement.
        /// </summary>
        public DeltaHandler OnDraw = null;

        /// <summary>
        /// Total amount of frames since application has started.
        /// </summary>
        public ulong TotalFrames { get; private set; } = 0;

        /// <summary>
        /// Total amount of seconds since application has started.
        /// </summary>
        public double TotalSeconds { get; private set; } = 0.0;

        public static UpdateManager CreateUpdateManager(out DeltaHandler update, out DeltaHandler draw)
        {
            var manager = new UpdateManager();

            update = manager.HandleUpdate;
            draw = manager.HandleDraw;

            return manager;
        }

        private UpdateManager()
        {
            UpdateHelper.manager = this;
            for (int i = 0; i < POOLED_HELPERS; i++)
            {
                helpers.Add(new UpdateHelper(i));
            }
        }

        protected override void DisposeManagedResources()
        {
            foreach (var helper in helpers)
            {
                helper.Dispose();
            }

            helpers.Clear();
            helpers = null;

            UpdateHelper.manager = null;
            onEndOfFrame = null;

            OnUpdate = null;
            OnFixedUpdate = null;
            OnLateUpdate = null;
            OnDraw = null;
        }

        private void HandleUpdate(double dt)
        {
            TotalFrames++;
            TotalSeconds += dt;

            updateStep += dt;

#if ALLOW_UPDATE_RESET
            // If time is starting to get real delayed, reset to try to smooth things out
            if (updateStep >= RESET_THRESHOLD)
            {
                updateStep = 0;
                return;
            }
#endif
            Update(dt);

            while (updateStep >= FIXED_UPDATE_STEP)
            {
                updateStep -= FIXED_UPDATE_STEP;

                FixedUpdate(FIXED_UPDATE_STEP);
                LateUpdate(FIXED_UPDATE_STEP);
            }
        }

        private void HandleDraw(double dt)
        {
            Draw(updateStep);

            onEndOfFrame?.Invoke(dt);
        }

        private void Update(double dt)
        {
            OnUpdate?.Invoke(dt);
        }

        private void FixedUpdate(double fdt)
        {
            OnFixedUpdate?.Invoke(fdt);
        }

        private void LateUpdate(double fdt)
        {
            OnLateUpdate?.Invoke(fdt);
        }

        private void Draw(double i)
        {
            OnDraw?.Invoke(i);
        }
    }
}
