using System.Net.Sockets;
using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.OpenGL
{
    /// TODO CATCH UN INITIALIZED UPDATES
    public class GLVertexBuffer : GLBuffer , IVertexBuffer
    {  
        public GLVertexBuffer(UsageHint UH) : base(BufferTarget.ArrayBuffer)
        {
            BufferHint = UH;
        }
    

        public void Init() 
        {               
            Bind();
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

            Unbind();
        }  

        public void Update(Vertex[] VertexData)
        {
            Bind();
            GL.BufferData(GLBufferTarget, VertexData.Length * Vertex.Stride, VertexData, GLBufferUsageHint);
            Unbind();
        }

        public void Update(Vertex[] VertexData, UsageHint UH)
        {
            BufferHint = UH;
            Bind();
            GL.BufferData(GLBufferTarget, VertexData.Length * Vertex.Stride, VertexData, GLBufferUsageHint);
            Unbind();
        }

        public void Debug()
        {

        }
    }

}