using System;
using OpenTK;

namespace RobustEngine.Graphics.Shapes2D
{
    public sealed class Line2D : Shape2D
    {
        public Vector2 StartPoint;
        public Vector2 EndPoint;
        public Vector2 Center;        

        public Line2D(int startX, int startY, int endX, int endY)
        {
            Create(startX, startY, endX, endY, 1, 1, Color.DarkMagenta);
        }

        public Line2D(double startX, double startY, double endX, double endY)
        {
            Create((float)startX, (float)startY, (float)endX, (float)endY, 1.0f,1.0f, Color.DarkMagenta);
        }

        public Line2D(float startX, float startY, float endX, float endY)
        {
            Create(startX, startY, endX, endY, 1.0f, 1.0f, Color.DarkMagenta);
        }

        private void Create(float x1, float y1, float x2, float y2, float pointsize, float linewidth, Color fillcolor)
        {
            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.One
            };
            
            Indicies = new int[]
            {
                0, 1
            };

            ModelMatrix = Matrix4.Identity;
            Scale = Vector2.One;
            Position = new Vector2(x1,y1);
            Size = new Vector2(1,1);

            PointSize = pointsize;
            LineWidth = linewidth;

            FillColor = fillcolor;     
           
            SetHelperPoints();               
        }

        private void SetHelperPoints()
        {   
            StartPoint = VertexData[0].ToVector2();
            EndPoint = VertexData[1].ToVector2();
            Center = new Vector2(EndPoint.X / 2, EndPoint.Y / 2); 
        }

        public override void SetSize(Vector2 newSize)
        {
            VertexData[1].X *= newSize.X;
            VertexData[1].Y *= newSize.Y;
        }        
      
    }
}
