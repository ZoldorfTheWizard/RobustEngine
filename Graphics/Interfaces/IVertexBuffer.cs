namespace RobustEngine.Graphics.Interfaces
{
    public interface IVertexBuffer
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
        ///  Initialize Empty Vertex Buffer
        /// </summary>
        void Create();

        /// <summary>
        /// Update Vertex Buffer with new Vertex Data
        /// </summary>
        /// <param name="VertexData">Vertex Data</param>
        void Update(Vertex[] VertexData);

         /// <summary>
        /// Update Vertex Buffer with new Vertex Data, and Update the usage hint
        /// </summary>
        /// <param name="VertexData">Vertex Data</param>
        void Update(Vertex[] VertexData, UsageHint UH);
    
        /// <summary>
        /// Unbind the buffer
        /// </summary>
        void Unbind();
    }
}