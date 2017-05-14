
using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{
    public class BufferObject
    {
        private int EBOID; //Index Buffer Object
        private int VBOID; //Vertex Buffer Object
        private int ColorBOID;

        private float[,] Verticies;
        private float[] Indicies;

        public BufferObject(float X, float Y)
        {
            Verticies = new float[,]
                {
                   { 0.0f, 0.0f },
                   { 1.0f, 0.0f },
                   { 1.0f, 1.0f },
                   { 0.0f, 1.0f },
                };

            Indicies = new float[]
                {
                    0,
                    1,
                    2,
                    3
                };


            Create(); 
                
        }

        /// <summary>
        /// Creates a VertexBufferObject for Vertex Data and Indicies Data. 
        /// </summary>
        private void Create()
        {
            EBOID = GL.GenBuffer();
            VBOID = GL.GenBuffer();
            ColorBOID = GL.GenBuffer();

            GL.BindBuffer(BufferTarget.ArrayBuffer, EBOID);
            GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Verticies.Length * sizeof(float)), Verticies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0); 

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, EBOID);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        }

   //     public void Transform(Vector2 )

    }
}
