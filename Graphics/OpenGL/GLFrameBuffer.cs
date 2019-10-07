using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{
    public sealed class GLFrameBuffer : IFrameBuffer
    {
        private int ID;

        private GLTexture ColorAttachment;
        private GLTexture DepthAttachment;

        public int ID 
        {
            get { return id; }
        }  

        public GLFrameBuffer()
        {    
            ColorAttachment = new GLTexture(GLTextureTarget.Texture2D, GLTexture.DEFAULT_GL_TEXTURE_PARAMS);
            DepthAttachment  = new GLTexture(GLTextureTarget.Texture2D, GLTexture.DEFAULT_GL_TEXTURE_PARAMS);
        }

        public GLFrameBuffer(GLTextureTarget TT)
        {
            ColorAttachment = new GLTexture(TT,GLTexture.DEFAULT_GL_TEXTURE_PARAMS);
            DepthAttachment  = new GLTexture(TT,GLTexture.DEFAULT_GL_TEXTURE_PARAMS);
        }

        public GLFrameBuffer(GLTextureTarget TT, GLTextureParams TP)
        {           
            ColorAttachment = new GLTexture(TT,TP);
            DepthAttachment  = new GLTexture(TT,TP);
        }   

        ///<inherit-doc />
		public void Create(int w, int h, bool depth=false, bool stencil=false)
        {   
            ID = GL.GenFramebuffer();

            ColorAttachment.Bind();
            ColorAttachment.Create(w,h,IntPtr.Zero);
            ColorAttachment.Unbind();

            GL.BindFramebuffer(FramebufferTarget.Framebuffer, FramebufferID);
            GL.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, GLTexTarget, ColorAttachment.ID, 0);
 
            int rbo = GL.GenRenderbuffer();
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,rbo);
            GL.RenderbufferStorage(RenderbufferTarget.Renderbuffer,RenderbufferStorage.DepthComponent,w,h);
            GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer,FramebufferAttachment.DepthAttachment,RenderbufferTarget.Renderbuffer,rbo);
            GL.BindRenderbuffer(RenderbufferTarget.Renderbuffer,0);

            GL.FramebufferTexture(FramebufferTarget.Framebuffer,FramebufferAttachment.ColorAttachment0,ColorAttachment.ID,0);

            #if CHECKGLERRORS
            RobustEngine.CheckGLErrors();
            #endif
           
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);           
        }

        ///<inherit-doc />
		public void Begin()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,FramebufferID);
        }

        ///<inherit-doc />
		public void End()
        {
            GL.BindFramebuffer(FramebufferTarget.Framebuffer,0);
        }

        ///<inherit-doc />
		public void Bind()
        {
            GL.BindTexture(GLTexTarget, ColorAttachment.ID);
        }

        ///<inherit-doc />
		public void Unbind()
        {
            GL.BindTexture(GLTexTarget,0);
        }

      
    }
}