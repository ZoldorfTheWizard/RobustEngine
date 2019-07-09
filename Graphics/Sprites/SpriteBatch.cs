using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.Sprites
{
    public class SpriteBatch : Batch
    {
    
        private Texture2D CurrentTexture;
        private List<Shape2D> RenderQueue;

        private Stack<Matrix4> MatrixStack;


        public SpriteBatch(string key, float width, float height) : base(key,width,height)
        {
                     
        }




        //TODO optimize
        public void Queue(Shape2D Renderable)
        {
            Vector4 pos = Vector4.One;
            Matrix4 mat = Renderable.ModelMatrix;
            Vector4.Transform(ref pos, ref mat, out pos);
            var y = Renderable.ModelMatrix.Row1[3];
        }


        //   VAOID = GL.GenBuffer();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, VAOID);
        //      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, &vertices[0], BufferUsageHint.DynamicDraw);
    }
}
