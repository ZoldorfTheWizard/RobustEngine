using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK;


namespace RobustEngine.Graphics
{
    public struct Vertex
    {

        public float X, Y, Z;      // Position
        public float R, G, B, A;   // Color
        public float Nx, Ny, Nz;   // Normal
        public float Tx, Ty;       // Texture

        #region Readonly 

        public static Vertex Zero = new Vertex(0, 0);
        public static Vertex UnitX = new Vertex(1, 0);
        public static Vertex UnitY = new Vertex(0, 1);
        public static Vertex One = new Vertex(1, 1);

        public static readonly int Stride = 48;

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
            A = 1f;

            Nx = 0f;
            Ny = 0f;
            Nz = 1f;

            Tx = 0f;
            Ty = 0f;
        }

        #region OPERATORS OH BOY

        #region Color

        public void MultiplyColor(Color color, int vertex = -1)
        {
            R *= color.R;
            G *= color.G;
            B *= color.B;
            A *= color.A;
        }

        public void AddColor(Color color, int vertex = -1)
        {
            R += color.R;
            G += color.G;
            B += color.B;
            A += color.A;
        }

        public void SubColor(Color color)
        {
            R -= color.R;
            G -= color.G;
            B -= color.B;
            A -= color.A;
        }

        public void SetColor(Color color)
        {
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }

        #endregion

        public static Vertex operator *(Vertex A, Vector3 B)
        {
            return new Vertex(A.X * B.X, A.Y * B.Y, A.Z * B.Z);
        }

        public static Vertex operator +(Vertex A, Vector3 B)
        {
            return new Vertex(A.X + B.X, A.Y + B.Y, A.Z + B.Z);
        }

        public static Vertex operator -(Vertex A, Vector3 B)
        {
            return new Vertex(A.X - B.X, A.Y - B.Y, A.Z - B.Z);
        }

        public static Vertex operator /(Vertex A, Vector3 B)
        {
            return new Vertex(A.X / B.X, A.Y / B.Y, A.Z / B.Z);
        }


        #region VERTEX MATH 

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

        #endregion VERTEX MATH 

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
