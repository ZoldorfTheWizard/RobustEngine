using OpenTK;

namespace RobustEngine.Graphics.Interfaces
{
    public interface IShader
    {
        
        void Enable();
        
        void SetUniform(string UniformName, float f);
        void SetUniform(string UniformName, int i);
        void SetUniform(string UniformName, Vector2 vec2);
        void SetUniform(string UniformName, Vector3 vec3);
        void SetUniform(string UniformName, Vector4 vec4);
        void SetUniform(string UniformName, Matrix2 i);
        void SetUniform(string UniformName, Matrix3 i);
        void SetUniform(string UniformName, Matrix4 i);

        void Disable();


    }


}