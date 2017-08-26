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
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace RobustEngine
{
    public class RobustEngine
    {
        //Render engine. Handles all rendering.      
        public int version;
        public string GLINFO;




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

        public Sprite Sprite;

        public RobustEngine()
        {

        }

        public void Init()
        {
            RobustConsole.ClearConsole();
            RobustConsole.SetLogLevel(LogLevel.Debug);
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Init() Intializing...");

            Timekeeper = new Clock();
            VSettings = new VideoSettings(); //TODO import video settings here

            GameScreen = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Debug);

            //GameScreen = new GameWindow();
            //GameScreen.Size = VSettings.Size;
            //GameScreen.WindowBorder = VSettings.Border;
            //GameScreen.Title = "Space Station 14";
            //GameScreen.Visible = true;

            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE

            GLINFO += "\n\n------------------------------------------------------------------";
            GLINFO += "\n OpenGL Version: " + GL.GetString(StringName.Version);
            GLINFO += "\n Vendor: " + GL.GetString(StringName.Vendor);
            GLINFO += "\n GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n------------------------------------------------------------------\n";

            RobustConsole.Write(LogLevel.Info, this, GLINFO);
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
            GL.Enable(EnableCap.Texture2D);
            //GL.Enable(EnableCap.VertexArray);

            var ImageTestFile = Path.Combine(Environment.CurrentDirectory, "Graphics", "Shaders", "ImageTest");


            //TESTING
            Texture = new Texture2D("Devtexture_Floor.png");
            PlayerView = new View(Vector2.One, 0, 10);
            //   Spritebatch = new SpriteBatch(1920, 1080);
            Sprite = new Sprite("test", Texture);
            CurrentShader = new Shader(ImageTestFile + ".vert", ImageTestFile + ".frag");

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.ClearColor(Color.Gray);
            GL.Viewport(0, 0, 800, 600);
            GL.Ortho(-400, 400, -300, 300, 0, 1);


            //Context = new GraphicsContext(GraphicsMode.Default, GameScreen.WindowInfo,4,4,GraphicsContextFlags.Default);
            //Context.MakeCurrent(GameScreen.WindowInfo);
            //(Context as IGraphicsContextInternal).LoadAll();
            //GL.Enable(EnableCap.Blend);

            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Init() Done.");
            ReadyToRun = true;
        }

        #region State 
        public void Run()
        {

            RobustConsole.Write(LogLevel.Warning, "RobustEngine", "Starting Run loop...");

            Timekeeper.Start();
            GameScreen.Run(60);

        }

        public void Update(object Sender, FrameEventArgs E)
        {
            GameScreen.ProcessEvents();
            Sprite.update();
            mov += .0001f;
        }

        float mov;
        int frames;

        public void Render(object Sender, FrameEventArgs E)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);

            // PlayerView.Setup(800, 800);
            // PlayerView.Update();
            //Spritebatch.Begin();
            Texture.Bind();
            //    GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            CurrentShader.Enable();
            Sprite.mov(mov);
            Sprite.Draw();
            CurrentShader.Disable();

            //  GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            GameScreen.SwapBuffers();



            frames++;
            if (Timekeeper.GetElapsed().Seconds >= 1)
            {
                RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() FPS " + frames);
                Timekeeper.Start();
                frames = 0;
            }
            // RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() MS " + Timekeeper.GetTime().Milliseconds.ToString());
        }

        public void Stop()
        {
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Stopping...");
        }

        #endregion State

        public void setCurrentRenderTarget(RenderTarget RT)
        {

        }

        public void setScreenTarget(NativeWindow NW)
        {

        }

        #region Global Error Checking
        public static void CheckGLErrors()
        {
            var FBOEC = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
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
        # endregion Global Error Checking

    }
}