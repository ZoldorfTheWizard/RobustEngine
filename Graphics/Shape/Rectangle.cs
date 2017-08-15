using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace RobustEngine.Graphics.Shape
{
	public class Rectangle
	{


		public float X;
		public float Y;
		public float Width;
		public float Height;

		public int VertexArrayID;
        public float Left   => X;
        public float Top    => Y;
        public float Right  => Width;
        public float Bottom => Height;
		public int VertexBufferID;
		public int IndexBufferID;
		public int[] Indicies;

		public Color Color;
		public Vertex[] VertexData;


		/// <summary>
		/// Rectangle Entity Constructor.
		/// </summary>
		/// <param name="posX">Position X.</param>
		/// <param name="posY">Position Y</param>
		/// <param name="sizeX">Size X.</param>
		/// <param name="sizeY">Size Y</param>
		public Rectangle(int posX, int posY, int sizeX, int sizeY)
		{
			Create(posX, posY, sizeX, sizeY, Color.Transparent);
		}

		/// <summary>
		/// Rectangle Entity Constructor.
		/// </summary>
		/// <param name="posX">Position X.</param>
		/// <param name="posY">Position Y</param>
		/// <param name="sizeX">Size X.</param>
		/// <param name="sizeY">Size Y</param>
		public Rectangle(int posX, int posY, int sizeX, int sizeY, Color fc)
		{
			Create(posX, posY, sizeX, sizeY, fc);
		}

		private void Create(int posX, int posY, int sizeX, int sizeY, Color fillColor)
		{
			X = posX;
			Y = posY;
			Width = sizeX;
			Height = sizeY;
			Color = fillColor;

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
				//VertexData[i].SetColor(fillColor);
			}

			//Set W/H
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

			Indicies = new int[]
			{
				  0, 1, 2, // First Triangle
                  2, 3, 0  // Second Triangle
            };

			VertexArrayID = GL.GenVertexArray(); // Opengl Please give us a empty State
			GL.BindVertexArray(VertexArrayID);   // Opengl Please use and write everything to this new empty state now thx            

			//Generate Buffers
			VertexBufferID = GL.GenBuffer(); // Hey Opengl we want a empty VBO to use for data pls generate thx
			IndexBufferID = GL.GenBuffer();  // Hey Opengl we want a empty VBO to use for indexing pls generate      // Hey Opengl we want a empty VBO to use for colors pls generate thx

			// VBO
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
			GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);

			// Vertex Data
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 0);
			GL.EnableVertexAttribArray(0); // Layout 1 Position Data

			// Color Data
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 12);
			GL.EnableVertexAttribArray(1); // Layout 2 Color Data

			// Normal Data
			GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, Vertex.Stride, 24);
			GL.EnableVertexAttribArray(2); // Layout 2 Color

			// TextureUVCoords
			GL.VertexAttribPointer(3, 2, VertexAttribPointerType.Float, false, Vertex.Stride, 36);
			GL.EnableVertexAttribArray(3); // Layout 3 TexCoord

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

			//INDEX DATA
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
			GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(Indicies.Length * sizeof(float)), Indicies, BufferUsageHint.DynamicDraw);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);

			GL.BindVertexArray(0);


		}

		public void Update()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
			GL.BufferData(BufferTarget.ArrayBuffer, VertexData.Length * Vertex.Stride, VertexData, BufferUsageHint.DynamicDraw);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		public void BindVertexArray()
		{
			GL.BindVertexArray(VertexArrayID);
		}

		public void BindVertexBuffer()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, VertexBufferID);
		}

		public void BindIndexBuffer()
		{
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, IndexBufferID);
		}

		public void Unbind()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			GL.BindVertexArray(0);
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
				if (Y + Height >= A.Y && A.Y + A.Height >= Y)
				{
					return true; //Collision
				}
			}
			return false;
		}

	}
}
