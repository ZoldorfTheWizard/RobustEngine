namespace RobustEngine.Graphics.Interfaces
{
    public interface IGLFrameBuffer
    {
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