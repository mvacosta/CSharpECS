using System;

static class Program
{
    [STAThread]
    static void Main(string[] _)
    {
        using var a = new App.App.AppInstance();
        a.Run();
    }
}

namespace App
{
    using ECS;
    using Update;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;

    public static class App
    {
        //TODO: Move these to an exportable/importable engine profile thingy
        public const int FIXED_FRAMERATE = 60;
        public static Color CLEAR_COLOR = new (0.0f, 0.5f, 1.0f);

        #region Singletons

        public static ContentManager Content { get; internal set; } = null;

        public static GraphicsDeviceManager GraphicsDeviceManager { get; internal set; } = null;

        public static GraphicsDevice GraphicsDevice { get; internal set; } = null;

        public static SpriteBatch SpriteBatch { get; internal set; } = null;

        public static UpdateManager UpdateManager { get; internal set; } = null;

        public static GameManager GameManager { get; internal set; } = null;

        public static ECSManager ECSManager { get; internal set; } = null;

        public static void DrawNextFrame()
        {
            AppInstance.drawNextFrame = true;
        }

        #endregion

        #region App Game Instance

        internal sealed class AppInstance : Game
        {
            private DeltaHandler handleUpdate = null;
            private DeltaHandler handleDraw = null;

            internal static bool drawNextFrame = false;

            internal AppInstance()
            {
                GraphicsDeviceManager = new GraphicsDeviceManager(this)
                {
                    PreferredBackBufferWidth = 1280,
                    PreferredBackBufferHeight = 720,
                    IsFullScreen = false,
                    SynchronizeWithVerticalRetrace = true
                };

                IsFixedTimeStep = false;

                Content.RootDirectory = "Content";
            }

            protected override void Initialize()
            {
                base.Initialize();

                App.Content = Content;
                App.GraphicsDevice = GraphicsDevice;
                SpriteBatch = new SpriteBatch(GraphicsDevice);

                UpdateManager = UpdateManager.CreateUpdateManager(out handleUpdate, out handleDraw);
                GameManager = new GameManager();
                ECSManager = new ECSManager();

                UpdateManager.OnUpdate += HandleExitGameInput;
                UpdateManager.OnDraw += HandleClearScreen;
            }

            protected override void Dispose(bool disposing)
            {
                drawNextFrame = false;

                UpdateManager.OnUpdate -= HandleExitGameInput;
                UpdateManager.OnDraw -= HandleClearScreen;

                handleUpdate = null;
                handleDraw = null;

                ECSManager.Dispose();
                ECSManager = null;

                GameManager.Dispose();
                GameManager = null;

                SpriteBatch.Dispose();
                SpriteBatch = null;

                UpdateManager.Dispose();
                UpdateManager = null;

                App.Content = null;
                App.GraphicsDevice = null;
                GraphicsDeviceManager = null;

                base.Dispose(disposing);
            }

            protected override void LoadContent()
            {
                //
            }

            protected override void UnloadContent()
            {
                //
            }

            protected override void Update(GameTime gameTime)
            {
                handleUpdate(gameTime.ElapsedGameTime.TotalSeconds);

                base.Update(gameTime);
            }

            protected override bool BeginDraw()
            {
                if (drawNextFrame)
                {
                    return base.BeginDraw();
                }

                return false;
            }

            protected override void Draw(GameTime gameTime)
            {
                drawNextFrame = false;

                handleDraw(gameTime.ElapsedGameTime.TotalSeconds);

                base.Draw(gameTime);
            }

            private void HandleExitGameInput(double _)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                    Exit();
            }

            private void HandleClearScreen(double _)
            {
                GraphicsDevice.Clear(CLEAR_COLOR);
            }
        }

        #endregion
    }
}
