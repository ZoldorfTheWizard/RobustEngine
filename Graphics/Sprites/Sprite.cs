using System.Drawing;
using OpenTK;

namespace RobustEngine.Graphics.Sprites
{
    public class Sprite
    {
        public string ID;               

    
        //GL VBO'S
        public int VertexBuffer;
        public int TextureBuffer;
        public int ColorBuffer;

        //Accessors TODO
        public double Rotation;
        public int Width;
        public int Height;
        public Rectangle SpriteAABB;
        public Texture2D Texture;
        public Vector2 Scale;
        public Vector2 Position;

        public Sprite(string id, Texture2D texture)
        {
            ID = id;
            Texture = texture;
            Setup();
        }   

        private void Setup()
        {          
            SpriteAABB = Texture.TextureAABB;
            Width      = SpriteAABB.Width;
            Height     = SpriteAABB.Height;
            Rotation   = 0.0;
            Scale      = Vector2.One;
            Position   = Vector2.Zero;
        }
        
        public void SetRotation(double newRotation)
        {
            Rotation = newRotation;
        }

        public void SetPosition(Vector2 newPosition)
        {
            Position = newPosition;
        }

        public void SetScale(Vector2 newScale)
        {
            Scale = newScale;
        }        

        public void Blit()
        {
           
        }
        

    }
}
