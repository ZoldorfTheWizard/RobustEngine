using System;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLHelper
    {

        public static GLTextureParams DefaultGLTextureParams = new GLTextureParams()
        {
           MinFilter= TextureMinFilter.Linear,
           MagFilter= TextureMagFilter.Nearest,
           WrapModeS= TextureWrapMode.Repeat,
           WrapModeT= TextureWrapMode.Repeat,
        };

        public static PixelInternalFormat CheckTextureFormat(InternalFormat TF)
        {
            switch(TF)
            {
                case InternalFormat.RGBA : return PixelInternalFormat.Rgba; 
                case InternalFormat.Depth : return PixelInternalFormat.DepthComponent;
                default : return PixelInternalFormat.Rgba;
            }
        }

        public static GLTextureTarget CheckTextureTarget(TextureTarget TT)
        {
             switch(TT)
            {
                case TextureTarget.Texture1D : return GLTextureTarget.Texture1D;
                case TextureTarget.Texture2D : return GLTextureTarget.Texture2D;
                case TextureTarget.Texture3D : return GLTextureTarget.Texture3D;
                default : return GLTextureTarget.Texture2D;
            }    
        }

         public static GenerateMipmapTarget CheckGenerateMipMapTarget(GLTextureTarget TT)
        {
             switch(TT)
            {
                case GLTextureTarget.Texture1D : return GenerateMipmapTarget.Texture1D;
                case GLTextureTarget.Texture2D : return GenerateMipmapTarget.Texture2D;
                case GLTextureTarget.Texture3D : return GenerateMipmapTarget.Texture3D;
                default : return GenerateMipmapTarget.Texture2D;
            }    
        }

        public static BufferUsageHint CheckUsageHint(UsageHint UH)
        {
            switch (UH)
            {
                case UsageHint.Dynamic : 
                    switch(UH)
                    {
                        case UsageHint.Read  : return  BufferUsageHint.DynamicRead;     
                        case UsageHint.Write : return  BufferUsageHint.DynamicDraw;  
                        case UsageHint.Copy  : return  BufferUsageHint.DynamicCopy;  
                        default : return BufferUsageHint.DynamicDraw;
                    }               
                case UsageHint.Static :
                    switch(UH)
                    {
                        case UsageHint.Read  : return  BufferUsageHint.StaticRead;   
                        case UsageHint.Write : return  BufferUsageHint.StaticDraw; 
                        case UsageHint.Copy  : return  BufferUsageHint.StaticCopy;  
                        default : return BufferUsageHint.StaticDraw;
                    }
                case UsageHint.Stream : 
                    switch(UH)
                    {
                        case UsageHint.Read  : return  BufferUsageHint.StreamRead;    
                        case UsageHint.Write : return  BufferUsageHint.StreamDraw;   
                        case UsageHint.Copy  : return  BufferUsageHint.StreamCopy; 
                        default : return BufferUsageHint.StreamDraw;
                    }
                default : return BufferUsageHint.DynamicDraw;
            }
        }

        public static GLTextureParams CheckTextureParams(TextureParams RobustTexParams)
        {
            GLTextureParams GLTexParams = new GLTextureParams();
            
            switch (RobustTexParams.MinFilter)
            {
                case TextureMinFilterParams.GL_LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMinFilterParams.GL_NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMinFilterParams.GL_LINEAR_MIPMAP_LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.LinearMipmapLinear;
                    break;
                case TextureMinFilterParams.GL_LINEAR_MIPMAP_NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.LinearMipmapNearest;
                    break;
                case TextureMinFilterParams.GL_NEAREST_MIPMAP_LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapLinear;
                    break;
                case TextureMinFilterParams.GL_NEAREST_MIPMAP_NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapNearest;
                    break;
                default :
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapLinear;
                    break;
            }

            switch (RobustTexParams.MagFilter)
            {
                case TextureMagFilterParams.GL_LINEAR :
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMagFilterParams.GL_NEAREST :
                    GLTexParams.MinFilter=TextureMinFilter.Nearest;
                    break;
                default :
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
            }

            switch(RobustTexParams.WrapModeS)
            {
                case TextureWrapParams.GL_REPEAT:
                    GLTexParams.WrapModeS=TextureWrapMode.Repeat;
                    break;
                case TextureWrapParams.GL_MIRRORED_REPEAT:
                    GLTexParams.WrapModeS=TextureWrapMode.MirroredRepeat;
                    break;
                case TextureWrapParams.GL_CLAMP_TO_EDGE:
                    GLTexParams.WrapModeS=TextureWrapMode.ClampToEdge;
                    break;
                case TextureWrapParams.GL_CLAMP_TO_BOARDER:
                    GLTexParams.WrapModeS=TextureWrapMode.ClampToBorder;
                    break;
                default :
                      GLTexParams.WrapModeS=TextureWrapMode.Repeat;
                    break;
            }

            switch(RobustTexParams.WrapModeT)
            {
                case TextureWrapParams.GL_REPEAT:
                    GLTexParams.WrapModeT=TextureWrapMode.Repeat;
                    return GLTexParams;;
                case TextureWrapParams.GL_MIRRORED_REPEAT:
                    GLTexParams.WrapModeT=TextureWrapMode.MirroredRepeat;
                    return GLTexParams;;
                case TextureWrapParams.GL_CLAMP_TO_EDGE:
                    GLTexParams.WrapModeT=TextureWrapMode.ClampToEdge;
                    return GLTexParams;;
                case TextureWrapParams.GL_CLAMP_TO_BOARDER:
                    GLTexParams.WrapModeT=TextureWrapMode.ClampToBorder;
                    return GLTexParams;;
                default :
                    GLTexParams.WrapModeT=TextureWrapMode.Repeat;
                    return GLTexParams;
            }

        }

    }

   

    public enum InternalFormat
    {
        RGBA,
        Depth,
        Stencil
    }
   

    public enum TextureTarget
    {
        Texture1D,
        Texture2D,
        Texture3D,        
    }

    public enum TextureMinFilterParams
    {
        GL_NEAREST,
        GL_LINEAR,
        GL_NEAREST_MIPMAP_NEAREST,
        GL_LINEAR_MIPMAP_NEAREST,
        GL_NEAREST_MIPMAP_LINEAR,
        GL_LINEAR_MIPMAP_LINEAR,
        GL_REPEAT,
    }

    public enum TextureMagFilterParams
    {
        GL_NEAREST,
        GL_LINEAR,
    }

    public enum TextureWrapParams
    {
        GL_REPEAT,
        GL_MIRRORED_REPEAT,
        GL_CLAMP_TO_EDGE,
        GL_CLAMP_TO_BOARDER
    }  

    public struct TextureParams
    {
        public TextureMagFilterParams MagFilter;
        public TextureMinFilterParams MinFilter;        
        public TextureWrapParams WrapModeS;
        public TextureWrapParams WrapModeT;
    }
    
    public enum DrawTarget
    {
        Screen,
        Framebuffer,
        Offscreen
    }



}
