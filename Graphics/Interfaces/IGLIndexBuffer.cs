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
        
        /// <summary>
        /// Update a Index Buffer with Index Data and change the GL Buffer Usage Hint
        /// </summary>
        /// <param name="Indicies"></param>
        /// <param name="UH"></param>
        void Update(int[] Indicies, UsageHint UH);
    }
}