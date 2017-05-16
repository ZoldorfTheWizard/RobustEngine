using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Graphics
{
    public class View
    {

        public Vector2 Position;
        public double Rotation;
        public double Zoom;
        public float Scale;

        private Matrix4 ViewMatrix;


        public Vector2 ToWorld(Vector2 input)
        {
            input /= (float)Zoom;
            Vector2 dx = new Vector2((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Vector2 dy = new Vector2((float)Math.Cos(Rotation + MathHelper.PiOver2), ((float)Math.Sin(Rotation + MathHelper.PiOver2)));

            return (this.Position + dx * input.X + dy * input.Y);
        }
      
        public View(Vector2 pos, double rot, double zoom)
        {
            Position = pos;
            Rotation = rot;
            Zoom = zoom;
            Scale = 1;
        }

        public void Update()
        {
            GL.MultMatrix(ref ViewMatrix);
        }

        public void Setup(int ScreenW, int ScreenH)
        {

      

            Matrix4 Transform = Matrix4.Identity;

            Transform = Matrix4.Mult(Transform, Matrix4.CreateTranslation(-Position.X, -Position.Y, 0));
            Transform = Matrix4.Mult(Transform, Matrix4.CreateRotationZ((float)-Rotation));
            Transform = Matrix4.Mult(Transform, Matrix4.CreateScale((float)Zoom, (float)Zoom, 0.0f));
            ViewMatrix = Transform;

            GL.MultMatrix(ref Transform);
        }




    }
}
