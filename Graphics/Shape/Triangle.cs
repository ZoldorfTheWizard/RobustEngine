using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shape
{
    public class Triangle : IRenderable2D, ITransformable2D
    {
        //
        public float X;
        public float Y;
        public float Width;
        public float Height;

        //Points
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 CenterTop;
        public Vector2 Center; // Centroid

        //Transformation
        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Position;

        //Buffers
        public int VertexArrayID;
        public int VertexBufferID;
        public int IndexBufferID;
        public int[] Indicies;

        //Tri Specific
        public Color FillColor;
        public Vertex[] VertexData;
        public Matrix4 Matrix;
        public Debug DebugMode;

        public Triangle(int posX, int posY, int width, int height)
        {
            Create(posX, posY, width, height, Color.DarkMagenta);
        }
        public Triangle(double posX, double posY, double width, double height)
        {
            Create((float)posX, (float)posY, (float)width, (float)height, Color.DarkMagenta);
        }

        public Triangle(float posX, float posY, float width, float height)
        {
            Create(posX, posY, width, height, Color.DarkMagenta);
        }


        private void Create(float posX, float posY, float width, float height, Color fillcolor)
        {
            X = posX;
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
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length, IntPtr.Zero, BufferUsageHint.DynamicDraw);

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
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);


        }

        public void SetFillColor(Color col)
        {
            FillColor = col;
            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].SetColor(FillColor);
                VertexData[i].A = .5f;
            }
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
            Matrix *= Matrix4.CreateTranslation(-Origin.X, -Origin.Y, 0);
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
        /// <param name="axis"> axis to rotate </param>
        public void SetRotation(float newRotation, Axis axis)
        {
            Rotation = newRotation;
            switch (axis)
            {
                case Axis.X: Matrix *= Matrix4.CreateRotationX(Rotation); break;
                case Axis.Y: Matrix *= Matrix4.CreateRotationY(Rotation); break;
                case Axis.Z: Matrix *= Matrix4.CreateRotationZ(Rotation); break;
            }

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
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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
            GL.DrawElements(PrimitiveType.Triangles, 3, DrawElementsType.UnsignedInt, 0);
            Unbind();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            PopMatrix();
        }

        #endregion Rendering









    }
}
