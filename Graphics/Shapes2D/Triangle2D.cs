using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shapes2D
{
    public class Triangle2D : Shape2D
    {
       
        public Vector2 Center;        
        public Vector2 BottomLeft;
        public Vector2 CenterTop;
        public Vector2 BottomRight;
        

        public Triangle2D()
        {
            Create(0,0,1,1, Color.DarkMagenta);
        }

        public Triangle2D(int posX, int posY, int width, int height)
        {
            Create(posX, posY, width, height, Color.DarkMagenta);
        }

        public Triangle2D(double posX, double posY, double width, double height)
        {
            Create((float)posX, (float)posY, (float)width, (float)height, Color.DarkMagenta);
        }

        public Triangle2D(float posX, float posY, float width, float height)
        {
            Create(posX, posY, width, height, Color.DarkMagenta);
        }


        private void Create(float x, float y, float w, float h, Color fillcolor)
        {
            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,                
            };

            Indicies = new int[]
            {
                0, 1, 2
            };
            
            ModelMatrix = Matrix4.Identity;
            Scale = Vector2.One;
            Position = new Vector2(x, y);
            Size = new Vector2(w,h);
            FillColor = fillcolor;

            float XAverage = 0;
            float YAverage = 0;

            for (int i = 0; i < VertexData.Length; i++)
            {
                XAverage += VertexData[i].X;
                YAverage += VertexData[i].Y;
            }

            Center = new Vector2((XAverage / 3), (YAverage / 3));     
            BottomLeft = VertexData[0].ToVector2();
            CenterTop = VertexData[1].ToVector2();
            BottomRight = VertexData[2].ToVector2();         

            SetTextureMapping();        
       
        }

        public override void SetTextureMapping()
        {
            VertexData[0].Tx = 0;
            VertexData[0].Ty = 1;

            VertexData[1].Tx = 1;
            VertexData[1].Ty = 1;

            VertexData[2].Tx = 1;
            VertexData[2].Ty = 0;
        }

        public override void SetSize(Vector2 newSize)
        {
            VertexData[1].X *= newSize.X;
            VertexData[2].X *= newSize.X;
            VertexData[2].Y *= newSize.Y;
        }
           
    }
}
