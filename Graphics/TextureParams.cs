using System;
using RobustEngine.Graphics.OpenGL;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{
    public enum TextureMinFilterParams
    {
        NEAREST,
        LINEAR,
        NEAREST_MIPMAP_NEAREST,
        LINEAR_MIPMAP_NEAREST,
        NEAREST_MIPMAP_LINEAR,
        LINEAR_MIPMAP_LINEAR,
        REPEAT,
    }

    public enum TextureMagFilterParams
    {
        NEAREST,
        LINEAR,
    }

    public enum TextureWrapParams
    {
        REPEAT,
        MIRRORED_REPEAT,
        CLAMP_TO_EDGE,
        CLAMP_TO_BOARDER
    }

    public struct TextureParams
    {
        public TextureMinFilterParams MinFilter; 
        public TextureMagFilterParams MagFilter;
        public TextureWrapParams WrapModeS;
        public TextureWrapParams WrapModeT;


        public static TextureParams DEFAULT_ROBUST_TEXTURE_PARAMS = new TextureParams
        {
            MinFilter = TextureMinFilterParams.LINEAR,
            MagFilter = TextureMagFilterParams.NEAREST,
            WrapModeS = TextureWrapParams.REPEAT,
            WrapModeT = TextureWrapParams.REPEAT
        };

       
    }
    

}