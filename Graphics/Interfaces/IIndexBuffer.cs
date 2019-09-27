namespace RobustEngine.Graphics.Interfaces
{
    public interface IIndexBuffer
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
        /// Update a Index Buffer with Index Data
        /// </summary>
        void Update(int[] Indicies);      

        /// <summary>
        /// Update a Index Buffer with Index Data
        /// </summary>
        void Update(int[] Indicies, UsageHint UH);     

         /// <summary>
        /// Unbind the buffer
        /// </summary>
        void Unbind();

    }
}