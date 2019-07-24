namespace RobustEngine.Graphics.Interfaces
{
    public interface IGLBuffer
    {  
        /// <summary>
        /// GL Generated Buffer ID
        /// </summary>
        int ID {get; }
        
        /// <summary>
        /// Bind the buffer
        /// </summary>
        void Bind();

        /// <summary>
        /// Unbind the buffer
        /// </summary>
        void Unbind();
    }
}