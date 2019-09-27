using System;
using System.Collections.Generic;
using RobustEngine.System;
using RobustEngine.Graphics.Interfaces;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.OpenGL
{

    public class GLShader : IShader
    {

        private int VSID;
        private int FSID;
        private int GSID;
        private int PID;

        private string VertCode;
        private string FragCode;
        private string GeomCode;
        private bool DEBUG;

        private Dictionary<string, int> UniformLocations;


        public GLShader(string VertCode, string FragCode, string GeomCode = "")
        {
            VSID = 0;
            FSID = 0;
            GSID = 0;
            Compile(VertCode,FragCode,GeomCode);
        }

        /// <summary>
        /// Compiles the vertex/fragment/geometry shader. Reports any compile-time issues.
        /// </summary>
        private void Compile(string VertCode, string FragCode, string GeomCode)
        {
            CompileVertex(VertCode);           
            CompileFragment(FragCode);
            CompileGeometry(GeomCode);
            CompileProgram();      
            
            UniformLocations = new Dictionary<string, int>();
        }

        #region  Compile Private Methods

        private void CompileVertex(string vertshader)
        {
            VSID = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(VSID, VertCode);
            GL.CompileShader(VSID);
            GL.GetShader(VSID, ShaderParameter.CompileStatus, out int VertexCompiled);
            if (VertexCompiled != 1)
            { 
                //INSERT DEFAULT TYPE SHADER HERE
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(VSID));
            }
        }

        private void CompileFragment(string fragshader)
        {
            FSID = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(FSID, FragCode);
            GL.CompileShader(FSID);
            GL.GetShader(FSID, ShaderParameter.CompileStatus, out int FragmentCompiled);
            if (FragmentCompiled != 1)
            {
                 //INSERT DEFAULT TYPE SHADER HERE
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(FSID));
            }
        }

        private void CompileGeometry(string GeomCode)
        {
            if (GeomCode == "") 
                return;

            GSID = GL.CreateShader(ShaderType.GeometryShader);
            GL.ShaderSource(GSID, GeomCode);
            GL.CompileShader(GSID);
            GL.GetShader(GSID, ShaderParameter.CompileStatus, out int FragmentCompiled);
            if (FragmentCompiled != 1)
            {   
                //INSERT DEFAULT TYPE SHADER HERE
                RobustConsole.Write(LogLevel.Critical, this, GL.GetShaderInfoLog(GSID));
            }
        }

        private void CompileProgram()
        {
            PID = GL.CreateProgram();

            GL.AttachShader(PID, VSID);
            GL.AttachShader(PID, FSID);
            if (GSID != 0) 
            {
                GL.AttachShader(PID,GSID);
            }

            GL.LinkProgram(PID);
            GL.GetProgram(PID, GetProgramParameterName.LinkStatus, out int ProgramComplied);

            if (ProgramComplied != 1)
            {
                RobustConsole.Write(LogLevel.Warning, this, GL.GetShaderInfoLog(PID));
            } 

            GL.DetachShader(PID, VSID);
            GL.DeleteShader(VSID);
            
            GL.DetachShader(PID, FSID);
            GL.DeleteShader(FSID);
   
            if (GSID != 0) 
            {
                GL.AttachShader(PID,GSID);
                GL.DeleteShader(GSID);
            }

        }
        #endregion
        
        private int getUniformLoc(string UniformName)
        {
            if (!UniformLocations.ContainsKey(UniformName))
            {
                UniformLocations.Add(UniformName, GL.GetUniformLocation(PID, UniformName));
            }//TODO catch not found uniform var names

            if (UniformLocations[UniformName] == -1 && DEBUG)
            {
                RobustConsole.Write(LogLevel.Warning, this, "| Shader PID:" + PID + " | Could not find Uniform Variable named " + UniformName + "!. Uniform data will be discarded!");
            } //todo move to logging service

            return UniformLocations[UniformName];
        }

        #region SetUniform Methods
    
        ///<inherit-doc />
		public void SetUniform(string UniformName, int i)
        {
            GL.Uniform1(getUniformLoc(UniformName), i);
        }
        
        ///<inherit-doc />
		public void SetUniform(string UniformName, float f)
        {
            GL.Uniform1(getUniformLoc(UniformName), f);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Vector2 vec2)
        {
            GL.Uniform2(getUniformLoc(UniformName), vec2);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Vector3 vec3)
        {
            GL.Uniform3(getUniformLoc(UniformName), vec3);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Vector4 vec4)
        {
            GL.Uniform4(getUniformLoc(UniformName), vec4);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Matrix2 mat2)
        {
            GL.UniformMatrix2(getUniformLoc(UniformName), false, ref mat2);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Matrix3 mat3)
        {
            GL.UniformMatrix3(getUniformLoc(UniformName), false, ref mat3);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Matrix4 mat4)
        {
            GL.UniformMatrix4(getUniformLoc(UniformName), false, ref mat4);
        }

        ///<inherit-doc />
		public void SetUniform(string UniformName, Texture2D texture)
        {
 

        }
        #endregion  

        ///yes
		public void ToggleDebug()
        {
            DEBUG = !DEBUG;
        }

   
        ///<inherit-doc />
		public void Enable()
        {
            GL.UseProgram(PID);
        }

        ///<inherit-doc />
		public void Disable()
        {
            GL.UseProgram(0);
        }


    }

}