//#define ALLOW_UPDATE_RESET

using System;
using System.Collections.Generic;

namespace App.Update
{
    public delegate void DeltaHandler(double dt);

    public sealed partial class UpdateManager : AbstractDisposable
    {
        private const int POOLED_HELPERS = 10;
        private const double FIXED_UPDATE_STEP = 1.0 / App.FIXED_FRAMERATE; // Simulate in 60 FPS
        private const double FIXED_DRAW_STEP = 1.0 / App.FIXED_FRAMERATE; // Locked FPS as a const for now; TODO: Have user set draw FPS
        private const double RESET_THRESHOLD = 1.0 / 4.0; // XNA caps at 0.5 dt but our threshold is lower

        private double updateStep = 0.0;
        private double drawStep = 0.0;
        private HashSet<UpdateHelper> helpers = new();

        /// <summary>
        /// Use for user input, networked input & output, or timing.
        /// </summary>
        public event Action<double> OnUpdate = null;

        /// <summary>
        /// Use for game logic and physics updates.
        /// </summary>
        public event Action<double> OnFixedUpdate = null;

        /// <summary>
        /// A second fixed update that occurs after; useful for ordering execution.
        /// </summary>
        public event Action<double> OnLateUpdate = null;

        /// <summary>
        /// Use only for draw code. Interpolation is passed for smoother on-screen movement.
        /// </summary>
        public event Action<double> OnDraw = null;

        /// <summary>
        /// Draw at a fixed time step. False will draw on every frame possible.
        /// </summary>
        public bool UseFixedDraw { get; set; } = true;

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
            for (int i = 0; i < POOLED_HELPERS; i++)
            {
                helpers.Add(new UpdateHelper(i, this));
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
                updateStep = 0.0;
                drawStep = 0.0;
            
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
            double interpolation = updateStep / dt;

            if (UseFixedDraw)
            {
                drawStep += dt;

                if (drawStep >= FIXED_DRAW_STEP)
                {
                    drawStep -= FIXED_DRAW_STEP;

                    Draw(interpolation);
                }
            }
            else
            {
                Draw(interpolation);
            }
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
