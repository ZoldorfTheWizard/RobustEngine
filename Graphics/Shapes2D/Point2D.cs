using RobustEngine.Graphics.OpenGL;
using OpenTK;

namespace RobustEngine.Graphics.Shapes2D
{
    public class Point2D : Shape2D, IRenderable2D
    {

        public Point2D() 
        {
            Create(0,0,1,1,Color.DarkMagenta);
        }

        public Point2D(float x, float y, float w = 1, float h = 1) 
        {
            Create(x,y,w, h,Color.DarkMagenta);
        }

        public Point2D(int x, int y, int w = 1, int h =1) 
        {
            Create(x,y,w,h, Color.DarkMagenta);
        }

        private void Create(float x, float y, float sizex, float sizey, Color fillcolor)
        {
            VertexData = new Vertex[]
            {   
                Vertex.One
            };  

            Indicies = new int[]
            {
                0
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
