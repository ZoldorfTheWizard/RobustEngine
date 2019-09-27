using System;
using OpenTK;
using RobustEngine.Graphics.OpenGL;
using RobustEngine.Graphics.Interfaces;

namespace RobustEngine.Graphics.Render
{
  

    public sealed class Framebuffer : IRenderable
    {
        private int Width;
        private int Height;

        public Vertex[] VertexData{get;set;}
        public int[] Indicies{get;set;}
        public Matrix4 ModelMatrix {get;set;}
        public Debug DebugMode {get;set;}
        public float PointSize{get;set;}
        public float LineWidth{get;set;}

        public IFrameBuffer Implementation;
        
        public Framebuffer(TextureTarget TT = TextureTarget.Texture2D)
        {
          
        }


        public void Init(int width,int height)
        {
            ModelMatrix = Matrix4.Identity;

            VertexData = new Vertex[]
            {
                Vertex.One *-1,
                Vertex.OneNegOne,
                Vertex.One,
                Vertex.OneNegOne*-1            
            };

            Indicies = new int[]
            {
                0,1,2,
                2,3,0
            };
         
            VertexData[0].Tx = 0;
            VertexData[0].Ty = 0;

            VertexData[1].Tx = 1;
            VertexData[1].Ty = 0;
            
            VertexData[2].Tx = 1;
            VertexData[2].Ty = 1;
            
            VertexData[3].Tx = 0;
            VertexData[3].Ty = 1; 
           
            PointSize=1f;
            LineWidth=1f;

            //GLCreate(width,height);          

            //Init dx12/gl buff  
        }

    }
}