using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shapes2D
{
    public class Line2D : IRenderable2D, ITransformable2D
    {
        //Line
        public float X1;
        public float Y1;
        public float X2;
        public float Y2;
        public float Width;

        //Points
        public Vector2 Center; // Centroid

        //Transformation
        public Vector2 Origin;
        public Vector2 Scale;
        public float Rotation;
        public Vector2 Position;

        //Buffers
        public GLBuffers Buffers;

        //Line Specific
        public Color FillColor;
        public Vertex[] VertexData;
        public int[] Indicies;
        public Matrix4 Matrix;


        public Line2D(int startX, int startY, int endX, int endY, int width = 1)
        {
            Create(startX, startY, endX, endY, width, Color.DarkMagenta);
        }

        public Line2D(double startX, double startY, double endX, double endY, double width = 1.0)
        {
            Create((float)startX, (float)startY, (float)endX, (float)endY, (float)width, Color.DarkMagenta);
        }

        public Line2D(float startX, float startY, float endX, float endY, float width = 1.0f)
        {
            Create(startX, startY, endX, endY, width, Color.DarkMagenta);
        }

        private void Create(float x1, float y1, float x2, float y2, float width, Color fillcolor)
        {
            X1 = x1;
            Y1 = y1;

            X2 = x2;
            Y2 = y2;

            Matrix = Matrix4.Identity;

            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.One
            };
            
            Indicies = new int[]
            {
                0, 1
            };

            VertexData[1].X *= X2;
            VertexData[1].Y *= Y2;

            GL.PointSize(5f);
            GL.LineWidth(width);

            SetScale(Vector2.One);
            SetPosition(new Vector2(X1, Y1));
            SetFillColor(fillcolor);

            Center = new Vector2(X2 / 2, Y2 / 2);           
            Buffers = new GLBuffers();
            Buffers.Init(VertexData, Indicies);
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

        public void SetLineWidth(float width)
        {
            Width = width;
            GL.LineWidth(Width);
        }

        public void SetPointSize(float size)
        {
            GL.PointSize(size);
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
            Rotation = MathHelper.DegreesToRadians(newRotation);
            switch (axis)
            {
                case Axis.X:
                    Matrix *= Matrix4.CreateRotationX(Rotation);
                    break;
                case Axis.Y:
                    Matrix *= Matrix4.CreateRotationY(Rotation);
                    break;
                case Axis.Z:
                    Matrix *= Matrix4.CreateRotationZ(Rotation);
                    break;
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

        public void Update()
        {
           Buffers.UpdateElementBuffer(VertexData);
        }

        /// <summary>
        /// Draw this instance. 
        /// </summary>
        public void Draw()
        {
            RobustEngine.CurrentShader.setUniform("ModelMatrix", Matrix);
            RobustEngine.CurrentShader.setUniform("UsingTexture", GL.GetInteger(GetPName.TextureBinding2D));


            Buffers.BindVertexArray();
            Buffers.BindIndexBuffer();

            GL.DrawElements(PrimitiveType.Lines, 2, DrawElementsType.UnsignedInt, 0);
            
            Buffers.UnbindIndexBuffer();
            Buffers.UnbindVertexArray();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            PopMatrix();
        }
        #endregion Rendering
    }
}
