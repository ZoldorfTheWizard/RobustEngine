using System.Drawing;
using System.Collections.Generic;
using OpenTK;

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
        }
        
        public void SetRotation(float newRotation)
        {
            Rotation = newRotation;

            Matrix *= Matrix4.CreateRotationY(Rotation);
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void SetScale(Vector2 newScale)
        {
            Scale = newScale;
        }        

        public void Draw()
        {
           
        }
        

    }
}
