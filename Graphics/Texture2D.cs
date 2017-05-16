using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using GLPixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;
using PixelFormat = System.Drawing.Imaging.PixelFormat;
using System.Runtime.InteropServices;
using System;

namespace RobustEngine.Graphics
{
    public class Texture2D
    {

        public int ID;

        private Bitmap     Bitmap;
        private BitmapData BitmapData;
        private Color[,]   PixelData;

        public Vertex[]  TexCoords;
        public Rectangle TextureAABB;


        public Texture2D(string path)
        {
           Load(path);         
        }

        public Texture2D(string path, PixelInternalFormat PIF)
        {        
            Load(PIF,path);
        }

        public void Bind()
        {
            GL.BindTexture(TextureTarget.Texture2D, ID); 
        }              

        private void Load(string path)
        {
            Load( PixelInternalFormat.Rgba, path);
        }
      
        private void Load(PixelInternalFormat PIF, string path)
        {
            ID = GL.GenTexture(); 

            Bind();

            Bitmap = new Bitmap(path);

            if(Bitmap.Width % 2 != 0 || Bitmap.Height % 2 != 0)
            {
                throw new Exception("TEXTURE AINT A POWER OF TWO"); 
            }

            TextureAABB = new Rectangle(0, 0, Bitmap.Width, Bitmap.Height);
            PixelData   = new Color[TextureAABB.Width, TextureAABB.Height];
            BitmapData  = Bitmap.LockBits(TextureAABB, ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            TexCoords = new Vertex[]
            {
                Vertex.Zero,
                Vertex.UnitX * Bitmap.Width, 
                Vertex.UnitY * Bitmap.Width,
                Vertex.One * Bitmap.Width 

            };
                
            GL.TexImage2D
                (
                    TextureTarget.Texture2D,
                    0, 
                    PIF,
                    TextureAABB.Width,
                    TextureAABB.Height,                        
                    0,
                    GLPixelFormat.Bgra, 
                    PixelType.UnsignedByte,
                    BitmapData.Scan0
                );
                 
            Bitmap.UnlockBits(BitmapData);

            for (int x = 0; x < Bitmap.Width; x++)
            {
                for (int y = 0; y < Bitmap.Height; y++)
                {
                    PixelData[x, y] = Bitmap.GetPixel(x, y);
                }
            }

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Clamp);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Clamp);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                     

            //todo Mipmap + Bump map maybe?

        }
         


       


    }


    public enum BlendMode
    {
        /// <summary>No blending.</summary>
        None = 0,
        /// <summary>Modulated blending.</summary>
        Modulated = 1,
        /// <summary>Additive blending.</summary>
        Additive = 2,
        /// <summary>Inverse modulated blending.</summary>
		ModulatedInverse = 4,
        /// <summary>Color blending.</summary>
        Color = 8,
        /// <summary>Additive color.</summary>
        ColorAdditive = 16,
        /// <summary>Use premultiplied.</summary>
        PreMultiplied = 32,
        /// <summary>Invert.</summary>
        Inverted = 64
    }

    /// <summary>
    /// Enumeration for alpha blending operations.
    /// </summary>
    public enum AlphaBlendOperation
    {
        /// <summary>Blend factor of 0,0,0.</summary>
        Zero = 0,
        /// <summary>Blend factor is 1,1,1.</summary>
        One = 1,
        /// <summary>Blend factor is Rs', Gs', Bs', As.</summary>
        SourceColor = 2,
        /// <summary>Blend factor is As', As', As', As.</summary>
        SourceAlpha = 3,
        /// <summary>Blend factor is 1-Rs', 1-Gs', 1-Bs', 1-As.</summary>
        InverseSourceColor = 4,
        /// <summary>Blend factor is 1-As', 1-As', 1-As', 1-As.</summary>
        InverseSourceAlpha = 5,
        /// <summary>Blend factor is Rd', Gd', Bd', Ad.</summary>
        DestinationColor = 6,
        /// <summary>Blend factor is Ad', Ad', Ad', Ad.</summary>
        DestinationAlpha = 7,
        /// <summary>Blend factor is 1-Rd', 1-Gd', 1-Bd', 1-Ad.</summary>
        InverseDestinationColor = 8,
        /// <summary>Blend factor is 1-Ad', 1-Ad', 1-Ad', 1-Ad.</summary>
        InverseDestinationAlpha = 9,
        /// <summary>Blend factor is f,f,f,1 where f = min(A, 1-Ad)</summary>
        SourceAlphaSaturation = 10,
        /// <summary>Source blend factor is 1-As', 1-As', 1-As', 1-As and destination is As', As', As', As.  Overrides the blend destination, and is only valid if the SourceBlend state is true.</summary>
        BothInverseSourceAlpha = 11,
        /// <summary>Constant color blend factor.  Only valid if the driver SupportBlendingFactor is true.</summary>
        BlendFactor = 12,
        /// <summary>Inverted constant color blend factor.  Only valid if the driver SupportBlendingFactor capability is true.</summary>
        InverseBlendFactor = 13
    }
}
