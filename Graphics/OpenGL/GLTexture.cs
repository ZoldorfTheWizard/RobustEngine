using System.Reflection.Metadata;
using System;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{    
    public class GLTexture
    {

        private int id;
        private int slot;

        protected GLTextureTarget GLTextureTarget;
        protected PixelInternalFormat GLPixelInternalFormat;

        public int ID 
        {
            get { return id; }
        } 

        public int Slot 
        {
            get{ return slot; } 
            set{ CheckImageUnits(value); }
        }

        public GLTexture(TextureTarget TT)
        {
            id = GL.GenTexture();
        }

        private void CheckImageUnits(int unit)
        {   
            if (unit >= GLLimits.MAX_GL_TEXTURE_IMAGE_UNITS)
            {
                throw new Exception("Exceeded Maximum TextureImageUnits.");
            }
            slot = unit;            
        }

        /// <summary>
        /// Bind Texture.   
        /// </summary>
        /// <returns>The bind.</returns>
        /// <optional name="TextureUnit">Specify which TextureUnit you want to bind to. 0-16.</optional>
        public void Bind(int TexUnit = 0)
        {
            CheckImageUnits(TexUnit);
            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(GLTextureTarget.Texture2D, ID);
        }

        /// <summary>
        /// Unbind Texture
        /// </summary>
        public void Unbind()
        {
            GL.BindTexture(GLTextureTarget.Texture2D, 0);            
        }

        public void Create(IntPtr dataptr, int width, int height, int slot = 0, InternalFormat tf = InternalFormat.RGBA)
        {
           
            GLPixelInternalFormat = GLHelper.CheckTextureFormat(tf);
            Bind(slot);

            //TODO TEXSTORAGE && TEXSUBIMAGE HERE??
            GL.TexImage2D
            (
                GLTextureTarget.Texture2D,
                0,
                GLPixelInternalFormat,
                (int) width,
                (int) height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                (IntPtr) dataptr
            );
            
            //TODO Mipmap + Bump map here maybe? also move this to enum based
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            Unbind();          
        } 
     
    }

       
}