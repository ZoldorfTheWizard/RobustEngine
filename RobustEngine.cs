
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using RobustEngine.Graphics;
using RobustEngine.Graphics.Render;
using RobustEngine.Graphics.Shaders;
using RobustEngine.Graphics.Sprites;
using RobustEngine.System;
using RobustEngine.System.Settings;
using RobustEngine.System.Time;
using RobustEngine.Window;
using System.Collections.Generic;
using System.Drawing;

namespace RobustEngine
{
    public class RobustEngine 
    {
        //Render engine. Handles all rendering.      
        public int version;   


   


        //Add this to Atmos Engine.
        public delegate void OnAtmosUpdate();


        //Rendering
        public Dictionary<string, NativeWindow> Windows;
        public RenderTarget[] Rendertargets; // This may be unused due to Multiple Windows.
        public GameWindow GameScreen;
        public GameWindow AltScreens; // All other non gamescreen windows
        public Shader CurrentShader;
        public GraphicsContext Context;
        public DisplayDevice SelectedMonitor;

        public VideoSettings VSettings;



        public int Frame;
        public int FrameTime;

        private bool ReadyToRun;
        

        //Testing
        public View PlayerView;
        public Clock Timekeeper;
        public SpriteBatch Spritebatch;
        public Texture2D Texture;
              


        public RobustEngine()
        {
           
        }

        public void Init()
        {
            

            RobustConsole.SetLogLevel(LogLevel.Debug);
            RobustConsole.Write(LogLevel.Debug, "RobustEngine Init()", "Intializing...");
            Timekeeper = new Clock();
           
            VSettings = new VideoSettings(); //TODO import video settings here

            GameScreen              = new GameWindow();
            GameScreen.Size         = VSettings.Size;
            GameScreen.WindowBorder = VSettings.Border;
            GameScreen.Title        = "Space Station 14";
            GameScreen.Visible      = true;

            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
            GL.Enable(EnableCap.Texture2D);
         //   GL.Enable(EnableCap.VertexArray);
            GL.Viewport(0, 0, 800, 800);

            //TESTING
            Texture = new Texture2D("Devtexture_Floor.png");
            PlayerView = new View(Vector2.One, 0, 10);
            Spritebatch = new SpriteBatch(1920, 1080);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Black);
          

            //Context = new GraphicsContext(GraphicsMode.Default, GameScreen.WindowInfo,4,4,GraphicsContextFlags.Default);
            //Context.MakeCurrent(GameScreen.WindowInfo);
            //(Context as IGraphicsContextInternal).LoadAll();

            //     GL.Enable(EnableCap.Blend);
         

            RobustConsole.Write(LogLevel.Debug, "RobustEngine Init()", "Done.");
            ReadyToRun = true;
        }

        #region State 
        public void Run()
        {

            RobustConsole.Write(LogLevel.Warning, "RobustEngine", "Starting Run loop...");

            GameScreen.ProcessEvents();
            GameScreen.Run(60);

        }

        public void Update(object Sender, FrameEventArgs E)
        {
            GameScreen.ProcessEvents();
          
        }


        int frames;
        float timer = 0;
        public void Render(object Sender, FrameEventArgs E)
        {
            Timekeeper.Start();
            
          
     
         //  PlayerView.Setup(800, 800);



//PlayerView.Update();
            Spritebatch.Begin();

          
            for (int i = 0; i < 10; i++)
            {
                for (int iy = 0; iy < 10; iy++)
                {

               //     Spritebatch.Draw(Texture, new Vector2(i, iy), new Vector2(1, 1), Color.Yellow, new Vector2(0, 0));

                }
            }

         
            GameScreen.SwapBuffers();
            frames++;
          
            
            if (Timekeeper.GetElapsed().Seconds >= 1)
            {
                RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() FPS " + frames);
                Timekeeper.Reset();
                frames = 0;
            }
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() MS " + Timekeeper.GetTime().Milliseconds.ToString());
        }

        public void Stop()
        {
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Stopping...");
        }

        #endregion

        public void setCurrentRenderTarget(RenderTarget RT)
        {
           
        }

        public void setScreenTarget(NativeWindow NW)
        {
            
        }
     

        //var gw = new NativeWindow();
        //gw.Title = "aaaaaa";
        //    gw.MakeCurrent();
        //    gw.Run(60.0);

        #region Global Error Checking
        public static void CheckGLErrors()
        {
            var FBOEC =  GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            while ((FBOEC == FramebufferErrorCode.FramebufferComplete))
            {
                RobustConsole.Write(LogLevel.Verbose, "RobustEngine", FBOEC.ToString()); //TODO expand error message
                break;
            }

            var EC = GL.GetError();
            while (EC != ErrorCode.NoError)
            {
                RobustConsole.Write(LogLevel.Fatal, "RobustEngine", EC.ToString()); // TODO expand error message
            }
        }

        #endregion

    }
}
