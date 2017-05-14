using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shape
{
    public class Line
    {
        private int VertexBufferID;
        

        public Line(int a, int b)
        {
            VertexBufferID = GL.GenBuffer();
       //     GL.BindBuffer(ArrayBuffer.BufferTarget, VertexBufferID);


            
            


        }
            
       
    }
}
