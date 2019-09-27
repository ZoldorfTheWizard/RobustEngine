using System;
using System.IO;
using System.Runtime.InteropServices;
using RobustEngine.Graphics.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;


namespace RobustEngine.Graphics
{  
    public sealed class Texture2D 
    {

        private ITexture _texture;

        public Image<Rgba32> Image;        

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D()
        { 
            _texture = GraphicsAPI.GetTextureImplementation(TextureTarget.Texture2D);
        }            

        /// <summary>
        /// Constructs a new 2D Texture 
        /// </summary>
        /// <param name="path">Path to texture.</param>
        /// <param name="PIF">Pixel format. Default is RGBA.</param>
        public Texture2D(TextureParams TP) 
        {     
           _texture = GraphicsAPI.GetTextureImplementation(TextureTarget.Texture2D,TP);
        }    


        public void Bind(int slot = 0)
        {
            _texture.Bind(slot);
        }

        public void Unbind()
        {            
            _texture.Unbind();
        }

        public unsafe void LoadImageUsingImagesharp(string path, int slot = 0, InternalFormat IF = InternalFormat.RGBA)
        {
            using( FileStream stream = File.Open(path, FileMode.Open))
            {
                using (Image<Rgba32> img = (Image<Rgba32>) SixLabors.ImageSharp.Image.Load(stream))
                {
                    Image = img;
                    fixed (Rgba32* dataptr = &MemoryMarshal.GetReference(img.GetPixelSpan()))
                    {
                        _texture.Bind(slot);
                        _texture.Create(img.Width, img.Height,(IntPtr) dataptr, IF);
                        _texture.Unbind();
                    }
                }
            }
        }

        public unsafe void LoadSubImageUsingImageSharp(string path, int slot = 0, InternalFormat IF = InternalFormat.RGBA)
        {
            // using( FileStream stream = File.Open(path, FileMode.Open))
            // {
            //     using (Image<Rgba32> img = (Image<Rgba32>)  SixLabors.ImageSharp.Image.Load(stream))
            //     {
            //         Image = img;
                    
            //        fixed (Rgba32* dataptr = &MemoryMarshal.GetReference(img.GetPixelSpan()))
            //        {
            //            _texture.Bind(slot);
            //            _texture.Create(img.Width, img.Height,(IntPtr) dataptr, IF);
            //            _texture.Unbind();
            //        }
            //     }
            // }
            // TODO load subrect
        }

        public bool IsOpaqueAt(int x, int y)
        {
           return Image.GetPixelRowSpan(y)[x].A == 255 ? true : false;
        }

    }

}
