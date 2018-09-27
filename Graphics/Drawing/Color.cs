namespace RobustEngine.Graphics.Drawing
{
    public struct Color
    {
        private long rgba; 

        public Color(int R = 0, int G = 0, int B = 0, int A = 0)
        {
            rgba = 0xFFFFFFFF; //Black 
            rgba = (rgba       & (byte) R);
            rgba = (rgba << 8  & (byte) G); 
            rgba = (rgba << 16 & (byte) B);
            rgba = (rgba << 24 & (byte) A);
        }     
    }
}