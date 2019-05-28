using System;
using System.Collections.Generic;
using System.IO;
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
using RobustEngine.Graphics.OpenGL;
using RobustEngine.Graphics.Interfaces;
using RobustEngine.Graphics.Shapes2D;

namespace RobustEngine
{
    public class RobustEngine
    {
        //Render engine. Handles all rendering.      
        public int version;
        public string GLINFO;


        //Rendering
        public Dictionary<string, NativeWindow> Windows;

        public RenderTarget[] Rendertargets; // This may be unused due to Multiple Windows.
        public GameWindow GameScreen;
        public GameWindow AltScreens; // All other non gamescreen windows
        public static Shader CurrentShader;
        public GraphicsContext Context;
        public DisplayDevice SelectedMonitor;

        public VideoSettings VSettings;



        public int Frame;
        public int FrameTime;
       public int VAOID;
        private bool ReadyToRun;


        //Testing
        public View PlayerView;
        public Clock Timekeeper;
        public SpriteBatch Spritebatch;
        public Texture2D Texture;

        public Rect2D RectTest;
        public Triangle2D TriangleTest;
        public Triangle2D TriangleTest2;
        public Line2D LineTest;

        public GLVertexBuffer VBUFF;
        public GLIndexBuffer IBUFF;

        public Sprite Sprite;

        public RobustEngine()
        {

        }

        public void Init()
        {
           // RobustConsole.ClearConsole();
            RobustConsole.SetLogLevel(LogLevel.Debug);
            RobustConsole.Write(LogLevel.Info, "RobustEngine", "Init() Intializing...");

            Timekeeper = new Clock();
            VSettings = new VideoSettings(); //TODO import video settings here

            GameScreen = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando", GameWindowFlags.Default, DisplayDevice.Default, 3, 3, GraphicsContextFlags.Default);

            //GameScreen = new GameWindow();
            //GameScreen.Size = VSettings.Size;
            //GameScreen.WindowBorder = VSettings.Border;
            //GameScreen.Title = "Space Station 14";
            //GameScreen.Visible = true;

            // GameScreen.VSync = VSyncMode.Off;

            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE

            GLINFO += "\n\n-------------- OpenGL Initialization Report -----------------------";
            GLINFO += "\n "; 
            GLINFO += "\n OpenGL Version: " + GL.GetString(StringName.Version);
            GLINFO += "\n Vendor: " + GL.GetString(StringName.Vendor);
            GLINFO += "\n GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n------------------------------------------------------------------\n";

            RobustConsole.Write(LogLevel.Info, this, GLINFO);
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
          

            // GL.Enable(EnableCap.Blend);
            // GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            RobustEngine.CheckGLErrors();
            GL.ClearColor(0, 0, 0, 0);           


            var ImageTestFile = Path.Combine(Environment.CurrentDirectory, "Graphics", "Shaders", "ImageTest");

           


            //TESTING
         //   Texture = new Texture2D("Devtexture_Floor.png");
          //  LineTest = new Line2D(0, 0, 1, 0, 1);
            //LineTest = new Line(0, 0, 1, 1, 1);


            RectTest = new Rect2D(0,0,1,1);
            RectTest.FillColor=Color.Red;
           // RectTest.SetFillColor(Color.Red);
          //  RectTest.SetScale(new Vector2(.1f, .1f));
       //     RectTest.SetPosition(new Vector2(0f,0f));
          //  RectTest.Update();

           
            CurrentShader = new Shader(ImageTestFile + ".vert", ImageTestFile + ".frag");
            VBUFF = new GLVertexBuffer(UsageHint.Dynamic | UsageHint.Write);
            IBUFF = new GLIndexBuffer(UsageHint.Dynamic | UsageHint.Write);
            VAOID = GL.GenVertexArray();
            GL.BindVertexArray(VAOID);  
            VBUFF.Init(RectTest.VertexData);
            IBUFF.Init(RectTest.Indicies);
            GL.BindVertexArray(0);

            RectTest.DebugMode = Debug.Wireframe;
            

            



          //  PlayerView = new View(Vector2.Zero, 0, 100);            
           // GL.Viewport(0, 0, 800, 800);


            //Context = new GraphicsContext(GraphicsMode.Default, GameScreen.WindowInfo,4,4,GraphicsContextFlags.Default);
            //Context.MakeCurrent(GameScreen.WindowInfo);
            //(Context as IGraphicsContextInternal).LoadAll();
            //GL.Enable(EnableCap.Blend);

            RobustConsole.Write(LogLevel.Info, "RobustEngine", "Init() Done.");
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
            mov -= .01f;
         
            
        }

        float mov = .01f;

        float scale = 1 / 256f;

        int frames;
        Vector2 scal = new Vector2(1f, 1f);

        public void Render(object Sender, FrameEventArgs E)
        {

            GL.Clear(ClearBufferMask.ColorBufferBit);
        
           
            //   Spritebatch.Begin();
          
            CurrentShader.Enable();
            //PlayerView.Setup(1600, 1600);
           // PlayerView.Update();
          //  PlayerView.RotateTo(.25f);
            
            
            
            
            /* LineTest.SetFillColor(Color.Red);
                        
            LineTest.SetRotation(mov, Axis.Z);
            LineTest.Update();
            LineTest.Draw();

            LineTest.SetFillColor(Color.Red);
            LineTest.SetOrigin(LineTest.Center);
            LineTest.SetScale(new Vector2(.5f,.5f));
            LineTest.SetPosition(new Vector2(.10f , .10f ));
            LineTest.SetRotation(180f, Axis.Z);
            LineTest.Update();
            LineTest.Draw();

            TriangleTest.SetFillColor(Color.Blue);
            TriangleTest.DebugMode = Debug.Wireframe;
            TriangleTest.SetOrigin(TriangleTest.CenterTop);
            TriangleTest.SetScale(new Vector2(.1f, .1f));
            TriangleTest.SetRotation(-mov, Axis.Z);
            TriangleTest.SetPosition(new Vector2(0.5f, 0.5f));
            TriangleTest.Update();
            TriangleTest.Draw();

            TriangleTest2.SetFillColor(Color.Blue);
            TriangleTest2.DebugMode = Debug.Wireframe;
            TriangleTest.SetOrigin(TriangleTest.CenterTop);
            TriangleTest.SetScale(new Vector2(.1f, .1f));
            TriangleTest.SetRotation(-mov, Axis.Z);
            TriangleTest2.SetPosition(new Vector2(0.0f + mov, 0.0f+mov));
            TriangleTest2.Update();
            TriangleTest2.Draw(); */
            
           
             
            RobustEngine.CurrentShader.setUniform("ModelMatrix", RectTest.ModelMatrix);
            RobustEngine.CurrentShader.setUniform("UsingTexture", GL.GetInteger(GetPName.TextureBinding2D));

            switch (RectTest.DebugMode)
            {
                case Debug.Points: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point); break;
                case Debug.Wireframe: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); break;
                default : break;
            }
 GL.BindVertexArray(VAOID);
      //      VBUFF.Bind();
            IBUFF.Bind();
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
         //   VBUFF.Unbind();
            IBUFF.Unbind();
 GL.BindVertexArray(0);
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            RectTest.PopMatrix();

/* 
            Sprite.SetOrigin(Sprite.Rect.TopRight);
            Sprite.SetScale(new Vector2(.005f, .005f)); //+ (.001f * x), scale + (.001f * y)));
            Sprite.SetRotation(mov, Axis.Z);
            Sprite.SetPosition(new Vector2(.1f, .1f));
            Sprite.Update();
            Sprite.Draw(); */

            CurrentShader.Disable();


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


       /*  public void Draw(IShape2D Shape)
        {
            RobustEngine.CurrentShader.setUniform("ModelMatrix", Shape.ModelMatrix);
            RobustEngine.CurrentShader.setUniform("UsingTexture", GL.GetInteger(GetPName.TextureBinding2D));

            switch (Debug.None)
            {
                case Debug.Points: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Point); break;
                case Debug.Wireframe: GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line); break;
            }
//
  //          Bind();
            GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
    //        Unbind();

            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            //popmatrix;
        } */

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


    public enum Debug
    {
        None,
        Points,
        Wireframe,

    }


}