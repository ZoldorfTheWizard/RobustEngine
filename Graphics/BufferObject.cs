
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{
    public class BufferObject
    {
        private int VertexArrayID;    // Vertex Array ID. 

        private int VertexBufferID;   // Vertex Buffer Obj ID
        private int IndexBufferID;    // Index Buffer Obj ID
        private int ColorBOID;        // Color Buffer Obj ID

        private Vertex[] Verticies;
        private Vertex[] TexCoord;
        private float[] Indicies;

        public BufferObject()
        {
            Verticies = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,
                Vertex.UnitY
            };

            Create(); 
        }
        
        public BufferObject(Vertex[] Verts)
        {
            Verticies = Verts;    
            Create();

        }

        /// <summary>
        /// Creates a Buffer for Vertex Data and Indicies Data. 
        /// </summary>
        private void Create()
        {

            // 0,1 1,1  3 2
            // 0,0 0,1  0 1
            TexCoord = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,
                Vertex.UnitY,
            };

            //float[] vertices =
            //{
            //    -0.5f, -0.5f, 0.0f,
            //     0.5f, -0.5f, 0.0f,
            //     0.0f,  0.5f, 0.0f
            //};

            // 0 1
            // 2 3  
            Indicies = new float[]
            {
                0, 1, 2,
                2, 3, 1 
            };

            
            VertexArrayID = GL.GenVertexArray();
            GL.BindVertexArray(VertexArrayID); // Buffers now write to this VAO.
            
            //Generate Buffers
            VertexBufferID = GL.GenBuffer();
            IndexBufferID = GL.GenBuffer();
            ColorBOID = GL.GenBuffer();

            //Bind Buffer to Gen Buffer.
            //Describe Buffer.
            //Bind 0 to go back to default buffer
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData<Vertex>(BufferTarget.ArrayBuffer, (IntPtr)(Verticies.Length * Vertex.Stride), Verticies, BufferUsageHint.DynamicDraw);
            GL.VertexAttribPointer(0, 4, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            GL.EnableVertexAttribArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

          //  GL.BindVertexArray(0);

            //Indicies
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);

        }


        public void BindVertexArray()
        {
            GL.BindVertexArray(VertexArrayID);
        }

        public void BindVertexBuffer()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
        }

        public void BindIndexBuffer()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
        }

        public void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
            GL.BindVertexArray(0);
        }


    }
}
