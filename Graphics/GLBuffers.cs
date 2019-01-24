using OpenTK.Graphics.OpenGL;
using System;

namespace RobustEngine.Graphics
{
    public class GLBuffers
    {  
        public int VertexArrayID;
        public int VertexBufferID;
        public int IndexBufferID;
        public int[] Indicies;
        public GLBuffers()
        {

        }
    
        public void Init(Vertex[] VertexData, int[] Indicies)
        {
            VertexArrayID = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayID);

            VertexBufferID = GL.GenBuffer();
            IndexBufferID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length, IntPtr.Zero, BufferUsageHint.DynamicDraw);

            // Vertex Data
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

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //INDEX DATA
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);
        }

        public void BindVertexArray()
        {
            GL.BindVertexArray(VertexArrayID);
        }

        public void UnbindVertexArray()
        {
            GL.BindVertexArray(0);
        }

        public void BindElementBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
        }

        public void UnbindElementBuffer()
        {
             GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void BindIndexBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
        }

        public void UnbindIndexBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

        public void UpdateElementBuffer(Vertex[] VertexData)
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public void Debug()
        {

        }
    }

}