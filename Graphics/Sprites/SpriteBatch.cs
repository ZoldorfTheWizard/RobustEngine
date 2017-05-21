using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.Sprites
{
    public class SpriteBatch
    {
        private int VAOID; // Vertex Array Object ID
        private int VBOID;
        private int IBOID;

        public string Key;
        public float ScreenW;
        public float ScreenH;

        private Texture2D reuse;
        private List<IRenderable2D> Queue;
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

            GL.Enable(EnableCap.VertexArray);
            GL.BindVertexArray(VBOID);
       
          

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

            Queue.Add(Renderable);
        }

        //   Vector2[] vertices = new Vector2[4]
        //{
        //      Vector2.Zero,
        //      Vector2.UnitX,
        //      Vector2.One,
        //      Vector2.UnitY
        //};
            
           
        //   GL.Begin(PrimitiveType.Quads);

        //   GL.Color3(Color.AliceBlue);

        //   for (int i = 0; i < 4; i++)
        //   {

        //       GL.TexCoord2(vertices[i]);

        //       vertices[i].X *= texture.TextureAABB.Width;
        //       vertices[i].Y *= texture.TextureAABB.Height;
        //       vertices[i] -= origin;
        //       vertices[i] *= scale;
        //       vertices[i] += position; //translate      
        //       GL.Vertex2(vertices[i]);
        //   }       
        
         
        
        //   GL.End();

        //   VAOID = GL.GenBuffer();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, VAOID);
        //      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, &vertices[0], BufferUsageHint.DynamicDraw);
          
                   
        

        public void End()
        {

        }
    }
}
