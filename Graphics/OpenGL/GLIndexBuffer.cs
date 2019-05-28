using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLIndexBuffer : GLBuffer, IGLIndexBuffer 
    {   

        public GLIndexBuffer(UsageHint UH) : base (BufferTarget.ElementArrayBuffer)
        {   
            BufferHint = UH;
        }

        public void Init(int[] Indicies)
        {
            Bind();
            GL.BufferData(GLBufferTarget, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, GLBufferUsageHint);
            Unbind();
        }

        public void Update(int[] Indicies)
        {
            Bind();
            GL.BufferData(GLBufferTarget, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, GLBufferUsageHint);
            Unbind();
        }

        public void Update(int[] Indicies, UsageHint UH)
        {
            BufferHint = UH;
            Bind();
            GL.BufferData(GLBufferTarget, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, GLBufferUsageHint);
            Unbind();
        }
    }

}