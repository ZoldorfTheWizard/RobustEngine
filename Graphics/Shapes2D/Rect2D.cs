using System;
using OpenTK;   

namespace RobustEngine.Graphics.Shapes2D
{
    public class Rect2D : Shape2D
    {
        #region Class Variables
      
        //Point Coords
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopRight;
        public Vector2 TopLeft;
        public Vector2 Center;      

        #endregion Class Varables

        /// <summary>
        /// Constructor for a Rectangle Primitive 
        /// </summary>
        /// <param name="x">Local Space Position X</param>
        /// <param name="y">Local Space Position Y</param>
        /// <param name="w">Local Space Width/SizeX </param>
        /// <param name="h">Local Space Height/SizeY </param>
        public Rect2D()
        {
            Create(0,0,1,1,Color.DarkMagenta);
        }

           /// <summary>
        /// Constructor for a Rectangle Primitive 
        /// </summary>
        /// <param name="x">Local Space Position X</param>
        /// <param name="y">Local Space Position Y</param>
        /// <param name="w">Local Space Width/SizeX </param>
        /// <param name="h">Local Space Height/SizeY </param>
        public Rect2D(float x, float y, float w, float h)
        {
            Create(x, y, w, h, Color.DarkMagenta);
        }

            /// <summary>
        /// Constructor for a Rectangle Primitive 
        /// </summary>
        /// <param name="x">Local Space Position X</param>
        /// <param name="y">Local Space Position Y</param>
        /// <param name="w">Local Space Width/SizeX </param>
        /// <param name="h">Local Space Height/SizeY </param>
        public Rect2D(int x, int y, int w, int h)
        {
            Create((float)x, (float)y, (float)w, (float)h, Color.DarkMagenta);
        }

            /// <summary>
        /// Constructor for a Rectangle Primitive 
        /// </summary>
        /// <param name="x">Local Space Position X</param>
        /// <param name="y">Local Space Position Y</param>
        /// <param name="w">Local Space Width/SizeX </param>
        /// <param name="h">Local Space Height/SizeY </param>
        public Rect2D(Rect2D rect)
        {
            Create(rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y, Color.DarkMagenta);
        }

        /// <summary>
        /// Create A Rectangle Primitive 
        /// </summary>
        /// <param name="x">Local Space Position X</param>
        /// <param name="y">Local Space Position Y</param>
        /// <param name="w">Local Space Width/SizeX </param>
        /// <param name="h">Local Space Height/SizeY </param>
        /// <param name="fillColor"></param>
        private void Create(float x, float y, float w, float h, Color fillColor)
        {
            VertexData = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX,
                Vertex.One,
                Vertex.UnitY
            };

            Indicies = new int[]
            {
                  0, 1, 2, // First Triangle
                  2, 3, 0  // Second Triangle
            };
            
            ModelMatrix = Matrix4.Identity;
            Scale = Vector2.One; 
            Position = new Vector2(x,y);           
            Size = new Vector2(w,h);   
            FillColor = fillColor;  
            
            PointSize = 1f;
            LineWidth = 1f;         
                     
            // Origin Helpers 
            BottomLeft  = VertexData[0].ToVector2();
            BottomRight = VertexData[1].ToVector2();
            TopRight    = VertexData[2].ToVector2();
            TopLeft     = VertexData[3].ToVector2();
            Center      = new Vector2( Size.X / 2, Size.Y / 2);
        }
        
        public override void SetSize(Vector2 newSize)
        {
            VertexData[1].X *= newSize.X;
            VertexData[2].X *= newSize.X;
            VertexData[2].Y *= newSize.Y;
            VertexData[3].Y *= newSize.Y;
        }   
  
        public Rect2D Union(Rect2D A, Rect2D B)
        {
            var x = Math.Min(A.Position.X, B.Position.X);
            var width = Math.Max(A.Position.X + A.Size.X, B.Position.X);

            var y = Math.Min(A.Position.Y, B.Position.Y);
            var height = Math.Max(A.Position.Y + A.Size.Y, B.Position.Y);

            return new Rect2D(x, y, width, height);
        }


    }
}
