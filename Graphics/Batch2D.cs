
using System;
using RobustEngine.Graphics.Interfaces;
using RobustEngine.Graphics.OpenGL;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{

    public class Batch2D : Transformable2D, IRenderable
    {
        public string Key;
        public float Width;
        public float Height;
        public int BatchSize;  

        protected GLVertexBuffer VBO;
        protected GLIndexBuffer IBO;             

        private Color fillcolor;
        private Debug debugmode;
        private Vertex[] vertexdata;
        private int[] indicies;


        public Color FillColor 
        {
            get { return fillcolor; }
            set { SetFillColor(value);}
        }

        public Debug DebugMode 
        {
            get { return debugmode; }
            set { debugmode = value; }
        }

        public Vertex[] VertexData 
        {
            get { return vertexdata; }
            protected set { vertexdata = value; }
        }

        public int[] Indicies
        {
            get { return indicies; }
            protected set { indicies = value; }
        }

        public void SetFillColor(Color newColor)
        {
            fillcolor = newColor;
            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].SetColor(newColor);
            }
        }

        public Batch2D(string Name, float width, float height)
        {
            Key=Name;
            Width=width;
            Height=height;
            BatchSize=0;

           // VBO = new GLVertexBuffer(UsageHint.Dynamic);
           // IBO = new GLIndexBuffer(UsageHint.Dynamic); 
        }

        public void Setup(int MAX_BATCH_SIZE = 1000000)
        {
            Indicies = new int[MAX_BATCH_SIZE*6];
            int offset = 0;

            for (int i = 0; i <= MAX_BATCH_SIZE; i++)
            {
                Indicies[i]   = offset + 0;
                Indicies[i+1] = offset + 1;
                Indicies[i+2] = offset + 2;
                Indicies[i+3] = offset + 2;
                Indicies[i+4] = offset + 3;
                Indicies[i+5] = offset + 0;

                offset+=4;
                i+=5;
            }
            
            VBO.Bind();
            VBO.Create();
            VBO.Unbind(); 

            IBO.Bind();
            IBO.Create();
            IBO.Update(Indicies);
            IBO.Unbind();
        }

        public virtual void Process()
        {

        }

        public void Draw()
        {                          
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
        
        public void DrawInstanced()
        {                          
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