using System.IO;
using System;
using RobustEngine.Graphics.OpenGL;
using Color = System.Drawing.Color;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
namespace RobustEngine.Graphics
{  


    public class Texture2D 
    {

        private GLTexture texture;

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D()
        {     
            texture = new GLTexture(TextureTarget.Texture2D);
        }            

        public void Load(string path, int slot = 0, InternalFormat Tf = InternalFormat.RGBA)
        {
            using( FileStream strem = File.Open(path, FileMode.Open))
            {
                using (Image<Rgba32> image = Image.Load(strem))
                {
                    texture.Load(image, 0, InternalFormat.RGBA);
                }
            }

        }

        public bool IsOpaqueAt(int x, int y, int level = 0)
        {
           // return Images[level].GetPixelRowSpan(y)[x].A == 255 ? true : false ;
           return false;
        }


    }

}
