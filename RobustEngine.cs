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
using RobustEngine.Graphics.Sprites2D;
using RobustEngine.Graphics.Interfaces;
using RobustEngine.System;
using RobustEngine.System.Settings;
using RobustEngine.System.Time;
using RobustEngine.Window;
using RobustEngine.Graphics.OpenGL;
using RobustEngine.Graphics.Batching2D;
using RobustEngine.Graphics.Shapes2D;
using RobustFrameBuffer = RobustEngine.Graphics.Render.Framebuffer;

namespace RobustEngine
{
    public class RobustEngine
    {
        //Render engine. Handles all rendering.      
        public int version;
        public string GLINFO;
        public string EXTENSIONS;

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
        public View SpritebatchView;
        public Clock Timekeeper;
        public Sprite2DBatch Spritebatch;
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

        public IVertexBuffer VBUFF;
        public IIndexBuffer IBUFF;

        public Sprite2D Sprite;
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
            AltScreens = new GameWindow(800, 800, GraphicsMode.Default, "RobustWando2", GameWindowFlags.Default, DisplayDevice.Default, 4, 3, GraphicsContextFlags.Debug);

            //GameScreen = new GameWindow();
            //GameScreen.Size = VSettings.Size;
            //GameScreen.WindowBorder = VSettings.Border;
            //GameScreen.Title = "Space Station 14";
            //GameScreen.Visible = true;

            // GameScreen.VSync = VSyncMode.Off;
            AltScreens.MakeCurrent();
            GameScreen.MakeCurrent(); //OPENGL CONTEXT STARTS HERE
            AltScreens.Visible=true;

            PrintOGLInfo();
            
            GameScreen.RenderFrame += Render;
            GameScreen.UpdateFrame += Update;
            AltScreens.RenderFrame += Render;
            AltScreens.UpdateFrame += Update;

            // GL.Enable(EnableCap.Blend);
            // GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            RobustEngine.CheckGLErrors();
            GL.ClearColor(0, 0, 0, 0);           

            var ShaderTestFile = Path.Combine(Environment.CurrentDirectory, "Graphics", "Shaders", "ImageTest");
            var TextureTestFile = Path.Combine(Environment.CurrentDirectory, "floor_texture.png");

            Texture = new Texture2D(); 
            TriangleTest = new Triangle2D();    
            PointTest = new Point2D();
            VBUFF = GraphicsAPI.Resolve<IVertexBuffer>();
            IBUFF = GraphicsAPI.Resolve<IIndexBuffer>();
            RenderTarget = new RobustFrameBuffer();
          

            RectTest = new Rect2D();
            RectTest.SetOrigin(RectTest.Center);  
            //RectTest.SetRotation(new Vector3(0,0,45));
            //RectTest.SetFillColor(Color.DarkBlue);

            AltScreens.MakeCurrent();

            VAOID = GL.GenVertexArray();    
            GL.BindVertexArray(VAOID);  

            Timekeeper.Start();
           // Texture.LoadImageUsingImagesharp(TextureTestFile);
            Timekeeper.Stop();
            Console.WriteLine("Texture create " + Timekeeper.GetElapsed().TotalMilliseconds + "MS");    

            VBUFF.Bind();
            VBUFF.Create();
            VBUFF.Update(TriangleTest.VertexData);            
            VBUFF.Unbind();

            IBUFF.Bind();
            IBUFF.Create();
            IBUFF.Update(TriangleTest.Indicies);
            IBUFF.Unbind();  

            RenderTarget.Init(800,800);      
            CurrentShader = new Shader(ShaderTestFile + ".vert", ShaderTestFile + ".frag");
            GL.BindVertexArray(0);

            
           
            
            GameScreen.MakeCurrent();
            Shape2DBatchTest = new Shape2DBatch("test", 800,800);
            VAOID2 = GL.GenVertexArray(); 
            GL.BindVertexArray(VAOID2);  
            Shape2DBatchTest.Setup();
            CurrentShader2 = new Shader(ShaderTestFile + ".vert", ShaderTestFile + ".frag");         
            GL.BindVertexArray(0);

            int b = 10;
            Vector3 rot = new Vector3(0,0,45);
            Timekeeper.Start();
            for(int ix=0; ix < b; ix++)            
            {   
                for(int iy=0; iy < b; iy++)
                {                  
                   Vector2 pos = new Vector2((((float)ix/b)-0.5f),((float)iy/b)-.5f);
                    RectTest2 = new Rect2D(0,0,.1f,.1f);   
                    RectTest2.SetRotation(rot);
                    RectTest2.SetPosition(pos);
                    Shape2DBatchTest.Queue(RectTest2);
                    
                }
            }
            Timekeeper.Stop();
            Console.WriteLine(b + " loop total time: " + Timekeeper.GetElapsed().TotalMilliseconds + "MS");    
        

            Timekeeper.Start();
            Shape2DBatchTest.Process(); 
            Timekeeper.Stop();
            Console.WriteLine(Shape2DBatchTest.BatchSize + " total time: " + Timekeeper.GetElapsed().TotalMilliseconds + " MS");    
           
            PlayerView = new View(Vector2.Zero, 0, 1);
            PlayerView.Setup(800,800);  

            SpritebatchView = new View(Vector2.Zero, 0, 1);
            SpritebatchView.Setup(800,800);  

            RobustConsole.Write(LogLevel.Info, "RobustEngine", "Init() Done.");
            ReadyToRun = true;
        }

        #region State 
        public void Run()
        {

            RobustConsole.Write(LogLevel.Fatal, "RobustEngine", "Starting Run loop...");

            Timekeeper.Start();
            GameScreen.Run(60);
            AltScreens.Run(60);
        }

        Vector3 newrott = Vector3.One;
        float poscam = 0;
        public void Update(object Sender, FrameEventArgs E)
        {
            mov=50f;
            poscam+=.00001f;

            newrott= new Vector3(0,0,MathHelper.DegreesToRadians(.05f));
            
           // RectTest.SetRotation(newrott);
           // VBUFF.Update(RectTest.VertexData);
          
           //Shape2DBatchTest.ModelMatrix*=Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(.05f));

            GameScreen.ProcessEvents();
            AltScreens.ProcessEvents();
            Shape2DBatchTest.BatchSize+=1; 
           
            if(Shape2DBatchTest.BatchSize>=10000)
            {
                 Shape2DBatchTest.BatchSize=1;
            }
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
               GL.BindVertexArray(VAOID2);    
                GL.Clear(ClearBufferMask.ColorBufferBit);
               // CurrentShader.Enable();
                //Texture.Bind();  

          
    
               // RobustEngine.CurrentShader.setUniform("ModelMatrix",Matrix4.Identity);
               // RobustEngine.CurrentShader.setUniform("UsingTexture", 0);
               
                SpritebatchView.Update();  
                Shape2DBatchTest.Draws();  
                GL.BindVertexArray(0); 
               
               
               // Texture.Unbind();
              //  CurrentShader.Disable();
                GL.BindVertexArray(0); 
            GameScreen.SwapBuffers();

         
            AltScreens.MakeCurrent();   
               
                GL.Clear(ClearBufferMask.ColorBufferBit);   
                GL.BindVertexArray(VAOID); 
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
               // RenderTarget.Begin(); 
                 GL.Clear(ClearBufferMask.ColorBufferBit);   
              //  CurrentShader2.Enable();
                Texture.Bind();              
                 

                VBUFF.Update(RectTest.VertexData);
                IBUFF.Update(RectTest.Indicies);
                IBUFF.Bind();
             
               // RobustEngine.CurrentShader2.setUniform("ModelMatrix", RectTest.ModelMatrix );
               // RobustEngine.CurrentShader2.setUniform("UsingTexture",0);
                
                GL.DrawElements(PrimitiveType.Triangles, 6, DrawElementsType.UnsignedInt, 0);
              
             //  RenderTarget.End();
        
               
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
                Texture.Unbind();
                IBUFF.Unbind();
                
               // RenderTarget.Bind();     
                        
                VBUFF.Update(RenderTarget.VertexData);
                IBUFF.Update(RenderTarget.Indicies);
                IBUFF.Bind();
                PlayerView.Update();
             //   RobustEngine.CurrentShader2.setUniform("ModelMatrix", RenderTarget.ModelMatrix );
              //  RobustEngine.CurrentShader2.setUniform("UsingTexture",1);
                
                GL.DrawElements(PrimitiveType.Triangles , 6, DrawElementsType.UnsignedInt, 0);               

                
                //GL.DrawArrays(PrimitiveType.Triangles,0,3);
                IBUFF.Unbind();                
              //  RenderTarget.Unbind();   
              //  CurrentShader2.Disable();
                GL.BindVertexArray(0);                 
               
            AltScreens.SwapBuffers();  

            frames++;
            if (Timekeeper.GetElapsed().Seconds >= 1)
            {
                PlayerView.TranslateTo(new Vector2(poscam,poscam));
               
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



        public void PrintOGLInfo()
        {
            GLINFO = "";
            GLINFO += "\n\n-------------- OpenGL Initialization Report -----------------------";         
            GLINFO += "\n OpenGL Renderer   : " + GL.GetString(StringName.Renderer);
            GLINFO += "\n OpenGL Version    : " + GL.GetString(StringName.Version);
            GLINFO += "\n OpenGL Vendor     : " + GL.GetString(StringName.Vendor);
            GLINFO += "\n Context Flags     : " + GL.GetInteger(GetPName.ContextFlags);
            GLINFO += "\n GLSL Version      : " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n---------------------------------------------------------------------\n";
            RobustConsole.Write(LogLevel.Info, this, GLINFO);

        }

        public void PrintOGLInfoExtendedBecauseSomethingBroke()
        {          
            int i = 0;
            GLINFO = "";
            EXTENSIONS = "";    
            var ext= "";        
            var ssl= "";
            var ss2= "";       
    
            while (true)
            {
                ext = GL.GetString(StringNameIndexed.Extensions,i);
                if (ext != "")
                {
                    ssl = GL.GetString(StringNameIndexed.ShadingLanguageVersion,i);
                    if(ssl != "")
                    {
                        ss2+= ssl + ",";
                    }

                    EXTENSIONS+= "\n" + ext; 
                    i++;       
                }
                else
                {
                    break;
                }  
            }           

            GLINFO += "\n--------------[ OpenGL Initialization Report ]-----------------------";
            GLINFO += "\n OpenGL Renderer   : " + GL.GetString(StringName.Renderer);
            GLINFO += "\n OpenGL Version    : " + GL.GetString(StringName.Version);
            GLINFO += "\n OpenGL Vendor     : " + GL.GetString(StringName.Vendor);
            GLINFO += "\n Context Flags     : " + GL.GetInteger(GetPName.ContextFlags);
            GLINFO += "\n GLSL Version      : " + GL.GetString(StringName.ShadingLanguageVersion);
            GLINFO += "\n GLSL Versions     : " + ss2;

            GLINFO += "\n\n----------------[ OpenGL EXT/ARB Report ]--------------------------\n";
            GLINFO += ""; 
            GLINFO += "Total Extensions found:" + i;
            GLINFO +=  EXTENSIONS;
            GLINFO += "\n---------------------------------------------------------------------\n";

            RobustConsole.Write(LogLevel.Info, this, GLINFO);

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