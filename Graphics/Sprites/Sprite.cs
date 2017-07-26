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

        public Color Color;
        public Rectangle AABB;
        public Texture2D Texture;

        public Vector2 Scale;
        public Vector2 Position;
        public Vector2 Size;
        public float Rotation;

        private Matrix4 Matrix;
        private BufferObject VBO;

             
        /// <summary>
        /// Sprite Constructor. 
        /// </summary>
        /// <param name="id">Name of the sprite</param>
        /// <param name="texture">Texture of thesprite</param>
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
            AABB  = Texture.TextureAABB;
            Size        = new Vector2(AABB.Width, AABB.Height);
            Rotation    = 0.0f;
            Scale       = Vector2.One;
            Position    = Vector2.Zero;
            Matrix      = Matrix4.Identity;

            VBO = new BufferObject();
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
        /// Sets the matrix back to Identity.
        /// </summary>
        public void PopMatrix()
        {
            Matrix = Matrix4.Identity;
        }

        /// <summary>
        /// Sets the rotation of the sprite. 
        /// </summary>
        /// <param name="newRotation"> new rotation</param>
        public void SetRotation(float newRotation)
        {
            Rotation = newRotation;

            Matrix *= Matrix4.CreateRotationY(Rotation);
        }


        /// <summary>
        /// Sets the world Position of the sprite
        /// </summary>
        /// <param name="newPosition">New position.</param>
        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;

            Matrix *= Matrix4.CreateTranslation(Position.X, Position.Y, 1);
        }

        public void mov(float move)
        {
            VBO.move(move);
        }

        public void update()
        {
            VBO.Update();
        }
        /// <summary>
        /// Sets the scale of the sprite
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector2 newScale)
        {
            Scale = newScale;

            Matrix *= Matrix4.CreateScale(Scale.X, Scale.Y, 1);
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary> 
        public void Draw()
        {
			Texture.Bind();
            VBO.BindVertexArray();
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 4);
            VBO.BindIndexBuffer();
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
			Texture.Unbind();
        }

    }
}
