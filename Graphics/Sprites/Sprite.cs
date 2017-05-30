using System.Drawing;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Sprites
{
    public class Sprite : IRenderable2D
    {

        public string ID;               
           
        //GL VBO'S
        public int VertexBuffer;
        public int IndexBuffer;

        public Color ColorBuffer;
        public Rectangle SpriteAABB;
        public Texture2D Texture;
        public Vector2 Scale;
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;

        private Matrix4 Matrix;
    


     
        /// <summary>
        /// Sprite Constructor. 
        /// </summary>
        /// <param name="id">Name Of sprite</param>
        /// <param name="texture">Prebuilt Texture for the sprite</param>
        public Sprite(string id, Texture2D texture)
        {
            ID = id;
            Texture = texture;
            Setup();
        }   

        /// <summary>
        /// Setup defaults. Push the Identity Matrix on the Matrix Stack.
        /// </summary>
        private void Setup()
        {          
            SpriteAABB  = Texture.TextureAABB;
            Size        = new Vector2(SpriteAABB.Width, SpriteAABB.Height);
            Rotation    = 0.0f;
            Scale       = Vector2.One;
            Position    = Vector2.Zero;
            Matrix      = Matrix4.Identity;

            VertexBuffer = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBuffer);
           // GL.BufferData()
        }
            
        /// <summary>
        /// Push a custom Matrix.
        /// </summary>
        /// <param name="mat"></param>
        public void PushMatrix(Matrix4 mat)
        {
            Matrix *= mat;
        }

        /// <summary>
        /// Set the matrix back to Identity.
        /// </summary>
        public void PopMatrix()
        {
            Matrix = Matrix4.Identity;
        }


        public void SetRotation(float newRotation)
        {
            Rotation = newRotation;

            Matrix *= Matrix4.CreateRotationY(Rotation);
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;

            Matrix *= Matrix4.CreateTranslation(Position.X, Position.Y, 1);
        }

        public void SetScale(Vector2 newScale)
        {
            Scale = newScale;

            Matrix *= Matrix4.CreateScale(Scale.X, Scale.Y, 1);
        }        

        public void Draw()
        {
           
        }
        

    }
}
