namespace RobustEngine.Graphics.Interfaces
{
    public interface IVertexBuffer
    {   
        /// <summary>
        ///  Initialize a GLBuffer as a Vertex Buffer with VertexData
        /// </summary>
        /// <param name="VertexData">Vertex Data</param>
        void Init(Vertex[] VertexData); 
        
        /// <summary>
        /// Update Vertex Buffer with new Vertex Data
        /// </summary>
        /// <param name="VertexData">Vertex Data</param>
        void Update(Vertex[] VertexData);

        /// <summary>
        /// Update Vertex Buffer with new Vertex Data and change the GL Buffer Usage Hint
        /// </summary>
        /// <param name="VertexData"></param>
        /// <param name="UH"></param>
        void Update(Vertex[] VertexData, UsageHint UH);
    }
}