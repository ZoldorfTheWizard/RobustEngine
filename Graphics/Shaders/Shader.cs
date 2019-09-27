using System.IO;
using RobustEngine.Graphics.Interfaces;
using OpenTK;


namespace RobustEngine.Graphics.Shaders
{
    public class Shader
    {       

        private IShader shader;      

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Char array containing Path to Vertex Shader</param>
        /// <param name="FragFilePath">Char array containing Fragment Shader</param>
        public Shader(char[] VertexCharArray, char[] FragCharArray)
        {
            ///Shader = GraphicsAPI.Get
        }

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Path to Vertex Shader</param>
        /// <param name="FragFilePath">Path to Fragment Shader</param>
        public Shader(string VertexFilePath, string FragFilePath)
        {
            //shader.Compile(VertexFilePath, FragFilePath);
        }

       
    }
}
