using System;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics.Interfaces;


namespace RobustEngine.Graphics.OpenGL
{
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