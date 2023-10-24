using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace App.ECS
{
    /// <summary>
    /// TBA
    ///
    /// Components: Transform, ???
    /// </summary>
    public class BasicEffectSystem : AbstractSystem
    {
        private static readonly float AspectRatio = App.GraphicsDevice.Viewport.Width / App.GraphicsDevice.Viewport.Height;
        private static readonly Matrix View = Matrix.CreateLookAt(new Vector3(0.0f, 10.0f, 0.0f), Vector3.Zero, Vector3.Up);
        private static readonly Matrix Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0f), AspectRatio, 1.0f, 10000.0f);

        public override void Enable(ECSWorld world)
        {
            App.UpdateManager.OnDraw += OnDraw;

            base.Enable(world);
        }

        public override void Disable()
        {
            App.UpdateManager.OnDraw -= OnDraw;

            base.Disable();
        }

        public void OnDraw(double dt)
        {
            var (Count, C1) = World.GetArchetype<Transform>();
            Model m = null;

            Matrix[] transforms = new Matrix[m.Bones.Count];
            m.CopyAbsoluteBoneTransformsTo(transforms);
            foreach (ModelMesh mesh in m.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects.Cast<BasicEffect>())
                {
                    effect.EnableDefaultLighting();

                    effect.View = View;
                    effect.Projection = Projection;
                    effect.World = transforms[mesh.ParentBone.Index] * Matrix.CreateTranslation(Vector3.Zero);
                }

                mesh.Draw();
            }
        }
    }
}

