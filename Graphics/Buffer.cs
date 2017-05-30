
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{
    public class Buffer
    {
        private int EBOID;     //Index Buffer Obj ID
        private int VBOID;     //Vertex Buffer Obj ID
        private int ColorBOID; //Color Buffer Obj ID

        private Vertex[] Verticies;
        private Vertex[] TexCoord;
        private float[] Indexes;

        public Buffer()
        {
            Verticies = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.UnitY,  
                Vertex.One             
            };

            Create(); 
        }
        
        public Buffer(Vertex[] Verts)
        {
            Verticies = Verts;    
            Create();

        }

        /// <summary>
        /// Creates a VertexBufferObject for Vertex Data and Indicies Data. 
        /// </summary>
        private void Create()
        {
            TexCoord = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.UnitY,
                Vertex.One,
            };

            Indexes = new float[]
            {
                0, 1, 2,
                2, 3, 1 
            };

            EBOID = GL.GenBuffer();
            VBOID = GL.GenBuffer();
            ColorBOID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, EBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Verticies.Length * sizeof(float)), Verticies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); 

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indexes.Length * sizeof(float)), Indexes, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }


        public void BindVertex()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, EBOID);
        }

        public void BindIndicies()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOID);
        }
            

    


        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }


    }
}
