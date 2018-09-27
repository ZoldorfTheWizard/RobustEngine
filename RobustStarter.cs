using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RobustEngine.Graphics;
using RobustEngine.Graphics.Render;
using RobustEngine.Graphics.Shaders;
using RobustEngine.Graphics.Sprites;
using RobustEngine.System;
using RobustEngine.Window;


namespace RobustEngine
{
    public class RobustStarter
    {

        public static RobustEngine RE;

        public static void Main()
        {
            RobustConsole.SetLogLevel(LogLevel.Debug);

            RE = new RobustEngine();
            RE.Init();
			///tetetetetetet
            RE.Run();
                      
        }
    }
}
