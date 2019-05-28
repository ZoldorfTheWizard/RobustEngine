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

           
        }


        public void Begin()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferID);
        }


        public void End()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
        }

		public void Bind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferID);
		}

		public void Unbind()
		{
			GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
		}       
    }
}
