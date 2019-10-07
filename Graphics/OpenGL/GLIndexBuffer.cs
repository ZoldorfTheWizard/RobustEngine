using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.OpenGL
{
    public sealed class GLIndexBuffer : GLBuffer, IIndexBuffer 
    {   

        public GLIndexBuffer(BufferUsageHint UH) : base (BufferTarget.ElementArrayBuffer)
        {   
            GLBufferUsageHint = UH;
        }

        ///<inherit-doc />
		public void Create()
        {
            GL.BufferData(GLBufferTarget, 0, IntPtr.Zero, GLBufferUsageHint);
        }

        ///<inherit-doc />
		public void Update(int[] Indicies)
        {
            GL.BufferData(GLBufferTarget, Indicies.Length * sizeof(float), Indicies, GLBufferUsageHint);          
        }

        ///<inherit-doc />
		public void Update(int[] Indicies, UsageHint UH)
        {
            SetUsageHint(UH);
            GL.BufferData(GLBufferTarget,Indicies.Length * sizeof(float), Indicies, GLBufferUsageHint);
        }
    }

}