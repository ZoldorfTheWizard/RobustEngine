using System;
using System.IO;
using System.Runtime.InteropServices;
using RobustEngine.Graphics.OpenGL;
using Color = System.Drawing.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RobustEngine.Graphics
{  


    public class Texture2D : GLTexture
    {

        public Image<Rgba32> Image;

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D() : base(TextureTarget.Texture2D)
        {     
            SetTextureParams(GLHelper.DefaultGLTextureParams);
        }            

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D(TextureParams TP) : base(TextureTarget.Texture2D)
        {     
            SetTextureParams(GLHelper.CheckTextureParams(TP));
        }    

        public unsafe void LoadImage(string path, int slot = 0, InternalFormat IF = InternalFormat.RGBA)
        {
            using( FileStream stream = File.Open(path, FileMode.Open))
            {
                using (Image<Rgba32> image = SixLabors.ImageSharp.Image.Load(stream))
                {
                    Image = image;
                    fixed (Rgba32* pin = &MemoryMarshal.GetReference(image.GetPixelSpan()))
                    {
                        GLCreate((IntPtr) pin, image.Width, image.Height, slot, IF);
                    }
                }
            }
        }

        public bool IsOpaqueAt(int x, int y)
        {
           return Image.GetPixelRowSpan(y)[x].A == 255 ? true : false;
        }


    }

}
