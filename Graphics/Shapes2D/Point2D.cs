using RobustEngine.Graphics.OpenGL;
using OpenTK;

namespace RobustEngine.Graphics.Shapes2D
{
    public class Point2D : Shape2D, IRenderable2D
    {
        public Point2D(float x, float y, float sizex = 1, float sizey = 1) 
        {
            Create(x,y,sizex, sizey,Color.Maroon);
        }

        public Point2D(int x, int y, int sizex = 1, int sizey =1) 
        {
            Create(x,y,sizex, sizey, Color.Maroon);
        }

        private void Create(float x, float y, float sizex, float sizey, Color fillcolor)
        {
            VertexData = new Vertex[]
            {   
                Vertex.One
            };  

            ModelMatrix = Matrix4.Identity;
            Scale = Vector2.One;
            Position = new Vector2(x,y);
            Size = new Vector2(sizex,sizey);

            PointSize=1;
            LineWidth=1;
            
            FillColor = fillcolor;              

        }

        public override void SetSize(Vector2 newSize)
        {
            VertexData[0].X *= newSize.X;
            VertexData[0].Y *= newSize.Y;
        }   

    }
}
