using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.Sprites
{
    public class SpriteBatch
    {
        private int VAOID; // Vertex Array ObjectID
        private int VBOID; // Vertex Buffer ObjectID
        private int IBOID; // Index Buffer ObjectID

        public string Key;
        public float ScreenW;
        public float ScreenH;

        private Texture2D CurrentTexture;
        private List<IRenderable2D> RenderQueue;

        private Stack<Matrix4> MatrixStack;


        public SpriteBatch(float screenwidth, float screenheight)
        {
            ScreenW = screenwidth;
            ScreenH = screenheight;

            Setup();                     
        }


        private void Setup()
        {
            VAOID = GL.GenVertexArray();
            VBOID = GL.GenBuffer();


            GL.BindBuffer(BufferTarget.ArrayBuffer, VBOID);

           // GL.Enable(EnableCap.VertexArray);
           //GL.BindVertexArray(VBOID);
          
          

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
           
        }

        public void Begin()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-ScreenW/2,ScreenW/2,ScreenH/2,-ScreenH/2,0f,1f); // 0,0 is center of world

        }



        //TODO optimize
        public void Draw(IRenderable2D Renderable)
        {
            RenderQueue.Add(Renderable);
        }


        //   VAOID = GL.GenBuffer();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, VAOID);
        //      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, &vertices[0], BufferUsageHint.DynamicDraw);
          
                   
        

        public void End()
        {

        }
    }
}
