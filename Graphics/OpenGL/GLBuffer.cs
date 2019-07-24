using System;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics.Interfaces;



namespace RobustEngine.Graphics.OpenGL
{
    public enum UsageHint
    {
        Read =1,
        Write = 2,
        Copy = 4,
        Dynamic = 8,
        Static = 16,
        Stream = 32,
    }

    public class GLBuffer : IGLBuffer
    {        

        private int id;
        private UsageHint RobustBufferUsageHint;

        protected BufferTarget GLBufferTarget;
        protected BufferUsageHint GLBufferUsageHint;      

        public int ID 
        {
            get { return id; }
        }  

        public UsageHint BufferHint
        {
            get { return RobustBufferUsageHint; }
            set { GLBufferUsageHint = GLHelper.CheckUsageHint(value); RobustBufferUsageHint = value; }
        }            

        public GLBuffer(BufferTarget BF)
        {
            id = GL.GenBuffer();
            GLBufferTarget = BF;
        }
       
        public void Bind()
        {
            GL.BindBuffer(GLBufferTarget, id);
        }

        public void Unbind()
        {
            GL.BindBuffer(GLBufferTarget, 0);
        }
    }
    
}