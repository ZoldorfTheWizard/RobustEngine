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


        private void Create(float posX, float posY, float width, float height, Color fillcolor)
        {
           /*  X = posX;
            Y = posY;
            Width = width;
            Height = height;
            FillColor = fillcolor;

            Matrix = Matrix4.Identity;

            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One
            };

            SetScale(Vector2.One);
            SetPosition(new Vector2(X, Y));
            SetFillColor(fillcolor);

            VertexData[0].X *= Width;
            VertexData[1].X *= Width;
            VertexData[2].X *= Width;
            VertexData[2].Y *= Height;

            BottomLeft = VertexData[0].ToVector2();
            CenterTop = VertexData[1].ToVector2();
            BottomRight = VertexData[2].ToVector2();

            float XAverage = 0;
            float YAverage = 0;

            for (int i = 0; i < VertexData.Length; i++)
            {
                XAverage += VertexData[i].X;
                YAverage += VertexData[i].Y;
            }

            Center = new Vector2((XAverage / 3), (YAverage / 3));

            VertexData[0].Tx = 0;
            VertexData[0].Ty = 1;

            VertexData[1].Tx = 1;
            VertexData[1].Ty = 1;

            VertexData[2].Tx = 1;
            VertexData[2].Ty = 0;

            Indicies = new int[]
            {
                0, 1, 2
            };

            VertexArrayID = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayID);

            VertexBufferID = GL.GenBuffer();
            IndexBufferID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length, IntPtr.Zero, GLBufferUsageHint);

            // Vertex Data
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            GL.EnableVertexAttribArray(0); // Layout 0 Position

            // Color Data
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, Vertex.Stride, 12);
            GL.EnableVertexAttribArray(1); // Layout 1 Color Data

            // Normal Data
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 28);
            GL.EnableVertexAttribArray(2); // Layout 2 Normal Data

            // TextureUVCoords
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 40);
            GL.EnableVertexAttribArray(3); // Layout 3 Texture Coord Data

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //INDEX DATA
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, UsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);

 */
        }

       
    }
}
