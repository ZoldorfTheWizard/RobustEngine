namespace RobustEngine.Graphics.Interfaces
{
    public interface IGLIndexBuffer
    {
        /// <summary>
        /// Initialize Empty Index Buffer 
        /// </summary>
        void Init();

        /// <summary>
        /// Update a Index Buffer with Index Data
        /// </summary>
        void Update(int[] Indicies);        
    }
}