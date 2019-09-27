namespace RobustEngine.Graphics.Interfaces
{
    public interface IFrameBuffer
    {
        /// <summary>
        /// Create a Framebuffer
        /// </summary>
        void Create(int w, int h, bool depth, bool stencil);

        /// <summary>
        /// Begin Drawing to Framebuffer
        /// </summary>
        void Begin();

         /// <summary>
        /// Stop Drawing to Framebuffer
        /// </summary>
        void End();

        /// <summary>
        /// Bind Framebuffer Color Attachment/Texture 
        /// </summary>
        void Bind();

         /// <summary>
        /// Unbind Framebuffer Color Attachment
        /// </summary>
        void Unbind();      
    }
}