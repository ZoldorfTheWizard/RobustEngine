using System.IO;
using OpenTK.Graphics.OpenGL;
using RobustEngine.System;
using OpenTK;
using System.Collections.Generic;

namespace RobustEngine.Graphics.Shaders
{
    public class Shader
    {
        private int VSID;
        private int FSID;
        private int PID;

        private string VIDDump;
        private string FIDDump;

        private bool DEBUG;

        private Dictionary<string, int> UniformLocations;

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Char array containing Path to Vertex Shader</param>
        /// <param name="FragFilePath">Char array containing Fragment Shader</param>
        public Shader(char[] VertexCharArray, char[] FragCharArray)
        {
            Compile(VertexCharArray.ToString(), FragCharArray.ToString());
        }

        /// <summary>
        /// Construct a new Shader.
        /// </summary>
        /// <param name="VertexFilePath">Path to Vertex Shader</param>
        /// <param name="FragFilePath">Path to Fragment Shader</param>
        public Shader(string VertexFilePath, string FragFilePath)
        {
            Compile(VertexFilePath, FragFilePath);
        }

        /// <summary>
        /// Compiles the vertex/fragment shader. Reports any compile-time issues.
        /// </summary>
        private void Compile(string VertexCode, string FragCode)
        {
            DEBUG = false;
            VSID = GL.CreateShader(ShaderType.VertexShader);
            FSID = GL.CreateShader(ShaderType.FragmentShader);

            VIDDump = File.ReadAllText(VertexCode);
            FIDDump = File.ReadAllText(FragCode);

            GL.ShaderSource(VSID, VIDDump);
            GL.ShaderSource(FSID, FIDDump);

            GL.CompileShader(VSID);
            GL.GetShader(VSID, ShaderParameter.CompileStatus, out int VertexCompiled);
            if (VertexCompiled != 1)
            {
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(VSID));
            }

            GL.CompileShader(FSID);
            GL.GetShader(FSID, ShaderParameter.CompileStatus, out int FragmentCompiled);
            if (FragmentCompiled != 1)
            {
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(FSID));
            }

            PID = GL.CreateProgram();

            GL.AttachShader(PID, VSID);
            GL.AttachShader(PID, FSID);
            GL.LinkProgram(PID);

            GL.GetProgram(PID, GetProgramParameterName.LinkStatus, out int ProgramComplied);

            if (ProgramComplied != 1)
            {
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(PID));
            }

            GL.DetachShader(PID, VSID);
            GL.DetachShader(PID, FSID);
            GL.DeleteShader(VSID);
            GL.DeleteShader(FSID);

            UniformLocations = new Dictionary<string, int>();
        }

        private int getUniformLoc(string VarName)
        {
            if (!UniformLocations.ContainsKey(VarName))
            {
                UniformLocations.Add(VarName, GL.GetUniformLocation(PID, VarName));
            }

            if (UniformLocations[VarName] == -1 && DEBUG)
            {
                RobustConsole.Write(LogLevel.Warning, this, "| Shader PID:" + PID + " | Could not find Uniform Variable named " + VarName + "!. Uniform data will be discarded!");
            }

            return UniformLocations[VarName];
        }

        public void ToggleDebug()
        {
            DEBUG = !DEBUG;
        }

        public void Enable()
        {
            GL.UseProgram(PID);
        }

        public void Disable()
        {
            GL.UseProgram(0);
        }

        #region Set Uniforms
        public void setUniform(string VarName, float f)
        {
            GL.Uniform1(getUniformLoc(VarName), f);
        }

        public void setUniform(string VarName, Vector2 vec2)
        {
            GL.Uniform2(getUniformLoc(VarName), vec2);
        }

        public void setUniform(string VarName, Vector3 vec3)
        {
            GL.Uniform3(getUniformLoc(VarName), vec3);
        }

        public void setUniform(string VarName, Vector4 vec4)
        {
            GL.Uniform4(getUniformLoc(VarName), vec4);
        }

        public void setUniform(string VarName, Matrix2 mat2)
        {
            GL.UniformMatrix2(getUniformLoc(VarName), false, ref mat2);
        }

        public void setUniform(string VarName, Matrix3 mat3)
        {
            GL.UniformMatrix3(getUniformLoc(VarName), false, ref mat3);
        }

        public void setUniform(string VarName, Matrix4 mat4)
        {
            GL.UniformMatrix4(getUniformLoc(VarName), false, ref mat4);
        }

        public void setUniform(string VarName, Texture2D texture)
        {


        }
        #endregion  
    }
}
