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

    public struct GLTextureParams
    {
        public TextureMagFilter MagFilter;
        public TextureMinFilter MinFilter;
        public TextureWrapMode WrapModeS;
        public TextureWrapMode WrapModeT;
    }

    public class GLTexture
    {

        private int id;
        private int slot;
        private int mipmaplevels;
        private int width;
        private int height;

        protected GLTextureTarget GLTexTarget;
        protected GLTextureParams GLTexParams;
        protected GenerateMipmapTarget GLMipMapTarget;
        protected PixelInternalFormat GLPixelInternalFormat;

    #region Accessors
        public int ID 
        {
            get { return id; }
        } 

        public int Slot 
        {
            get{ return slot; } 
            set{ CheckImageUnits(value); slot = value; }
        }

        public int MipMapLevels 
        {
            get{ return mipmaplevels;} 
        }
#endregion

        public GLTexture(TextureTarget TT)
        {   
            id = GL.GenTexture();
            GLTexTarget=GLHelper.CheckTextureTarget(TT);
            GLMipMapTarget=GLHelper.CheckGenerateMipMapTarget(GLTexTarget);
        }

        private void CheckImageUnits(int unit)
        {   
            if (unit >= GLLimits.MAX_GL_TEXTURE_IMAGE_UNITS)
            {
                throw new Exception("Exceeded Maximum GLTextureImageUnits.");
            }                    
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

        internal void GLCreate(IntPtr dataptr, int w, int h, int slot = 0, InternalFormat tf = InternalFormat.RGBA)
        {
            width = w;
            height= h;

            GLPixelInternalFormat = GLHelper.CheckTextureFormat(tf);
            Bind(slot);

            //TODO TEXSTORAGE && TEXSUBIMAGE HERE??
            GL.TexImage2D
            (
                GLTexTarget,
                0,
                GLPixelInternalFormat,
                (int) width,
                (int) height,
                0,
                PixelFormat.Rgba,
                PixelType.UnsignedByte,
                dataptr
            );
            RobustEngine.CheckGLErrors();          
           
            Unbind();          
        } 

        public void GenerateMipMapLevels()
        {
            Bind();
            GL.GenerateMipmap(GLMipMapTarget);
            Unbind();

            #if NOTNVIDIA
                mipmaplevels = (int) Math.Log(Math.Max(width,height) + 1, 2);            
            #else
                mipmaplevels = (int) Math.Pow(2,Math.Floor(Math.Log(Math.Min(width,height),2)));
            #endif
        }

        public void SetTextureParams(GLTextureParams GLTexParams)
        {
            Bind();
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) GLTexParams.WrapModeS);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) GLTexParams.WrapModeT);

            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLTexParams.MinFilter);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLTexParams.MagFilter);
            Unbind();
        }
     
    }

       
}