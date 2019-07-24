namespace RobustEngine.Graphics.Interfaces
{
    public interface IVertexBuffer
    {   
        /// <summary>
        ///  Initialize Empty Vertex Buffer
        /// </summary>
        void Init(); 
        
        /// <summary>
        /// Update Vertex Buffer with new Vertex Data
        /// </summary>
        /// <param name="VertexData">Vertex Data</param>
        void Update(Vertex[] VertexData);
    }
}