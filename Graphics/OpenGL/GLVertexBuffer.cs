using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.OpenGL
{
    
    public sealed class GLVertexBuffer : GLBuffer , IVertexBuffer
    {  
        public GLVertexBuffer(BufferUsageHint BUH) : base(BufferTarget.ArrayBuffer)
        {
            GLBufferUsageHint = BUH;
        }
    
        public void Create() 
        {               
            GL.BufferData(GLBufferTarget, 0, IntPtr.Zero, GLBufferUsageHint);
            //Tell Opengl how to read Vertex 
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            GL.EnableVertexAttribArray(0); // Layout 0 Position
            // Color Data
            GL.VertexAttribPointer(1, 4, VertexAttribPointerType.Float, false, Vertex.Stride, 12);
            GL.EnableVertexAttribArray(1); // Layout 1 Color Data
            // Normal Data
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 28);
            GL.EnableVertexAttribArray(2); // Layout 2 Normal Data
            // TextureUVCoords
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 40);
            GL.EnableVertexAttribArray(3); // Layout 3 Texture Data
        }  

        public void Update(Vertex[] VertexData)
        {
            GL.BufferData(GLBufferTarget, VertexData.Length * Vertex.Stride, VertexData, GLBufferUsageHint);
        }

        public void Update(Vertex[] VertexData, UsageHint UH)
        {
            SetUsageHint(UH);
            GL.BufferData(GLBufferTarget, VertexData.Length * Vertex.Stride, VertexData, GLBufferUsageHint);
        }       
    }

}