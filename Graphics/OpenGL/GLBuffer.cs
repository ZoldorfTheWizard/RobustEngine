using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;


namespace RobustEngine.Graphics.OpenGL
{   

    public abstract class GLBuffer 
    {  
        private int id;

        protected BufferTarget GLBufferTarget;
        protected BufferUsageHint GLBufferUsageHint;      

        public int ID 
        {
            get { return id; }
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
        
        public void SetUsageHint(UsageHint UH)
        {
            SetUsageHint(GraphicsAPI.CheckUsageHint(UH));
        }

        internal void SetUsageHint(BufferUsageHint BUH)
        {   
            GLBufferUsageHint = BUH;
        }

    }
    
}