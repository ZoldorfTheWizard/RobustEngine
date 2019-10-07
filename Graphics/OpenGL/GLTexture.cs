using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;
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

    public class GLTexture : ITexture
    {
        #region Accessors
        public int ID 
        {
            get { return id; }
        } 

        public int Slot 
        {
            get{ return slot; } 
            set { slot = value;}
        }

        public int MipMapLevels 
        {
            get{ return mipmaplevels;} 
        }
        #endregion

        private int id;
        private int slot;
        private int mipmaplevels;
        private int width;
        private int height;
       
        internal static readonly GLTextureParams DEFAULT_GL_TEXTURE_PARAMS = new GLTextureParams()
        {
           MinFilter= TextureMinFilter.Linear,
           MagFilter= TextureMagFilter.Nearest,
           WrapModeS= TextureWrapMode.Repeat,
           WrapModeT= TextureWrapMode.Repeat,
        };      

        protected GLTextureTarget GLTexTarget;
        protected GLTextureParams GLTexParams;
        protected GenerateMipmapTarget GLMipMapTarget;
        protected PixelInternalFormat GLPixelInternalFormat;         
       
        public GLTexture(GLTextureTarget GLTT, PixelInternalFormat GLPIF = PixelInternalFormat.Rgba)
        {   
            Construct(GLTT,DEFAULT_GL_TEXTURE_PARAMS,GLPIF);
        }


        public GLTexture(GLTextureTarget GLTT, GLTextureParams GLTP, PixelInternalFormat GLPIF = PixelInternalFormat.Rgba)
        {   
            Construct(GLTT,GLTP,GLPIF);
        }

        private void Construct(GLTextureTarget GLTT, GLTextureParams GLTP, PixelInternalFormat GLPIF)
        {
            id = GL.GenTexture();
            GLTexTarget=GLTT;
            GLTexParams=GLTP;
            GLPixelInternalFormat = GLPIF;
            //todo move mipmaplevels to GL_DETECTED_LIMITS
            #if NOTNVIDIA
                mipmaplevels = (int) Math.Log(Math.Max(width,height) + 1, 2);            
            #else
                mipmaplevels = (int) Math.Pow(2,Math.Floor(Math.Log(Math.Min(width,height),2)));
            #endif
        }

        /// <inherit-doc />
        public void Bind(int GLTextureUnit = 0)
        {
            if (GLTextureUnit >= GL_DETECTED_LIMITS.MAX_GL_TEXTURE_IMAGE_UNITS)
            {
                throw new Exception("Exceeded Maximum GLTextureImageUnits." ); 
                //TODO consider throwing away the call and just resuming after warning?
            }               

            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(GLTextureTarget.Texture2D, ID);
        }

        /// <inherit-doc />
        public void Create(int w, int h, IntPtr dataptr, InternalFormat IF = InternalFormat.RGBA)
        {
            width = w;
            height= h;

            //TODO TEXSTORAGE && TEXSUBIMAGE HERE??
            GL.TexImage2D
            (
                GLTexTarget,
                0,
                GLPixelInternalFormat,
                (int) width,
                (int) height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                dataptr
            );

            SetTextureParams(GLTexParams);
        } 

        /// <inherit-doc />
        public void SetTextureParams(TextureParams TP)
        {
            SetTextureParams(GraphicsAPI.GetGLTexParams(TP));
        }
        
      
        /// <inherit-doc />
        public void GenerateMipMaps()
        {
            switch(GLTexTarget)
            {
                case GLTextureTarget.Texture1D :  GL.GenerateMipmap(GenerateMipmapTarget.Texture1D); break;
                case GLTextureTarget.Texture2D :  GL.GenerateMipmap(GenerateMipmapTarget.Texture2D); break;
                case GLTextureTarget.Texture3D :  GL.GenerateMipmap(GenerateMipmapTarget.Texture3D); break;
                default :  GL.GenerateMipmap(GenerateMipmapTarget.Texture2D); break;
                //todo other mipmap levels 
            }    
        }

        /// <inherit-doc />
        public void Unbind()
        {
            GL.BindTexture(GLTextureTarget.Texture2D, 0);            
        }

        /// <summary>
        /// Sets the Texture Parameters for OpenGL
        /// </summary>
        /// <param name="TP">Texture Parameters</param>
        internal void SetTextureParams(GLTextureParams TP)
        {
            GLTexParams = TP;
            
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float) GLTexParams.WrapModeS);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float) GLTexParams.WrapModeT);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)GLTexParams.MinFilter);
            GL.TexParameter(GLTextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)GLTexParams.MagFilter);
        }
        

    }

       
}