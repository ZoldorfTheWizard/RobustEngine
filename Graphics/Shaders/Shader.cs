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

        private Dictionary<string,int> UniformLocations;

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
           
            int VertexCompiled;
            int FragmentCompiled;
            int ProgramComplied;

            GL.CompileShader(VSID);
            GL.GetShader(VSID, ShaderParameter.CompileStatus, out VertexCompiled);
            
            if(VertexCompiled != 1)
            {
               RobustConsole.Write(LogLevel.Critical , this, GL.GetShaderInfoLog(VSID));
            }

            GL.CompileShader(FSID);
            GL.GetShader(FSID, ShaderParameter.CompileStatus, out FragmentCompiled);

            if (FragmentCompiled != 1)
            {
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(FSID));
            }

            PID = GL.CreateProgram();
            GL.BindAttribLocation(PID, 0, "position");
            GL.BindAttribLocation(PID, 1, "texcoord");
            GL.BindAttribLocation(PID, 2, "normal");
            GL.BindAttribLocation(PID, 3, "color");

            GL.AttachShader(PID, VSID);
            GL.AttachShader(PID, FSID);
            GL.LinkProgram(PID);

            GL.GetProgram(PID, GetProgramParameterName.LinkStatus, out ProgramComplied);

            if (FragmentCompiled != 1)
            {
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(FSID));
            }
            
        }

        private int getUniformLoc(string VarName)
        {
             if (!UniformLocations.ContainsKey(VarName))
            {
                UniformLocations.Add(VarName, GL.GetUniformLocation(PID, VarName));
                if (UniformLocations[VarName] == -1)
                {
                    RobustConsole.Write(LogLevel.Warning, this, "| Shader PID:" + PID +" | Could not find Uniform Variable named "+ VarName +"!. Uniform data will be discarded!");
                }
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
            GL.UniformMatrix2(getUniformLoc(VarName),false, ref mat2);
        }

        public void setUniform(string VarName, Matrix3 mat3)
        {
            GL.UniformMatrix3(getUniformLoc(VarName), false, ref mat3);
        }

        public void setUniform(string VarName, Matrix4 mat4)
        {
            GL.UniformMatrix4(getUniformLoc(VarName), false, ref mat4);
        }

        public void setUniform(string VarName, Texture2D mat4)
        {


        }




    }
}
