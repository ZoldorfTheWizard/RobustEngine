using System.Drawing;

namespace RobustEngine.Graphics.Shape
{
    public class Rectangle
	{
        public Vertex[] VertexData;

		public float X;
		public float Y;
		public float Width;
		public float Height;
		public Color Color;


		/// <summary>
		/// Rectangle Entity Constructor.
		/// </summary>
		/// <param name="posX">Position X.</param>
		/// <param name="posY">Position Y</param>
		/// <param name="sizeX">Size X.</param>
		/// <param name="sizeY">Size Y</param>
		public Rectangle(int posX, int posY, int sizeX, int sizeY)
        {
			X = posX;
			Y = posY;
			Width = sizeX;
			Height = sizeY;

			VertexData = new Vertex[]
			{
				Vertex.Zero,
				Vertex.UnitX,
				Vertex.One,
				Vertex.UnitY
			};

			for (int i = 0; i < VertexData.Length; i++)
			{
				VertexData[i].X += X;
				VertexData[i].Y += Y;
			}

			Calculate();
        }

		private void Calculate()
		{
			VertexData[1].X += Width;
			VertexData[2].X += Width;
			VertexData[2].Y += Height;
			VertexData[3].Y += Height;

			//Texture Data
			VertexData[0].Tx = 0;
			VertexData[0].Ty = 1;

			VertexData[1].Tx = 1;
			VertexData[1].Ty = 1;

			VertexData[2].Tx = 1;
			VertexData[2].Ty = 0;

			VertexData[3].Tx = 0;
			VertexData[3].Ty = 0;		
		}

		/// <summary>
		/// Detects if a Rectangle collides or intersects each other.
		/// </summary>
		/// <returns>The intersects.</returns>
		/// <param name="A">A.</param>
		public bool Intersects(Rectangle A)
		{
			// Collision X?
			if (X + Width >= A.X && A.X + A.Width >= X)
			{
				//Collision Y?
				if(Y + Height >= A.Y && A.Y + A.Height >= Y)
				{
					return true; //Collision
				}
			}
			return false;
		}

    }
}
