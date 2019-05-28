using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLHelper
    {

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

}
