
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using RobustEngine.Graphics;
using RobustEngine.Graphics.Render;
using RobustEngine.Graphics.Shaders;
using RobustEngine.Graphics.Sprites;
using RobustEngine.System;
using RobustEngine.Window;
using System.Drawing;



namespace RobustEngine
{
    public class RobustStarter
    {



        public static Texture2D tex;
        public static View view;
        public static SpriteBatch sp;

        public static bool ondown;

        public static GameWindow gam;
        public static RobustEngine RE;
 




        public static void Main()
        {
            RobustConsole.SetLogLevel(LogLevel.Debug);

            var Rp = new RobustPopup();


            RE = new RobustEngine();
            RE.Init();
            RE.Run();

           // RE.GameScreen.Mouse.ButtonDown += Game_MouseDown;
           // tex = new Texture2D("Devtexture_Floor.png");
           // view = new View(Vector2.One, 0, 10);
           // sp = new SpriteBatch(600, 800);
           // //var test = new RenderTarget(600, 800);
           //// var shader = new Shader(@"S:\Projects\Groups\Space Wizards Federation\RobustEngine\Graphics\Shaders\ImageTest.vert", @"S:\Projects\Groups\Space Wizards Federation\RobustEngine\Graphics\Shaders\ImageTest.frag");
       
           // GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
           // GL.ClearColor(Color.Black);            

         
           //view.Setup(1920 ,1080);

           // sp.Begin();
           // for (int i = 0; i <10; i++)
           // {
           //     for (int iy = 0; iy < 10; iy++)
           //     {
           //         sp.Draw(tex, new Vector2(i * 256, iy * 256), new Vector2(.1f,.1f), Color.Yellow, new Vector2(1,1));
              
           //     }

           // }

           // view.Update();          
         
        
          
           

            //    using (var game = new GameWindow())
            //    {
            //        gam = game;
            //        game.Load += (sender, e) =>
            //        {
            //           
            //        };

            //        game.Resize += (sender, e) =>
            //        {
            //            GL.Viewport(0, 0, game.Width, game.Height);
            //        };

            //        game.UpdateFrame += (sender, e) =>
            //        {

            //            //view.Position += Vector2.One;
            //            view.Update();



            //                // add game logic, input handling
            //                if (game.Keyboard[Key.Escape])
            //            {
            //                game.Exit();

            //            }
            //        };

            //        game.RenderFrame += (sender, e) =>
            //        {
            //            // render graphics


            //            //RobustConsole.Write(LogLevel.Debug, game, "aaaaaa");
            //           
            //            game.SwapBuffers();
            //        };
            //        game.MouseDown += Game_MouseDown;


            //// Run the game at 60 updates per second
            //game.Run(60.0);
            //    }
        }
        private static void Game_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {

                Vector2 muspos = new Vector2(e.Position.X, e.Position.Y);

                muspos -= new Vector2(gam.Width, gam.Height) / 2f;
                view.Position = view.ToWorld(muspos);
                RobustConsole.Write(LogLevel.Info, gam, "View POS was set to " + view.Position);

            }
            if (e.Button == MouseButton.Right)
            {

               view.Zoom *= .5;
               RobustConsole.Write(LogLevel.Info, gam, "View zoom was set to " + view.Zoom);

            }

        }

    }


    /*
     *     GL.MatrixMode(MatrixMode.Projection);
                    GL.LoadIdentity();
                    GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
                    
                    GL.Begin(PrimitiveType.Quads);

                    GL.Vertex2(0,0);
                    GL.TexCoord2(0,0);

                    GL.Vertex2(100,0);
                    GL.TexCoord2(1, 0);

                    GL.Vertex2(100, 100);
                    GL.TexCoord2(1, 1);

                    GL.Vertex2(0,100);
                    GL.TexCoord2(0, 1);
                    GL.End();
                    vertices[i] -= origin;
                    vertices[i] *= scale;
                    vertices[i] += position;
     */
}


