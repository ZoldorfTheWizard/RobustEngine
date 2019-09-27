using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics.Shapes2D;
using Color = OpenTK.Color;

namespace RobustEngine.Graphics.Sprites2D
{
    public class Sprite2D : Rect2D 
    {

        public string ID;
        public Texture2D Texture;

        public Rect2D AABB;
  
        /// <summary>
        /// Sprite Constructor. 
        /// </summary>
        /// <param name="id">Name of the sprite</param>
        /// <param name="texture">Texture of thesprite</param>
        public Sprite2D(string id, Texture2D texture)
        {
            ID = id;
            Texture = texture;            
        }

        /// <summary>
        /// Draws the sprite.
        /// </summary> 
        public void Draw()
        {

            if (Texture == null)
            {
                //base.Draw();
                return;
            }

            Texture.Bind();

            Texture.Unbind();
        } 
    }
}
