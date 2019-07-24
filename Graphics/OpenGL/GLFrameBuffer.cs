using System;
using System.Reflection;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;
using RobustEngine.Graphics.Interfaces;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLFrameBuffer : IGLFrameBuffer
    {

        public int FramebufferID;
        
        private GLTexture ColorAttachment;
        private GLTexture DepthAttachment;
        private GLTextureTarget GLTexTarget;
        private GLTextureParams GLTexParams;



        public GLFrameBuffer(TextureTarget TT)
        {
            ColorAttachment = new GLTexture(TT);
            DepthAttachment  = new GLTexture(TT);
            FramebufferID = GL.GenFramebuffer();

            GLTexTarget = GLHelper.CheckTextureTarget(TT);     
            GLTexParams = GLHelper.DefaultGLTextureParams;       
        }

        public void GLCreate(int w, int h, bool depth=true, bool stencil=false)
        {   
            ColorAttachment.GLCreate(IntPtr.Zero,w,h);
            ColorAttachment.SetTextureParams(GLTexParams);    

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, GLTexTarget, ColorAttachment.ID, 0);

            if(depth)
            {
                DepthAttachment.GLCreate(IntPtr.Zero, w, h, 0, InternalFormat.Depth);
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, GLTexTarget, DepthAttachment.ID, 0);

            }

            if (stencil)
            {              
                GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.Stencil, GLTexTarget, DepthAttachment.ID, 0);
            }

            #if CHECKGLERRORS
            RobustEngine.CheckGLErrors();
            #endif

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);           
        }

        public void Begin()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,FramebufferID);
        }

        public void End()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
        }

        public void Bind()
        {
            GL.BindTexture(GLTexTarget, ColorAttachment.ID);
        }

        public void Unbind()
        {
            GL.BindTexture(GLTexTarget,0);
        }
    }
}