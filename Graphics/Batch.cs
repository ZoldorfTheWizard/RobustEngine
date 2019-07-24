using RobustEngine.Graphics.OpenGL;
using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics
{

    public class Batch : ITransformable2D
    {
        public string Key;
        public float Width;
        public float Height;
        public int BatchSize;  

        #region Accessors     

        public Matrix4 ModelMatrix
        { 
            get { return modelmatrix; }
            set { modelmatrix = value; }
        }  

        public Vector2 Origin 
        {
            get { return origin; }
            set { SetOrigin(value); origin=value; }
        }

        public Vector2 Position 
        {
            get { return position; }
            set { SetPosition(value); position=value; }
        }
        
        public Vector2 Scale 
        {
            get { return scale; }
            set { SetScale(value); scale=value; }
        }

        public Vector2 Size 
        {
            get { return size; }
            set { SetSize(value); size=value; }
        }     

        public Vector3 Rotation
        {
            get { return rotation; }
            set { SetRotation(value); rotation=value;}
        }
 
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

        #endregion

        protected GLVertexBuffer VBO;
        protected GLIndexBuffer IBO;             
        
        private Matrix4 modelmatrix;       
        private Vector2 origin;
        private Vector2 position;
        private Vector2 scale;
        private Vector2 size;
        private Vector3 rotation;

        private Color fillcolor;
        private Debug debugmode;
        private Vertex[] vertexdata;
        private int[] indicies;

        public Batch(string Name, float width, float height)
        {
            Key=Name;
            Width=width;
            Height=height;
            BatchSize=0;

            VBO = new GLVertexBuffer(UsageHint.Dynamic);
            IBO = new GLIndexBuffer(UsageHint.Dynamic); 

            ModelMatrix = Matrix4.Identity;

        }

        public void Setup(int MAX_BATCH_SIZE = 1000000)
        {
            VBO.Init();
            IBO.Init();

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
            
            IBO.Update(Indicies);
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

        #region ITransformable
        
        public void SetOrigin(Vector2 newOrigin)
        {
            modelmatrix *= Matrix4.CreateTranslation(-newOrigin.X, -newOrigin.Y, 0);
        }

        public void SetPosition(Vector2 newPosition)
        {
            modelmatrix *= Matrix4.CreateTranslation(newPosition.X, newPosition.Y, 0);
        }

        public void SetRotation(Vector3 newRotation)
        {
          
            rotation.X = MathHelper.DegreesToRadians(newRotation.X);
            rotation.Y = MathHelper.DegreesToRadians(newRotation.Y);
            rotation.Z = MathHelper.DegreesToRadians(newRotation.Z);
            
            modelmatrix *= Matrix4.CreateRotationX(rotation.X);
            modelmatrix *= Matrix4.CreateRotationY(rotation.Y);             
            modelmatrix *= Matrix4.CreateRotationZ(rotation.Z);             
        }

        public void SetScale(Vector2 newScale)
        {
            modelmatrix *= Matrix4.CreateScale(newScale.X, newScale.Y, 1);
        }   

        public virtual void SetSize(Vector2 newSize)
        {
         
        }
   
        public void SetFillColor(Color newColor)
        {
            fillcolor = newColor;
            for (int i = 0; i < VertexData.Length; i++)
            {
                VertexData[i].SetColor(newColor);
            }
        }

        public void PushMatrix(Matrix4 mat)
        {
            modelmatrix *= mat;
        }

        public void PopMatrix()
        {
            modelmatrix = Matrix4.Identity;
        }
     
        #endregion

    }
}