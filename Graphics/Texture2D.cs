using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using PixelFormat = System.Drawing.Imaging.PixelFormat;

namespace RobustEngine.Graphics
{
    public class Texture2D
    {

        public int ID;
        public Rectangle AABB;

        private int TextureSlot;
        private Bitmap Bitmap;
        private BitmapData BitmapData;
        private Color[,] PixelData;

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D(string path, PixelInternalFormat PIF = PixelInternalFormat.Rgba)
        {
            Load(path, PIF);
        }

        /// <summary>
        /// Bind Texture.   
        /// </summary>
        /// <returns>The bind.</returns>
        /// <param name="TextureUnit">Specify which TextureUnit you want to bind to. 0-16.</param>
        public void Bind(int TexUnit = 0)
        {
            TextureSlot = TexUnit;
            GL.ActiveTexture(TextureUnit.Texture0 + TextureSlot);
            GL.BindTexture(TextureTarget.Texture2D, ID);
        }

        /// <summary>
        /// Unbind Texture
        /// </summary>
        public void Unbind()
        {
            GL.ActiveTexture(TextureUnit.Texture0 + TextureSlot);
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }


        /// <summary>
        /// Loads Textures into OpenGL Using Bitmaps. Supports PNG and JPG.
        /// </summary>
        /// <param name="PIF">Pixel Internal Format</param>
        /// <param name="path">Path.</param>
        private void Load(string path, PixelInternalFormat PIF)
        {
            ID = GL.GenTexture();

            Bind();

            Bitmap = new Bitmap(path);

            AABB = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            PixelData = new Color[AABB.Width, AABB.Height];
            BitmapData = Bitmap.LockBits(AABB, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            GL.TexImage2D
            (
                TextureTarget.Texture2D,
                0,
                PIF,
                AABB.Width,
                AABB.Height,
                0,
                GLPixelFormat.Bgra,
                PixelType.UnsignedByte,
                BitmapData.Scan0
            );

            Bitmap.UnlockBits(BitmapData);

            for (int x = 0; x < Bitmap.Width; x++)
            {
                for (int y = 0; y < Bitmap.Height; y++)
                {
                    PixelData[x, y] = Bitmap.GetPixel(x, y);
                }
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //TODO Mipmap + Bump map here maybe?

            Unbind();
            Bitmap.Dispose();
        }

        public bool IsOpaqueAt(int x, int y)
        {
            return PixelData[x, y].A != 0;
        }


    }

}
