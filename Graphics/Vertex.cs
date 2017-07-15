using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Graphics
{
    public struct Vertex
    {
        public static readonly Vertex Zero = new Vertex(0, 0);
        public static readonly Vertex UnitX = new Vertex(1, 0);
        public static readonly Vertex UnitY = new Vertex(0, 1);
        public static readonly Vertex One = new Vertex(1, 1);       
       
        public float X, Y, Z;    // Position
        public float NX, NY, NZ; // Normal
        public float S, T;       // Texture


        public Vertex(float x, float y, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;

            NX = 0.0f;
            NY = NX;
            NZ = 1.0f;

            S = 0.0f;
            T = 1.0f;
        }

        #region OPERATORS OH BOY

        #region VECTOR MATH 
        public static Vertex operator *(Vertex A, Vertex B)
        {
            return new Vertex(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
        }

        public static Vertex operator +(Vertex A, Vertex B)
        {
            return new Vertex(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static Vertex operator -(Vertex A, Vertex B)
        {
            return new Vertex(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        public static Vertex operator /(Vertex A, Vertex B)
        {
            return new Vertex(A.X / B.X, A.Y / B.Y, A.Z / B.Z);
        }
        #endregion

        #region INT MATH
        public static Vertex operator *(Vertex A, int B)
        {
            return new Vertex(A.X * B, A.Y * B, A.Z * B);
        }

        public static Vertex operator +(Vertex A, int B)
        {
            return new Vertex(A.X + B, A.Y + B, A.Z + B);
        }

        public static Vertex operator -(Vertex A, int B)
        {
            return new Vertex(A.X - B, A.Y - B, A.Z - B);
        }

        public static Vertex operator /(Vertex A, int B)
        {
            return new Vertex(A.X / B, A.Y / B, A.Z / B);
        }
        #endregion

        #endregion
    }
}
