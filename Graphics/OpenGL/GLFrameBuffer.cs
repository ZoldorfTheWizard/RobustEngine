using System;
using RobustEngine.Graphics.Interfaces;
using OpenTK.Graphics.OpenGL;
using GLTextureTarget = OpenTK.Graphics.OpenGL.TextureTarget;

namespace RobustEngine.Graphics.OpenGL
{
    public class GLFrameBuffer : IFrameBuffer
    {
        public int FramebufferID;
        
        private GLTexture ColorAttachment;
        private GLTexture DepthAttachment;
        private GLTextureTarget GLTexTarget;
        private GLTextureParams GLTexParams;

        public GLFrameBuffer()
        {
            GLTexTarget = GLTextureTarget.Texture2D;     
            GLTexParams = GLTexture.DEFAULT_GL_TEXTURE_PARAMS;       
            ColorAttachment = new GLTexture(GLTexTarget);
            DepthAttachment  = new GLTexture(GLTexTarget);
           
            FramebufferID = GL.GenFramebuffer();
        }

        public GLFrameBuffer(GLTextureTarget TT)
        {
            GLTexTarget = TT;                    
            GLTexParams = GLTexture.DEFAULT_GL_TEXTURE_PARAMS;       
            ColorAttachment = new GLTexture(GLTexTarget);
            DepthAttachment  = new GLTexture(GLTexTarget);
         
            FramebufferID = GL.GenFramebuffer();
        }

        public GLFrameBuffer(GLTextureTarget TT, GLTextureParams TP)
        {           
            GLTexTarget = TT;
            GLTexParams = TP;  
            ColorAttachment = new GLTexture(GLTexTarget);
            DepthAttachment  = new GLTexture(GLTexTarget);                
        
            FramebufferID = GL.GenFramebuffer();
        }   

        public void Create(int w, int h, bool depth=false, bool stencil=false)
        {   
            ColorAttachment.Bind();
            ColorAttachment.Create(w,h,IntPtr.Zero);
            ColorAttachment.SetTextureParams(GLTexParams);   
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