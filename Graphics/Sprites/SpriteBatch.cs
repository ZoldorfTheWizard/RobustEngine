using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Sprites
{
    public class SpriteBatch
    {
        private int VAOID; // Vertex Array Object ID
        private int VBOID; 

        public string Key;
        public float ScreenW;
        public float ScreenH;

        private Texture2D reuse;


        public SpriteBatch(float screenwidth, float screenheight)
        {
            ScreenW = screenwidth;
            ScreenH = screenheight;

            Setup();                     
        }


        private void Setup()
        {
            VAOID = GL.GenBuffer();

            VBOID = GL.GenBuffer();
           
        }

        public void Begin()
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-ScreenW/2,ScreenW/2,ScreenH/2,-ScreenH/2,0f,1f);
        }



        //TODO optimize
        public void Draw(Texture2D texture, Vector2 position , Vector2 scale, Color color, Vector2 origin)
        {

            Vector2[] vertices = new Vector2[4]
         {
               Vector2.Zero,
               Vector2.UnitX,
               Vector2.One,
               Vector2.UnitY
         };


            if (reuse != texture)
                texture.Bind();
           
            GL.Begin(PrimitiveType.Quads);

            GL.Color3(Color.AliceBlue);

            for (int i = 0; i < 4; i++)
            {

                GL.TexCoord2(vertices[i]);

                position.X *= texture.TextureAABB.Width;
                position.Y *= texture.TextureAABB.Height;
                vertices[i] -= origin;
                vertices[i] *= scale;
                vertices[i] += position; //translate
                GL.Vertex2(vertices[i]);
            }       
        
         
        
            GL.End();

         //   VAOID = GL.GenBuffer();
        //    GL.BindBuffer(BufferTarget.ArrayBuffer, VAOID);
      //      GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, &vertices[0], BufferUsageHint.DynamicDraw);
            
                   
        }


        public void End()
        {

        }
    }
}
