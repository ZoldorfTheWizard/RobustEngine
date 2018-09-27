using System;
using RobustEngine.Graphics.Shapes2D;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Color = System.Drawing.Color;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;


namespace RobustEngine.Graphics
{
    public class Texture2D
    {

        public int ID;
        public Rect2D AABB;

        private int TextureSlot;
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
        /// <optional name="TextureUnit">Specify which TextureUnit you want to bind to. 0-16.</optional>
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

            // Bitmap = new Bitmap(path);
            // AABB = new Rect2D(0, 0, Bitmap.Width, Bitmap.Height);
            // PixelData = new Color[AABB.Width, AABB.Height];
            // BitmapData = Bitmap.LockBits(AABB, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            GL.TexImage2D
            (
                TextureTarget.Texture2D,
                0,
                PIF,
                1,//AABB.Width,
                1,//AABB.Height,
                0,
                GLPixelFormat.Bgra,
                PixelType.UnsignedByte,
                IntPtr.Zero //BitmapData.Scan0
            );

            // Bitmap.UnlockBits(BitmapData);

            // for (int x = 0; x < Bitmap.Width; x++)
            // {
            //     for (int y = 0; y < Bitmap.Height; y++)
            //     {
            //         PixelData[x, y] = Bitmap.GetPixel(x, y);
            //     }
            // }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            //TODO Mipmap + Bump map here maybe?

            Unbind();
           // Bitmap.Dispose();
        }

        public bool IsOpaqueAt(int x, int y)
        {
            return PixelData[x, y].A != 0;
        }


    }

}
