using OpenTK;
using OpenTK.Graphics;
using OpenTK.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobustEngine.Window
{
    public class RobustWindow : GameWindow
    {
        //Create window
        //setup context
        //begin drawing
        //end drawing/swapbuff
     //   private WindowSettings Windowsettings;

        private bool isFocused;

        public RobustWindow(string title, int width, int height)
        {


        }

        public RobustWindow(string title, int width, int height, GameWindowFlags GWF, GraphicsMode GM, DisplayDevice DD)// : base(width, height, title, GWF, GM, DD)
        {
          //  Context = new GraphicsContext(GraphicsMode.Default, base.WindowInfo);
         //   Context.MakeCurrent(base.WindowInfo);
         //   base.Visible = true;
        }


        //Subscribe to OnUpdate. 
        //Update window


        public void OnRender()
        {


        }

        public void OnUpdate()
        {

        }

        public void OnClick()
        {
            isFocused = true;

        }
     

     
    }
    
}
