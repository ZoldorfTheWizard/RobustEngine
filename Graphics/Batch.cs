using RobustEngine.Graphics.OpenGL;
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{

    public class Batch 
    {
        public string Key;
        public float Width;
        public float Height;

        public Matrix4  BatchModelMatrix;
        public Vertex[] BatchData;
        public int BatchSize;
        public int[]  BatchIndicies;

        protected GLVertexBuffer VBO;
        protected GLIndexBuffer IBO;        
    

        public Batch(string Name, float width, float height)
        {
            Key=Name;
            Width=width;
            Height=height;

            VBO = new GLVertexBuffer(UsageHint.Dynamic);
            IBO = new GLIndexBuffer(UsageHint.Dynamic); 

            BatchModelMatrix = Matrix4.Identity;

        }

        public void Setup(int MAX_BATCH_SIZE = 1000000)
        {
            VBO.Init();
            IBO.Init();

            BatchIndicies = new int[MAX_BATCH_SIZE*6];
            int offset = 0;

            for (int i = 0; i <= MAX_BATCH_SIZE; i++)
            {
                BatchIndicies[i]   = offset + 0;
                BatchIndicies[i+1] = offset + 1;
                BatchIndicies[i+2] = offset + 2;
                BatchIndicies[i+3] = offset + 2;
                BatchIndicies[i+4] = offset + 3;
                BatchIndicies[i+5] = offset + 0;

                offset+=4;
                i+=5;

            }
            
            IBO.Update(BatchIndicies);
        }

        public void Draw()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);             
            //RobustEngine.CurrentShader.setUniform("ModelMatrix", BatchModelMatrix);
            //RobustEngine.CurrentShader.setUniform("UsingTexture", GL.GetInteger(GetPName.TextureBinding2D));
            
        
        
            // IBO.Bind();           
            // GL.DrawElements(PrimitiveType.Triangles, 6 * BatchSize, DrawElementsType.UnsignedInt, 0);                                      
            // IBO.Unbind();
            
            
            if (BatchSize >= 166666)
            {
                int BatchesLeft = (int) Math.Ceiling((double)BatchSize/166666);
                int BatchesDone = 0;
                IBO.Bind();
                while(BatchesDone < BatchesLeft)
                {
                    GL.DrawArrays(PrimitiveType.Triangles,166666 + (166666 * BatchesDone), BatchSize * 6);
                    BatchesDone++;
                }                              
                IBO.Unbind();

            }
            else
            {
                IBO.Bind();
                GL.DrawElements(PrimitiveType.Triangles, 6 * BatchSize, DrawElementsType.UnsignedInt, 0);
                IBO.Unbind();
            }
          
        }

    }
}