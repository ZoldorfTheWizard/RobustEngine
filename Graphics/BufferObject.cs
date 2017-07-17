
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace RobustEngine.Graphics
{
    public class BufferObject
    {
        private int VertexArrayID;    // Vertex Array ID. 

        private int VertexBufferID;   // Vertex Buffer Obj ID
        private int IndexBufferID;    // Index Buffer Obj ID
        private int ColorBOID;        // Color Buffer Obj ID

        private Vertex[] Verticies;
        private int[] Indicies;

        /// <summary>
        /// Creates a new buffer object with defaults.
        /// </summary>
        public BufferObject()
        {
            Verticies = new Vertex[]
            {
                Vertex.Zero  * .5f,
                Vertex.UnitX * .5f,
                Vertex.One   * .5f,
                Vertex.UnitY * .5f             
            };

            Create();
        }


        /// <summary>
        /// Creates a buffer object with specified verts.
        /// </summary>
        /// <param name="Verts"></param>
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

            Verticies[0].Tx = 0f;
            Verticies[0].Ty = 1f;

            Verticies[1].Tx = 1f;
            Verticies[1].Ty = 1f;

            Verticies[2].Tx = 1f;
            Verticies[2].Ty = 0f;

            Verticies[3].Tx = 0f;
            Verticies[3].Ty = 0f;
           
            Verticies[1].R = 1f;
            Verticies[2].G = 1f;
            Verticies[3].B = 1f;

            Indicies = new int[]
            {
                  0, 1, 2, // First Triangle
                  2, 3, 0  // Second Triangle
            };

            VertexArrayID = GL.GenVertexArray(); // Opengl Please give us a empty State
            GL.BindVertexArray(VertexArrayID);   // Opengl Please use and write everything to this new empty state now thx            

            //Generate Buffers
            VertexBufferID = GL.GenBuffer(); // Hey Opengl we want a empty VBO to use for data pls generate thx
            IndexBufferID = GL.GenBuffer();  // Hey Opengl we want a empty VBO to use for indexing pls generate thx
            ColorBOID = GL.GenBuffer();      // Hey Opengl we want a empty VBO to use for colors pls generate thx
            
            // VBO
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID); 
            GL.BufferData(BufferTarget.ArrayBuffer, Verticies.Length * Vertex.Stride, Verticies, BufferUsageHint.DynamicDraw);
                    
            // Vertex Data
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
            GL.EnableVertexAttribArray(0); // Layout 1 Position Data

            // Color Data
            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 12); 
            GL.EnableVertexAttribArray(1); // Layout 2 Color Data
            
            // Normal Data
            GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 24);
            GL.EnableVertexAttribArray(2); // Layout 2 Color

            // TextureUVCoords
            GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 36); 
            GL.EnableVertexAttribArray(3); // Layout 3 TexCoord

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            //INDEX DATA
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

            GL.BindVertexArray(0);

        }

        public void Update()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
            GL.BufferData(BufferTarget.ArrayBuffer, Verticies.Length * Vertex.Stride, Verticies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
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


        public void move(float mov)
        {
            for (int i = 0; i < 4; i++)
            { 
                //Verticies[i].Y = Verticies[i].Y + mov;
               // Verticies[i].Z = Verticies[i].Z + mov;
            }
        }

    }

    public struct test
    {
        public float x;
        public float y;
        public float z;
    }
}
