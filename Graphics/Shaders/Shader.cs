using System.IO;
using RobustEngine.Graphics.Interfaces;
using OpenTK;


namespace RobustEngine.Graphics.Shaders
{
    public class Shader
    {       

        private IShader shader; 
        private string VertCode;
        private string FragCode;     
         
        public Shader()
        {
            
        }

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Path to Vertex Shader</param>
        /// <param name="FragFilePath">Path to Fragment Shader</param>
        public void LoadFromFile(string VertexFilePath, string FragFilePath)
        {
            VertCode = File.ReadAllLines(VertexFilePath);
            FragCode = File.ReadAllLines(FragFilePath);

            shader = GraphicsAPI.NewShader(VertCode,FragCode);
        }

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Path to Vertex Shader</param>
        /// <param name="FragFilePath">Path to Fragment Shader</param>
        public void LoadFromString(string VertCode, string FragCode)
        {
            shader = GraphicsAPI.NewShader(VertCode,FragCode);
        }
        
        public void Bind()
        {
            shader.Bind();
        }

        public void SetUniform(string UniformName, float f)
        {
            shader.SetUniform(UniformName,f);
        }
        
        public void SetUniform(string UniformName, int i)
		{
            shader.SetUniform(UniformName,i);
        }
        
        public void SetUniform(string UniformName, Vector2 vec2)
		{
            shader.SetUniform(UniformName,vec2);
        }
        
        public void SetUniform(string UniformName, Vector3 vec3)
		{
            shader.SetUniform(UniformName,vec3);
        }
        
        public void SetUniform(string UniformName, Vector4 vec4)
		{
            shader.SetUniform(UniformName,vec4);
        }
        
        public void SetUniform(string UniformName, Matrix2 mat2)
		{
            shader.SetUniform(UniformName,mat2);
        }
        
        public void SetUniform(string UniformName, Matrix3 mat3)
		{
            shader.SetUniform(UniformName,mat3);
        }
        
        public void SetUniform(string UniformName, Matrix4 mat4)
		{
            shader.SetUniform(UniformName,mat4);
        }
        
        public void Unbind()
        {
            shader.Unind();  
        }

        
       
    }
}
