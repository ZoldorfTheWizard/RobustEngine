using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Graphics
{
    public struct Vertex
    {

        public float X, Y, Z;      // Position
        public float R, G, B;      // Color
        public float NX, NY, NZ;   // Normal
        public float Tx, Ty;       // Texture

        #region Readonly 

        public static readonly Vertex Zero  = new Vertex(0, 0);
        public static readonly Vertex UnitX = new Vertex(1, 0);
        public static readonly Vertex UnitY = new Vertex(0, 1);
        public static readonly Vertex One   = new Vertex(1, 1);               
        public static readonly int    Stride  = 44;

        #endregion Readonly
        
        /// <summary>
        /// Create a 2D Vertex with two points. Specify a third float to make a 3d vertex. 
        /// </summary>
        /// <param name="x">Pos X</param>
        /// <param name="y">Pos Y</param>
        /// <param name="z">Pos Z</param>
        public Vertex(float x, float y, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;

            R = 0f;
            G = 0f;
            B = 0f;

            NX = 0f;
            NY = 0f;
            NZ = 1f;

            Tx = 0f;
            Ty = 0f;
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
        #endregion VECTOR MATH 

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
        #endregion INT MATH

        #region FLOAT MATH
        public static Vertex operator *(Vertex A, float B)
        {
            return new Vertex(A.X * B, A.Y * B, A.Z * B);
        }

        public static Vertex operator +(Vertex A, float B)
        {
            return new Vertex(A.X + B, A.Y + B, A.Z + B);
        }

        public static Vertex operator -(Vertex A, float B)
        {
            return new Vertex(A.X - B, A.Y - B, A.Z - B);
        }

        public static Vertex operator /(Vertex A, float B)
        {
            return new Vertex(A.X / B, A.Y / B, A.Z / B);
        }
        #endregion INT MATH

        #endregion


    }
}
