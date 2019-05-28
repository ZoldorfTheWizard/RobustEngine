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
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect2D(float posX, float posY, float sizeX, float sizeY)
        {
            Create(posX, posY, sizeX, sizeY, Color.DarkMagenta);
        }

        /// <summary>
        /// Rectangle Entity Constructor.
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect2D(int posX, int posY, int sizeX, int sizeY)
        {
            Create((float)posX, (float)posY, (float)sizeX, (float)sizeY, Color.DarkMagenta);
        }

        /// <summary>
        /// Rectangle Entity Constructor.,
        /// </summary>
        /// <param name="posX">Position X.</param>
        /// <param name="posY">Position Y</param>
        /// <param name="sizeX">Size X.</param>
        /// <param name="sizeY">Size Y</param>
        public Rect2D(Rect2D rect)
        {
            Create(rect.Position.X, rect.Position.Y, rect.Size.X, rect.Size.Y, Color.DarkMagenta);
        }


        private void Create(float posX, float posY, float sizeX, float sizeY, Color fillColor)
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
            Position = new Vector2(posX,posY);           
            Size = new Vector2(sizeX,sizeY);   
          
            PointSize = 1f;
            LineWidth = 1f;

            FillColor = fillColor;
            
            SetHelperPoints();
            SetTextureMapping();        
          
        }

        private void SetHelperPoints()
        {
            //Point Coords
            BottomLeft  = VertexData[0].ToVector2();
            BottomRight = VertexData[1].ToVector2();
            TopRight    = VertexData[2].ToVector2();
            TopLeft     = VertexData[3].ToVector2();
            Center      = new Vector2( Size.X / 2, Size.Y / 2);
        }

        private void SetTextureMapping()
        {
            //Texture Mapping
            VertexData[0].Tx = 0;
            VertexData[0].Ty = 1;

            VertexData[1].Tx = 1;
            VertexData[1].Ty = 1;

            VertexData[2].Tx = 1;
            VertexData[2].Ty = 0;

            VertexData[3].Tx = 0;
            VertexData[3].Ty = 0;   
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
            var width = Math.Max(A.Position.X + A.Position.X + A.Size.X, B.Position.X);

            var y = Math.Min(A.Position.Y, B.Position.Y);
            var height = Math.Max(A.Position.Y + A.Position.Y + A.Size.Y, B.Position.Y);

            return new Rect2D(x, y, width, height);
        }


    }
}
