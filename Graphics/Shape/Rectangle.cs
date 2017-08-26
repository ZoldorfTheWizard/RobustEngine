using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shape
{
    public class Rectangle
    {
        #region Class Variables

        public float X;
        public float Y;
        public float Width;
        public float Height;

        /// <summary>
        /// Gets the left (x pos)
        /// </summary>
        /// <value>The left.</value>
        public float Left => X;

        /// <summary>
        /// Gets the top. (y pos)
        /// </summary>
        /// <value>The top.</value>
        public float Top => Y;

        /// <summary>
        /// Gets the right. (width)
        /// </summary>
        /// <value>The right.</value>
        public float Right => Width;

        /// <summary>
        /// Gets the bottom. (height)
        /// </summary>
        /// <value>The bottom.</value>
        public float Bottom => Height;

        public int VertexArrayID;
        public int VertexBufferID;
        public int IndexBufferID;
        public int[] Indicies;

        public Color FillColor;
        public Vertex[] VertexData;
        #endregion Class Varables

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rectangle(float posX, float posY, float sizeX, float sizeY)
        {
            Create(posX, posY, sizeX, sizeY, Color.Transparent);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rectangle(int posX, int posY, int sizeX, int sizeY)
        {
            Create((float)posX, (float)posY, (float)sizeX, (float)sizeY, Color.Transparent);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rectangle(int posX, int posY, int sizeX, int sizeY, Color fc)
        {
            Create(posX, posY, sizeX, sizeY, fc);
        }

        private void Create(float posX, float posY, float sizeX, float sizeY, Color fillColor)
        {
            X = posX;
            Y = posY;
            Width = sizeX;
            Height = sizeY;
            FillColor = fillColor;

            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,
                Vertex.UnitY
            };

            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].X += X;
                VertexData[i].Y += Y;
            }

            //Set W/H
            VertexData[1].X += Width;
            VertexData[2].X += Width;
            VertexData[2].Y += Height;
            VertexData[3].Y += Height;

            //Texture Data
            VertexData[0].Tx = 0;
            VertexData[0].Ty = 1;

            VertexData[1].Tx = 1;
            VertexData[1].Ty = 1;

            VertexData[2].Tx = 1;
            VertexData[2].Ty = 0;

            VertexData[3].Tx = 0;
            VertexData[3].Ty = 0;

            Indicies = new int[]
            {
                  0, 1, 2, // First Triangle
                  2, 3, 0  // Second Triangle
            };

            VertexArrayID = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayID);

            //Generate Buffers
            VertexBufferID = GL.GenBuffer();
            IndexBufferID = GL.GenBuffer();

            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);

            // Vertex Data
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            GL.EnableVertexAttribArray(0); // Layout 0 Vertex Data

            // Color Data
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 12);
            GL.EnableVertexAttribArray(1); // Layout 1 Color Data

            // Normal Data
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 24);
            GL.EnableVertexAttribArray(2); // Layout 2 Normal Data

            // TextureUVCoords
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 36);
            GL.EnableVertexAttribArray(3); // Layout 3 TexCoords

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //INDEX DATA
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);


        }

        public void Update()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void BindVertexArray()
        {
            GL.BindVertexArray(VertexArrayID);
        }

        public void BindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
        }

        public void BindIndexBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }


        /// <summary>
        /// Detects if a Rectangle collides or intersects each other.
        /// </summary>
        /// <returns>The intersects.</returns>
        /// <param name="A">A.</param>
        public bool Intersects(Rectangle A)
        {
            // Collision X?
            if (X + Width >= A.X && A.X + A.Width >= X)
            {
                //Collision Y?
                if (Y + Height >= A.Y && A.Y + A.Height >= Y)
                {
                    return true; //Collision
                }
            }
            return false;
        }


        public Rectangle Union(Rectangle A, Rectangle B)
        {
            var x = Math.Min(A.Left, B.X);
            var width = Math.Max(A.X + A.Width, B.X);

            var y = Math.Min(A.Y, B.Y);
            var height = Math.Max(A.Y + A.Height, B.Y);

            return new Rectangle(x, y, width, height);
        }


    }
}
