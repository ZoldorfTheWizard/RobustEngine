using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Static Vertex of Zero Zero, Known as just Zero
        /// </summary>
        /// <returns>Vertex(0,0)</returns>
        public static Vertex Zero = new Vertex(0, 0);
        
        /// <summary>
        /// Static Vertex of One Zero, Known as UnitX
        /// </summary>
        /// <returns>Vertex(1,0)</returns>
        public static Vertex UnitX = new Vertex(1, 0);

        /// <summary>
        ///  Static Vertex of Zero One, Known as UnitY
        /// </summary>
        /// <returns>Vertex(0,1)</returns>
        public static Vertex UnitY = new Vertex(0, 1);

        /// <summary>
        ///  Static Vertex of One One, Known as just One
        /// </summary>
        /// <returns>Vertex(1,1)</returns>
        public static Vertex One = new Vertex(1, 1);

        /// <summary>
        /// Static Vertex of One Negative One, Known as Just OneNegOne
        /// Inverse Y of Vertex.One, Used for Bottom Right or Top Right Vertex Positioning
        /// </summary>
        /// <returns>Vertex(1,-1)</returns>
        public static Vertex OneNegOne = new Vertex(1, -1);


        public static readonly int Stride = 48;

        #endregion Readonly

        /// <summary>
        /// Create a 2D Vertex with two points. Specify a third float to make a 3d vertex. 
        /// </summary>
        /// <param name="x">Pos X</param>
        /// <param name="y">Pos Y</param>
        /// <param name="z">Pos Z</param>
        public Vertex(float x=0, float y=0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;

            R = 255f;
            G = 0f;
            B = 0f;
            A = 1f;

            Nx = 0f;
            Ny = 0f;
            Nz = 1f;

            Tx = 0f;
            Ty = 0f;
        }
        
        public Vertex(Vertex Copy)
        {
            X = Copy.X;
            Y = Copy.Y;
            Z = Copy.Z;

            R = Copy.R;
            G = Copy.G;
            B = Copy.B;
            A = Copy.A;

            Nx = Copy.Nx;
            Ny = Copy.Ny;
            Nz = Copy.Nz;

            Tx = Copy.Tx;
            Ty = Copy.Ty;
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

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(X, Y, Z);
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
        
        public static Vertex TransformVertex(ref Vertex vertex, Matrix4 mat4)
        {
            Vertex result = new Vertex(vertex);
            result.X = Dot(vertex, mat4.Column0);

            result.Y = Dot(vertex, mat4.Column1);

            result.Z = Dot(vertex, mat4.Column2);
            return result;
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

        public static float Dot(Vertex A, Vector4 Vec4)
        {
            return A.X * Vec4.X + A.Y * Vec4.Y + A.Z * Vec4.Z + Vec4.W;
        }

        #endregion INT MATH

        #endregion

    }

    public enum Axis
    {
        X,
        Y,
        Z
    }
}
