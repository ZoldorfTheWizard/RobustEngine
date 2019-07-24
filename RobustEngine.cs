using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using RobustEngine.Graphics.Batches;
using RobustEngine.Graphics.Shapes2D;
using RobustFrameBuffer = RobustEngine.Graphics.Render.Framebuffer;

namespace RobustEngine
{
    public class RobustEngine
    {
        //Render engine. Handles all rendering.      
        public int version;
        public string GLINFO;


        //Rendering
        public Dictionary<string, NativeWindow> Windows;

        public Framebuffer[] Rendertargets; // This may be unused due to Multiple Windows.
        public GameWindow GameScreen;
        public GameWindow AltScreens; // All other non gamescreen windows
        public static Shader CurrentShader; public static Shader CurrentShader2;
        public GraphicsContext Context;
        public DisplayDevice SelectedMonitor;

        public VideoSettings VSettings;



        public int Frame;
        public int FrameTime;
        public int VAOID;
        public int VAOID2;
        public int[] intdata;
        private bool ReadyToRun;


        //Testing
        public View PlayerView;
        public Clock Timekeeper;
        public SpriteBatch Spritebatch;
        public Texture2D Texture;

        public Vertex VertTest0,VertTest1,VertTest2,VertTest3;
        public Vertex[] vertdata;

        public Rect2D RectTest;
        public Rect2D RectTest2;
      
        public Triangle2D TriangleTest;
        public Triangle2D TriangleTest2;
        public Line2D LineTest;
        public Point2D PointTest;
        public Shape2DBatch Shape2DBatchTest;

        public GLVertexBuffer VBUFF;
        public GLIndexBuffer IBUFF;

        public Sprite Sprite;
        public RobustFrameBuffer RenderTarget;

        public RobustEngine()
        {
            Timekeeper = new Clock();
            VSettings = new VideoSettings();
        }

        public void Init()
        {
           // RobustConsole.ClearConsole();
            RobustConsole.SetLogLevel(LogLevel.Debug);
            RobustConsole.Write(LogLevel.Info, "RobustEngine", "Init() Intializing...");

            
            //TODO import video settings here

            GameScreen = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando", GameWindowFlags.Default, DisplayDevice.Default, 4, 3, GraphicsContextFlags.Default);
            AltScreens = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando2", GameWindowFlags.Default, DisplayDevice.Default, 4, 3, GraphicsContextFlags.Default);

            //GameScreen = new GameWindow();
            //GameScreen.Size = VSettings.Size;
            //GameScreen.WindowBorder = VSettings.Border;
            //GameScreen.Title = "Space Station 14";
            //GameScreen.Visible = true;

            // GameScreen.VSync = VSyncMode.Off;
            AltScreens.MakeCurrent();
            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE
            AltScreens.Visible=true;


            GLINFO += "\n\n-------------- OpenGL Initialization Report -----------------------";
            GLINFO += "\n "; 
            GLINFO += "\n OpenGL Version: " + GL.GetString(StringName.Version);
            GLINFO += "\n Vendor: " + GL.GetString(StringName.Vendor);
            GLINFO += "\n GLSL Version: " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n------------------------------------------------------------------\n";

            RobustConsole.Write(LogLevel.Info, this, GLINFO);
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
            AltScreens.RenderFrame += Render;
            AltScreens.UpdateFrame += Update;

            // GL.Enable(EnableCap.Blend);
            // GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            RobustEngine.CheckGLErrors();
            GL.ClearColor(0, 0, 0, 0);           

            var ShaderTestFile = Path.Combine(Environment.CurrentDirectory, "Graphics", "Shaders", "ImageTest");
            var TextureTestFile = Path.Combine(Environment.CurrentDirectory, "Devtexture_Floor.png");

            Texture = new Texture2D();            
            RectTest = new Rect2D();
            TriangleTest = new Triangle2D();    
            PointTest = new Point2D();
            VBUFF = new GLVertexBuffer(UsageHint.Dynamic | UsageHint.Write);
            IBUFF = new GLIndexBuffer(UsageHint.Dynamic | UsageHint.Write);
            RenderTarget = new RobustFrameBuffer();
          


            RectTest.SetOrigin(RectTest.Center);  
            //RectTest.SetRotation(new Vector3(0,0,45));
            RectTest.SetFillColor(Color.DarkBlue);

            TriangleTest.SetOrigin(TriangleTest.Center);
            TriangleTest.SetRotation(new Vector3(0,0,135));

      

            AltScreens.MakeCurrent();

            VAOID = GL.GenVertexArray();    
            GL.BindVertexArray(VAOID);  
            Texture.LoadImage(TextureTestFile);
            VBUFF.Init();
            IBUFF.Init();      
            RenderTarget.Init(800,800);      
            CurrentShader = new Shader(ShaderTestFile + ".vert", ShaderTestFile + ".frag");
            GL.BindVertexArray(0);

            VBUFF.Update(TriangleTest.VertexData);
            IBUFF.Update(TriangleTest.Indicies);
            
            GameScreen.MakeCurrent();
            Shape2DBatchTest = new Shape2DBatch("test", 800,800);
            VAOID2 = GL.GenVertexArray(); 
            GL.BindVertexArray(VAOID2);  
            Shape2DBatchTest.Setup();
            CurrentShader2 = new Shader(ShaderTestFile + ".vert", ShaderTestFile + ".frag");         
            GL.BindVertexArray(0);

            int b = 100;
            Vector3 rot = new Vector3(0,0,45);
            for(int ix=0; ix < b; ix++)            
            {   
                for(int iy=0; iy < b; iy++)
                {                  
                   Vector2 pos = new Vector2((((float)ix/b)-0.5f),((float)iy/b)-.5f);
                    RectTest2 = new Rect2D(0,0,.01f,.01f);   
                    RectTest2.SetRotation(rot);
                    RectTest2.SetPosition(pos);
                    Shape2DBatchTest.Queue(RectTest2);
                }
            }
            
            Timekeeper.Start();
            Shape2DBatchTest.Process(); 
            Timekeeper.Stop();

            Console.WriteLine(Shape2DBatchTest.BatchSize + " total time: " + Timekeeper.GetElapsed().TotalMilliseconds);    
            Shape2DBatchTest.BatchSize=1;     
          //  Shape2DBatchTest.BatchModelMatrix*= Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(-90));
            //PlayerView = new View(Vector2.Zero, 0, 100);            
            //GL.Viewport(0, 0, 800, 800);


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
            AltScreens.Run(60);
        }

        Vector3 newrott = Vector3.One;
        public void Update(object Sender, FrameEventArgs E)
        {
            mov=.05f;

           
            
            //RectTest.SetRotation(newrott);
            //VBUFF.Update(RectTest.VertexData);
            Shape2DBatchTest.ShapeQueue.ForEach(delegate(Shape2D shape){shape.SetRotation(newrott);});  
            Shape2DBatchTest.ModelMatrix*=Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(.05f));
            //Shape2DBatchTest.Process();

            GameScreen.ProcessEvents();
            AltScreens.ProcessEvents();
            Shape2DBatchTest.BatchSize+=1; 
            
            if(Shape2DBatchTest.BatchSize>=10000)
            {
                 Shape2DBatchTest.BatchSize=1;
            }
           
           // Console.WriteLine(Shape2DBatchTest.BatchSize);

            // Shape2DBatchTest.BatchModelMatrix*= Matrix4.CreateRotationZ(.005f);
            // Shape2DBatchTest.BatchModelMatrix*= Matrix4.CreateRotationY(.005f);
          // RectTest.SetRotation(new Vector3(0,0,-mov));
        }

        float mov = 0f;

        float scale = 1 / 256f;

        int frames;
        Vector2 scal = new Vector2(1f, 1f);
        bool dirty=true;

        public void Render(object Sender, FrameEventArgs E)
        {        
    
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            GameScreen.MakeCurrent();   
                GL.Clear(ClearBufferMask.ColorBufferBit);
                CurrentShader.Enable();
                Texture.Bind();  

                
                RobustEngine.CurrentShader.setUniform("ModelMatrix",Shape2DBatchTest.ModelMatrix );
                RobustEngine.CurrentShader.setUniform("UsingTexture", 1);
               
                GL.BindVertexArray(VAOID2);                   
                Shape2DBatchTest.Draws();  
                GL.BindVertexArray(0); 
               
               
                Texture.Unbind();
                CurrentShader.Disable();
            GameScreen.SwapBuffers();

         
            AltScreens.MakeCurrent();   
                
                GL.BindVertexArray(VAOID); 
                RenderTarget.Begin(); 
                GL.Clear(ClearBufferMask.ColorBufferBit);                          
                CurrentShader2.Enable();
                Texture.Bind();
                
               
                CheckGLErrors();

                VBUFF.Update(RectTest.VertexData);
                IBUFF.Update(RectTest.Indicies);
                IBUFF.Bind();

                RobustEngine.CurrentShader2.setUniform("ModelMatrix", RectTest.ModelMatrix );
                RobustEngine.CurrentShader2.setUniform("UsingTexture",1);
                
                GL.DrawElements(PrimitiveType.LineStrip , 6, DrawElementsType.UnsignedInt, 0);
                RenderTarget.End();
                Texture.Unbind();
               
                
                RenderTarget.Bind();                  
     
                GL.Clear(ClearBufferMask.ColorBufferBit);   
     
                VBUFF.Update(RenderTarget.VertexData);
                IBUFF.Update(RenderTarget.Indicies);
                IBUFF.Bind();

                RobustEngine.CurrentShader2.setUniform("ModelMatrix", RenderTarget.ModelMatrix );
                RobustEngine.CurrentShader2.setUniform("UsingTexture", 1);
                
               // GL.DrawElements(PrimitiveType.LineStrip , 6, DrawElementsType.UnsignedInt, 0);
               
                //GL.DrawArrays(PrimitiveType.Triangles,0,3);
                IBUFF.Unbind();                
                RenderTarget.Unbind();   
               
                GL.BindVertexArray(0);                 
                CurrentShader2.Disable();
            AltScreens.SwapBuffers();  

            frames++;
            if (Timekeeper.GetElapsed().Seconds >= 1)
            {
                RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() FPS " + frames);
                Timekeeper.Start();
                frames = 0;
                CheckGLErrors();
            }
            // RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Render() MS " + Timekeeper.GetTime().Milliseconds.ToString());
        }


        public void Stop()
        {
            RobustConsole.Write(LogLevel.Debug, "RobustEngine", "Stopping...");
        }

        #endregion State

        public void setCurrentRenderTarget(Framebuffer RT)
        {

        }

        public void setScreenTarget(NativeWindow NW)
        {

        }

        #region Global Error Checking
        public static void CheckGLErrors()
        {
            var FBOEC = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            while ((FBOEC != FramebufferErrorCode.FramebufferComplete))
            {
                RobustConsole.Write(LogLevel.Verbose, "RobustEngine", FBOEC.ToString()); //TODO expand error message
                FBOEC = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
            }

            var EC = GL.GetError();
            while (EC != ErrorCode.NoError)
            {
                RobustConsole.Write(LogLevel.Fatal, "RobustEngine", EC.ToString()); // TODO expand error message
                EC = GL.GetError();
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