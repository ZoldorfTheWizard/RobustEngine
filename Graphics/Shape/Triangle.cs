using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RobustEngine.Graphics.Shape
{
    public class Triangle
    {
		public Vertex[] VertexData;

		public float X;
		public float Y;
		public float Z; 


		public Triangle(float p1, float p2, float p3)
		{
			X = p1;
			Y = p2;
			Z = p3;

			VertexData = new Vertex[]
			{
				Vertex.Zero,
				Vertex.UnitX,
				Vertex.One,
				Vertex.UnitY
			};

		}

    }
}
