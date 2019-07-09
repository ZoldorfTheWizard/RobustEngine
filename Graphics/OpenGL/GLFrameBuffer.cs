using System;
using System.Reflection;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLFrameBuffer
    {

        private GLTexture texture;
        private GLTexture depth;

        private GLTextureTarget GLTexTarget;
        private int framebufferID;


        public GLFrameBuffer()
        {
           texture = new GLTexture(TextureTarget.Texture2D);
           depth  = new GLTexture(TextureTarget.Texture2D);
        }

        private void Create(TextureTarget TT)
        {   
            texture.Create(IntPtr.Zero,1,1);
            depth.Create(IntPtr.Zero, 1, 1, 0, InternalFormat.Depth);

            GLTexTarget = GLHelper.CheckTextureTarget(TT);

            framebufferID = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebufferID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, GLTexTarget, texture.ID, 0);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, GLTexTarget, depth.ID, 0);

            RobustEngine.CheckGLErrors();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);           
        }




    }
}