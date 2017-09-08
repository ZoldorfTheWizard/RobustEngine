using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics.Shapes;

namespace RobustEngine.Graphics.Sprites
{
    public class Sprite : IRenderable2D, ITransformable2D
    {

        public string ID;

        public Texture2D Texture;
        public Rectangle AABB;
        public Rect2D Rect;


        public Color Color => Rect.FillColor;

        public Vector2 Origin => Rect.Origin;
        public Vector2 Scale => Rect.Scale;
        public float Rotation => Rect.Rotation;
        public Vector2 Position => Rect.Position;

        public Debug DebugMode => Rect.DebugMode;

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
        /// Setup defaults.
        /// </summary>
        private void Setup()
        {
            AABB = Texture.AABB;
            Rect = new Rect2D(AABB);
        }

        public void Update()
        {
            Rect.Update();
        }

        /// <summary>
        /// Sets the origin of the sprite
        /// </summary>
        /// <param name="newScale"></param>
        public void SetOrigin(Vector2 newOrigin)
        {
            Rect.SetOrigin(newOrigin);
        }

        /// <summary>
        /// Sets the scale of the sprite
        /// </summary>
        /// <param name="newScale"></param>
        public void SetScale(Vector2 newScale)
        {
            Rect.SetScale(newScale);
        }

        /// <summary>
        /// Sets the rotation of the sprite. 
        /// </summary>
        /// <param name="newRotation"> new rotation</param>
        public void SetRotation(float newRotation, Axis axis)
        {
            Rect.SetRotation(newRotation, axis);
        }

        /// <summary>
        /// Sets the world Position of the sprite
        /// </summary>
        /// <param name="newPosition">New position.</param>
        public void SetPosition(Vector2 newPosition)
        {
            Rect.SetPosition(newPosition);
        }

        /// <summary>
        /// Sets the Fill Color of the sprite.
        /// </summary>
        /// <param name="color">Color.</param>
        public void SetColor(Color color)
        {
            Rect.FillColor = color;
        }


        /// <summary>
        /// Draws the sprite.
        /// </summary> 
        public void Draw()
        {

            if (Texture == null)
            {
                Rect.Draw();
                return;
            }

            Texture.Bind();
            Rect.Draw();
            Texture.Unbind();
        }

    }
}
