using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.OpenGL
{

    public class GLLimits
    {

        public static readonly int MAX_GL_TEXTURE_IMAGE_UNITS = GL.GetInteger(GetPName.MaxTextureImageUnits);


    }



}