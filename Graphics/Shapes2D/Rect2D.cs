using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shapes2D
{
    public class Rect2D : IRenderable2D, ITransformable2D
    {
        #region Class Variables

        //Position
        public Vector2 Position;
        public Vector2 Size;
        public float X 
        {
            get { return Position.X; } 
            set { SetPosition(new Vector2(value, Y)); }
        }
        
        public float Y
         {
            get { return Position.Y; } 
            set { SetPosition(new Vector2(X, value)); }
        }
        public float Width
        {
            get { return Size.X; } 
            set { SetPosition(new Vector2(value, Y)); }
        }
        public float Height
        {
            get { return Size.X; } 
            set { SetPosition(new Vector2(value, Y)); } 
        }


        //BoundingBox
        public float Left => X;
        public float Top => Y;
        public float Right => X + Width;
        public float Bottom => Y + Height;

        //Point Coords
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopRight;
        public Vector2 TopLeft;
        public Vector2 Center;

        //Transformation Vars       
        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Transform;

        //Opengl VBO's
        public int VertexArrayID;
        public int VertexBufferID;
        public int IndexBufferID;
        public int[] Indicies;

        // Rect Specific
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
        public Rect2D(float posX, float posY, float sizeX, float sizeY)
        {
            Create(posX, posY, sizeX, sizeY, Color.Maroon);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect2D(int posX, int posY, int sizeX, int sizeY)
        {
            Create((float)posX, (float)posY, (float)sizeX, (float)sizeY, Color.Maroon);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect2D(int posX, int posY, int sizeX, int sizeY, Color fc)
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
        public Rect2D(Rect2D rect)
        {
            Create(rect.X, rect.Y, rect.Width, rect.Height, Color.Maroon);
        }


        private void Create(float posX, float posY, float sizeX, float sizeY, Color fillColor)
        {
            X = posX;
            Y = posY;
            Width = sizeX;
            Height = sizeY;

            Matrix = Matrix4.Identity;

            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,
                Vertex.UnitY
            };


            SetScale(Vector2.One);
            SetPosition(new Vector2(posX, posY));
            SetFillColor(fillColor);

            //Size  
            VertexData[1].X *= Width;
            VertexData[2].X *= Width;
            VertexData[2].Y *= Height;
            VertexData[3].Y *= Height;

            //Point Coords
            BottomLeft = VertexData[0].ToVector2();
            BottomRight = VertexData[1].ToVector2();
            TopRight = VertexData[2].ToVector2();
            TopLeft = VertexData[3].ToVector2();
            Center = new Vector2(Width / 2, Height / 2);

            //Texture[] Data 
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
            GL.EnableVertexAttribArray(0); // Layout 0 Position Data

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
        /// <param name="newRotation"> Degrees to rotate by</param>
        /// <param name="axis"> Axis to rotate around </param>
        public void SetRotation(float newRotation, Axis axis)
        {
            Rotation = MathHelper.DegreesToRadians(newRotation);
            switch (axis)
            {
                case Axis.X: Matrix *= Matrix4.CreateRotationX(Rotation); break;
                case Axis.Y: Matrix *= Matrix4.CreateRotationY(Rotation); break;
                case Axis.Z: Matrix *= Matrix4.CreateRotationZ(Rotation); break;
            }
        }

        /// <summary>
        /// Sets the world Position of the 2D Sprite
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
            RobustEngine.CurrentShader.setUniform("UsingTexture", GL.GetInteger(GetPName.TextureBinding2D));

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
        public bool Intersects(Rect2D A)
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

        public Rect2D Union(Rect2D A, Rect2D B)
        {
            var x = Math.Min(A.Left, B.Left);
            var width = Math.Max(A.Left + A.Right, B.Left);

            var y = Math.Min(A.Top, B.Top);
            var height = Math.Max(A.Top + A.Bottom, B.Top);

            return new Rect2D(x, y, width, height);
        }


    }
}
