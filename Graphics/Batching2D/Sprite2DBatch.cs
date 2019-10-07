using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.Batching2D
{
    public sealed class Sprite2DBatch : Batch2D
    {
    
        private Texture2D CurrentTexture;
        private List<Shape2D> RenderQueue;

        private Stack<Matrix4> MatrixStack;


        public Sprite2DBatch(string key, float width, float height) : base(key,width,height)
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
   }
}
