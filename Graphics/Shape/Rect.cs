using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using SysRectangle = System.Drawing.Rectangle;

namespace RobustEngine.Graphics.Shape
{
    public class Rect : IRenderable2D
    {
        #region Class Variables

        public float X;
        public float Y;
        public float Width;
        public float Height;

        public float Left => X;
        public float Top => Y;
        public float Right => Width;
        public float Bottom => Height;

        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Position;

        public int VertexArrayID;
        public int VertexBufferID;
        public int IndexBufferID;
        public int[] Indicies;

        public Color FillColor;
        public Vertex[] VertexData;
        public Matrix4 Matrix;

        public Debug DebugMode;

        #endregion Class Varables

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect(float posX, float posY, float sizeX, float sizeY)
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
        public Rect(int posX, int posY, int sizeX, int sizeY)
        {
            Create((float)posX, (float)posY, (float)sizeX, (float)sizeY, Color.Red);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect(int posX, int posY, int sizeX, int sizeY, Color fc)
        {
            Create(posX, posY, sizeX, sizeY, fc);
        }

        /// <summary>
        /// Rectangle Entity Constructor.,
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect(SysRectangle rect)
        {
            Create(rect.X, rect.Y, rect.Width, rect.Height, Color.Red);
        }


        private void Create(float posX, float posY, float sizeX, float sizeY, Color fillColor)
        {
            X = posX;
            Y = posY;
            Width = sizeX;
            Height = sizeY;
            FillColor = fillColor;
            Matrix = Matrix4.Identity;
            SetScale(Vector2.One);
            SetPosition(new Vector2(posX, posY));

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
                VertexData[i].SetColor(FillColor);
            }

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
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, Vertex.Stride, 12);
            GL.EnableVertexAttribArray(1); // Layout 1 Color Data

            // Normal Data
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 28);
            GL.EnableVertexAttribArray(2); // Layout 2 Normal Data

            // TextureUVCoords
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 40);
            GL.EnableVertexAttribArray(3); // Layout 3 

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //INDEX DATA
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);
        }

        #region Transformation
        /// <summary>
        /// Push a custom Matrix.
        /// </summary>
        /// <param name="mat"></param>
        public void PushMatrix(Matrix4 mat)
        {
            Matrix *= mat;
        }

        /// <summary>
        /// Sets the matrix back to Identity.
        /// </summary>
        public void PopMatrix()
        {
            Matrix = Matrix4.Identity;
        }

        /// <summary>
        /// Sets the origin of the sprite
        /// </summary>
        /// <param name="newScale"></param>
        public void SetOrigin(Vector2 newOrigin)
        {
            Origin = newOrigin;
            Matrix *= Matrix4.CreateTranslation(Origin.X, Origin.Y, 0);
        }

        /// <summary>
        /// Sets the scale of the sprite
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector2 newScale)
        {
            Scale = newScale;
            Matrix *= Matrix4.CreateScale(Scale.X, Scale.Y, 0);
        }

        /// <summary>
        /// Sets the rotation of the sprite. 
        /// </summary>
        /// <param name="newRotation"> new rotation</param>
        public void SetRotation(float newRotation)
        {
            Rotation = newRotation;
            Matrix *= Matrix4.CreateRotationZ(Rotation);
        }

        /// <summary>
        /// Sets the world Position of the sprite
        /// </summary>
        /// <param name="newPosition">New position.</param>
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
            Matrix *= Matrix4.CreateTranslation(Position.X, Position.Y, 0);

        }
        #endregion

        #region Rendering

        public void Bind()
        {
            BindVertexArray();
            BindIndexBuffer();
        }

        internal void BindVertexArray()
        {
            GL.BindVertexArray(VertexArrayID);
        }

        internal void BindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
        }

        internal void BindIndexBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Update()
        {
            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].SetColor(FillColor);
            }

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //  PopMatrix();
        }

        /// <summary>
        /// Draw this instance. 
        /// </summary>
        public void Draw()
        {
            RobustEngine.CurrentShader.setUniform("ModelMatrix", Matrix);


            switch (DebugMode)
            {
                case Debug.Points: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point); break;
                case Debug.Wireframe: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); break;
            }

            Bind();
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
            Unbind();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            PopMatrix();
        }

        #endregion Rendering

        /// <summary>
        /// Detects if a Rectangle collides or intersects each other.
        /// </summary>
        /// <returns>The intersects.</returns>
        /// <param name="A">A.</param>
        public bool Intersects(Rectangle A)
        {
            // Collision X?
            if (Left + Right >= A.Left && A.Left + A.Right >= Left)
            {
                //Collision Y?
                if (Top + Bottom >= A.Top && A.Top + A.Bottom >= Top)
                {
                    return true; //Collision
                }
            }
            return false;
        }

        public Rectangle Union(Rectangle A, Rectangle B)
        {
            var x = Math.Min(A.Left, B.Left);
            var width = Math.Max(A.Left + A.Right, B.Left);

            var y = Math.Min(A.Top, B.Top);
            var height = Math.Max(A.Top + A.Bottom, B.Top);

            return new Rectangle(x, y, width, height);
        }


    }
}
