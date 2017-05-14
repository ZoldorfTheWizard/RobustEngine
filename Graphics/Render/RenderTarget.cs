using System;
using OpenTK.Graphics.OpenGL;
using RobustEngine.System;

namespace RobustEngine.Graphics.Render
{
    public class RenderTarget
    {
        private int framebufferID;
        private int depthID;
        private int textureID;

        #region Constructors 
        public RenderTarget(int width, int height)
        {
            Create(width, height, PixelInternalFormat.Rgba, false);
        }

        public RenderTarget(int width, int height, PixelInternalFormat PIF)
        {
            Create(width, height, PIF, false);
        }

        public RenderTarget(int width, int height, PixelInternalFormat PIF, bool depthbuffer )
        {
            Create(width, height, PIF, depthbuffer);                
        }

        #endregion

        private void Create(int width, int height, PixelInternalFormat PIF, bool depthbuffer)
        {

            textureID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.ClampToBorder);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.ClampToBorder);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PIF, width, height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            depthID = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, depthID);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (float)TextureMinFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (float)TextureMagFilter.Nearest);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (float)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (float)TextureWrapMode.ClampToEdge);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.DepthComponent, width, height, 0, PixelFormat.DepthComponent, PixelType.UnsignedByte, IntPtr.Zero);

            GL.BindTexture(TextureTarget.Texture2D, 0);

            framebufferID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, textureID, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, TextureTarget.Texture2D, depthID, 0);

            RobustEngine.CheckGLErrors();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);            
        }


        public void Begin()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferID);
        }


        public void End()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }


       
    }
}
