using System;
using System.Collections.Generic;
using RobustEngine.Graphics.Interfaces;
using RobustEngine.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics
{

    public enum GRAPHICS_IMPLEMENTATION 
    {
        OPENGL,
        DX12
    }

    public static class GraphicsAPI
    {

        static Dictionary<Type,object> ResolvedTypes = new Dictionary<Type, object>();

        static GraphicsAPI()
        {
            
        }

        static void Register<Type>(object Implementation)
        {
            ResolvedTypes[typeof(Type)] = Implementation;
        }

        public static T Resolve<T>()
        {
            return (T) ResolveType(typeof(T));
        }

        public static object ResolveType(Type type)
        {
            if (ResolvedTypes.TryGetValue(type, out var value))
            {
                return value;
            }
            else
            {
                throw new Exception("not found");
            }
        }

        public static ITexture GetTextureImplementation(TextureTarget TT, InternalFormat IF = InternalFormat.RGBA)
        {
            var GLTT = CheckTextureTarget(TT);
            var GLIF = CheckInternalFormat(IF);
            return new GLTexture(GLTT,GLIF); //tODO Branch here based on GRAPHICS_IMPLEMENTATION_ENUM
        }

        public static ITexture GetTextureImplementation(TextureTarget TT,TextureParams TP, InternalFormat IF = InternalFormat.RGBA)
        {
            var GLTT = CheckTextureTarget(TT);
            var GLTP = CheckTextureParams(TP);
            var GLIF = CheckInternalFormat(IF);
            return new GLTexture(GLTT,GLTP,GLIF); //tODO Branch here based on GRAPHICS_IMPLEMENTATION_ENUM
        }

        public static GLShader GetShaderImplementation(string vertcode, string fragcode, string geomcode="")
        {            
            return new GLShader(vertcode,fragcode,geomcode);
        }

        
        public static PixelInternalFormat CheckInternalFormat(InternalFormat TF)
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

        public static GLTextureParams CheckTextureParams(TextureParams RobustTexParams)
        {
            GLTextureParams GLTexParams = new GLTextureParams();
            
            switch (RobustTexParams.MinFilter)
            {
                case TextureMinFilterParams.LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMinFilterParams.NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMinFilterParams.LINEAR_MIPMAP_LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.LinearMipmapLinear;
                    break;
                case TextureMinFilterParams.LINEAR_MIPMAP_NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.LinearMipmapNearest;
                    break;
                case TextureMinFilterParams.NEAREST_MIPMAP_LINEAR:             
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapLinear;
                    break;
                case TextureMinFilterParams.NEAREST_MIPMAP_NEAREST:             
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapNearest;
                    break;
                default :
                    GLTexParams.MinFilter=TextureMinFilter.NearestMipmapLinear;
                    break;
            }

            switch (RobustTexParams.MagFilter)
            {
                case TextureMagFilterParams.LINEAR :
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
                case TextureMagFilterParams.NEAREST :
                    GLTexParams.MinFilter=TextureMinFilter.Nearest;
                    break;
                default :
                    GLTexParams.MinFilter=TextureMinFilter.Linear;
                    break;
            }

            switch(RobustTexParams.WrapModeS)
            {
                case TextureWrapParams.REPEAT:
                    GLTexParams.WrapModeS=TextureWrapMode.Repeat;
                    break;
                case TextureWrapParams.MIRRORED_REPEAT:
                    GLTexParams.WrapModeS=TextureWrapMode.MirroredRepeat;
                    break;
                case TextureWrapParams.CLAMP_TO_EDGE:
                    GLTexParams.WrapModeS=TextureWrapMode.ClampToEdge;
                    break;
                case TextureWrapParams.CLAMP_TO_BOARDER:
                    GLTexParams.WrapModeS=TextureWrapMode.ClampToBorder;
                    break;
                default :
                      GLTexParams.WrapModeS=TextureWrapMode.Repeat;
                    break;
            }

            switch(RobustTexParams.WrapModeT)
            {
                case TextureWrapParams.REPEAT:
                    GLTexParams.WrapModeT=TextureWrapMode.Repeat;
                    return GLTexParams;;
                case TextureWrapParams.MIRRORED_REPEAT:
                    GLTexParams.WrapModeT=TextureWrapMode.MirroredRepeat;
                    return GLTexParams;;
                case TextureWrapParams.CLAMP_TO_EDGE:
                    GLTexParams.WrapModeT=TextureWrapMode.ClampToEdge;
                    return GLTexParams;;
                case TextureWrapParams.CLAMP_TO_BOARDER:
                    GLTexParams.WrapModeT=TextureWrapMode.ClampToBorder;
                    return GLTexParams;;
                default :
                    GLTexParams.WrapModeT=TextureWrapMode.Repeat;
                    return GLTexParams;
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

      

   }   


    public enum UsageHint
    {
        Read =1,
        Write = 2,
        Copy = 4,
        Dynamic = 8,
        Static = 16,
        Stream = 32,
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

    public enum DrawTarget
    {
        Screen,
        Framebuffer,
        Offscreen
    }


    public struct GL_DETECTED_LIMITS
    {
        public static readonly int MAX_GL_TEXTURE_IMAGE_UNITS = GL.GetInteger(GetPName.MaxTextureImageUnits);
    }
}
