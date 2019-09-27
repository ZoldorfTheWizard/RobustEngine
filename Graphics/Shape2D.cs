using RobustEngine.Graphics.Interfaces;
using OpenTK;

namespace RobustEngine.Graphics
{
    public abstract class Shape2D : Transformable2D, IRenderable
    { 
        private Vertex[] vertexdata;
        private int[] indicies; 
        private Color fillcolor;
        private Debug debugmode;

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

        public float PointSize {get;set;}
        public float LineWidth {get;set;}  
   
        /// <summary>
        /// Sets the Color of all verticies or specified vertex in VertexData
        /// </summary>
        /// <param name="newColor"></param>
        /// <param name="vertexAt">Specific vertex of the shape to recolor. Leave blank to color all vertex 
        /// in VertexData</param>
        public void SetFillColor(Color newColor, int vertexAt = -1)
        {
            if (vertexAt == -1)
            {
                fillcolor = newColor;
                for (int i = 0; i < VertexData.Length; i++)
                {
                    vertexdata[i].SetColor(newColor);  
                }
            }
            else
            { 
                vertexdata[vertexAt].SetColor(newColor);   
            }
            fillcolor = newColor;
        }
     
    }
}