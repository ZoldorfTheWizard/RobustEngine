using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics.Shapes2D;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Color = System.Drawing.Color;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using SLImage = SixLabors.ImageSharp.Image;

namespace RobustEngine.Graphics
{
    public class Texture2D
    {

        public int ID; 
        public Rect2D AABB;
        public Image<Rgba32>[] Images;
        private int TextureSlot;
        private IntPtr PixelDataMemLoc;

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
        private unsafe void Load(string path, PixelInternalFormat PIF)
        {
            ID = GL.GenTexture();

            Bind();

            // Bitmap = new Bitmap(path);
            // AABB = new Rect2D(0, 0, Bitmap.Width, Bitmap.Height);
            // PixelData = new Color[AABB.Width, AABB.Height];
            // BitmapData = Bitmap.LockBits(AABB, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            using (Image<Rgba32> img = Image.Load(path))
            {       
                var ImgBox = img.Bounds();

                AABB = new Rect2D(ImgBox.X, ImgBox.Y, ImgBox.Width, ImgBox.Height);

                fixed (void* pin = &MemoryMarshal.GetReference(img.GetPixelSpan()))
                {
                    PixelDataMemLoc = (IntPtr) pin;             
                }

                GL.TexImage2D
                (
                    TextureTarget.Texture2D,
                    0,
                    PIF,
                    (int) AABB.Width,
                    (int) AABB.Height,
                    0,
                    GLPixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    PixelDataMemLoc
                );
            
            }
                //TODO Mipmap + Bump map here maybe?
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Unbind();
        }

        public bool IsOpaqueAt(int x, int y, int level = 0)
        {
            return Images[level].GetPixelRowSpan(y)[x].A == 255 ? true : false ;
        }


    }

}
